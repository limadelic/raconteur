using System;
using System.IO;

namespace Raconteur.Helpers
{
    public class RunnerFileWatcher
    {
        static FileSystemWatcher Watcher;
        static string Path;
        
        public static void Start(string Path=null)
        {
            RunnerFileWatcher.Path = Path;

            if (IsRunning) ResetTimer();
            else StartTimer();   
        }

        static bool IsRunning { get { return Watcher != null; } }

        static void StartTimer()
        {
            Watcher = new FileSystemWatcher
            {
                Path = Path ?? ".",
                Filter = "*.runner.cs",
                IncludeSubdirectories = true,
            };

            Watcher.Changed += RunnerFileChanged;

            Watcher.EnableRaisingEvents = true;
        }

        static void RunnerFileChanged(object sender, FileSystemEventArgs e) 
        {
            FileChangeHandler(e.FullPath);
        }

        static void ResetTimer() {  }

        static Action<string> FileChangeHandler; 

        public static void OnFileChange(Action<string> FileChangeHandler)
        {
            RunnerFileWatcher.FileChangeHandler = FileChangeHandler;
            Start();
        }
    }
}