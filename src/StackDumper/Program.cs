using Common;
using System.Threading;

namespace StackDumper {
    class Program {
        static void Main(string[] args) {
            bool verbose;
            Utils.ParseArgs(args, out verbose, out _);
            Utils.Log("StackDumper active - press Ctrl+C to dump", "INFO");

            // Simulate threaded app with stack buildup
            var threads = new Thread[5];
            for (int i = 0; i < 5; i++) {
                int id = i;
                threads[i] = new Thread(() => SimulateWork(id, verbose));
                threads[i].Start();
            }

            Console.ReadLine();  // Wait for input
            Utils.DumpStack("User interrupt");

            foreach (var t in threads) t.Join();
            Utils.Log("All threads terminated", "INFO");
        }

        static void SimulateWork(int id, bool verbose) {
            while (true) {
                if (verbose) Utils.Log($"Thread {id} working...", "DEBUG");
                Thread.Sleep(1000);
                // Deep call stack simulation
                MethodA(id);
            }
        }

        static void MethodA(int id) => MethodB(id);
        static void MethodB(int id) => MethodC(id);
        static void MethodC(int id) => MethodD(id);
        static void MethodD(int id) {
            Utils.Log($"Deep stack reached in thread {id}", "TRACE");
        }
    }
}
