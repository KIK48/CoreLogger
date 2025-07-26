using System;
using System.IO;
using BepInEx;
using UnityEngine;

namespace CoreLoggerMod
{
    [BepInPlugin("com.kike.corelogger", "CoreLogger", "1.0.0")]
    public class CoreLogger : BaseUnityPlugin
    {
        public static string BaseDir;

        private void Awake()
        {
            BaseDir = Path.Combine(BepInEx.Paths.BepInExRootPath, "Logs");
            Logger.LogInfo("CoreLogger initialized.");
        }

        public static void Log(string modName, string playerId, string fileName, string message)
        {
            try
            {
                string playerDir = string.IsNullOrEmpty(playerId) ? "UnknownPlayer" : $"Player_{playerId}";
                string logDir = Path.Combine(BaseDir, modName, playerDir);
                Directory.CreateDirectory(logDir);

                string filePath = Path.Combine(logDir, fileName);
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[CoreLogger] Failed to log message: {ex}");
            }
        }
    }
}
