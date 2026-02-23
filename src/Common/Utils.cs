using System;
using System.IO;
using System.Diagnostics;

namespace Common {
    public static class Utils {
        public static void Log(string msg, string level = "INFO") {
            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string logEntry = $"[{timestamp}] [{level}] {msg}";
            Console.WriteLine(logEntry);
            File.AppendAllText("debug.log", logEntry + Environment.NewLine);
        }

        public static string[] ParseArgs(string[] args, out bool verbose, out string output) {
            verbose = args.Contains("--verbose") || args.Contains("-v");
            output = args.FirstOrDefault(a => a.StartsWith("--out="))?.Split('=')[1] ?? "output.txt";
            return args.Where(a => !a.StartsWith("--") && !a.StartsWith("-")).ToArray();
        }

        public static long GetProcessMemory() {
            using var process = Process.GetCurrentProcess();
            return process.WorkingSet64;
        }

        public static void DumpStack(string reason = "Manual dump") {
            Console.WriteLine($"=== Stack Dump: {reason} ===");
            Console.WriteLine(Environment.StackTrace);
            Utils.Log($"Stack dumped: {reason}");
        }

        public static void ValidateFile(string path) {
            if (!File.Exists(path)) {
                Utils.Log($"File not found: {path}", "ERROR");
                Environment.Exit(1);
            }
        }
    }
}
