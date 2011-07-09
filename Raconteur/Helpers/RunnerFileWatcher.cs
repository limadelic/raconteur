using System;
using System.IO;
using System.Timers;

namespace Raconteur.Helpers
{
    public static class RunnerFileWatcher
    {
        public const int DefaultTimeout = 60000;

        public static int Timeout = DefaultTimeout;

        static readonly FileSystemWatcher Watcher;

        static RunnerFileWatcher()
        {
            Timer = new Timer(Timeout);
            Timer.Elapsed += StopWatching;

            Watcher = new FileSystemWatcher
            {
                Path = ".",
                Filter = "*.runner.cs",
                IncludeSubdirectories = true,
            };
            Watcher.Changed += RunnerFileChanged;
        }

        public static void OnFileChange(Action<string> FileChangeHandler)
        {
            RunnerFileWatcher.FileChangeHandler = FileChangeHandler;
            StartWatching();
        }

        static void RunnerFileChanged(object sender, FileSystemEventArgs e) 
        {
            FileChangeHandler(e.FullPath);
        }

        static Action<string> FileChangeHandler; 

        public static string Path { set { Watcher.Path = value; } }

        public static bool IsRunning { get { return Watcher.EnableRaisingEvents; } }

        static readonly Timer Timer;

        static void StartWatching()
        {
            Timer.Interval = Timeout;
            Timer.Start();

            Watcher.EnableRaisingEvents = true;
        }

        static void StopWatching(object Sender, ElapsedEventArgs E)
        {
            Watcher.EnableRaisingEvents = false;
            Timer.Stop();
        }
    }
}