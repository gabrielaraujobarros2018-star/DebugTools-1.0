using Common;

namespace LogTracer {
    class Program {
        static void Main(string[] args) {
            bool verbose;
            string output;
            string[] files = Utils.ParseArgs(args, out verbose, out output);

            if (files.Length == 0) {
                Utils.Log("Usage: dotnet run -- [--verbose] [--out=file.txt] <files...>", "ERROR");
                return;
            }

            Utils.Log("LogTracer started", "INFO");
            foreach (string file in files) {
                Utils.ValidateFile(file);
                string[] lines = File.ReadAllLines(file);
                int errors = 0;

                for (int i = 0; i < lines.Length; i++) {
                    string line = lines[i];
                    if (verbose || line.Contains("ERROR") || line.Contains("EXCEPTION")) {
                        Utils.Log($"[{file}:{i+1}] {line}", "TRACE");
                        if (line.Contains("ERROR")) errors++;
                    }
                }
                Utils.Log($"Processed {file}: {errors} errors found", "SUMMARY");
            }

            long mem = Utils.GetProcessMemory();
            Utils.Log($"Total memory used: {mem / 1024 / 1024} MB", "STATS");
            Utils.DumpStack("LogTracer complete");
        }
    }
}
