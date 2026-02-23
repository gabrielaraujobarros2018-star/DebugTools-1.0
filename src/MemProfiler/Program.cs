using Common;
using System;
using System.Diagnostics;
using System.Threading;

namespace MemProfiler {
    class Program {
        static void Main(string[] args) {
            bool verbose;
            Utils.ParseArgs(args, out verbose, out _);
            Utils.Log("MemProfiler running - monitoring every 2s (Ctrl+C to stop)", "INFO");

            var stopwatch = Stopwatch.StartNew();
            long[] samples = new long[10];
            int idx = 0;

            while (true) {
                try {
                    Thread.Sleep(2000);
                    long mem = Utils.GetProcessMemory();
                    samples[idx % samples.Length] = mem;
                    idx++;

                    if (verbose || idx % 5 == 0) {
                        double avg = samples.Take(idx).Average() / 1024 / 1024;
                        Utils.Log($"Memory: {mem / 1024 / 1024} MB (avg: {avg:F1} MB)", "MEM");
                    }

                    // Allocate to simulate growth
                    if (idx % 3 == 0) {
                        var list = new List<byte[]>(1000);
                        for (int i = 0; i < 1000; i++) list.Add(new byte[1024]);
                        GC.Collect();  // Force collection for demo
                    }
                } catch (Exception ex) {
                    Utils.Log($"Error: {ex.Message}", "ERROR");
                    Utils.DumpStack("Mem error");
                    break;
                }
            }

            stopwatch.Stop();
            Utils.Log($"Profiled for {stopwatch.Elapsed.TotalSeconds:F1}s", "SUMMARY");
        }
    }
}
