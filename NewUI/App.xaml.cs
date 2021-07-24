﻿using System;
using System.Threading;
using System.Windows;


namespace NewUI {
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {
        private static readonly Mutex mutex = new Mutex(true, "{4C416227-4A30-4A2F-8F23-8701544DD7D6}");

        private void Application_Startup(object sender, StartupEventArgs e) {
            // 不允许多个实例同时运行
            if (!mutex.WaitOne(TimeSpan.Zero, true)) {
                Current.Shutdown();
                // 已存在实例时广播 WM_SHOWME，唤醒该实例
                _ = NativeMethods.BroadcastMessage(NativeMethods.MAGPIE_WM_SHOWME);
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e) {
            mutex.ReleaseMutex();
        }
    }
}
