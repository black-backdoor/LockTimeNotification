using System;
using System.Drawing; // Required for Icon
using System.Windows.Forms; // Required for NotifyIcon
using Microsoft.Win32;

class Program
{
    private static DateTime lockTime = DateTime.Now;
    private static DateTime unlockTime;
    private static NotifyIcon notifyIcon;

    static void Main()
    {
        notifyIcon = new NotifyIcon
        {
            Icon = new Icon("icon.ico"),
            Visible = true
        };

        // Create context menu with an Exit option
        ContextMenuStrip contextMenu = new ContextMenuStrip();
        ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Exit");
        exitMenuItem.Click += (sender, e) =>
        {
            Application.Exit();
        };
        contextMenu.Items.Add(exitMenuItem);
        notifyIcon.ContextMenuStrip = contextMenu;

        SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);

        Console.WriteLine("Monitoring machine lock/unlock events.");

        Application.Run();
        notifyIcon.Dispose();
    }

    static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
    {
        switch (e.Reason)
        {
            // Machine Locked
            case SessionSwitchReason.SessionLock:
                lockTime = DateTime.Now;
                Console.WriteLine("Machine locked at: " + lockTime);
                break;

            // Machine Unlocked
            case SessionSwitchReason.SessionUnlock:
                unlockTime = DateTime.Now;
                Console.WriteLine("Machine unlocked at: " + unlockTime);
                TimeSpan duration = unlockTime - lockTime;

                string formattedTime = "";
                if (duration.Days > 0) { formattedTime += $"{duration.Days}d "; }
                if (duration.Hours > 0) { formattedTime += $"{duration.Hours}h "; }
                if (duration.Minutes > 0) { formattedTime += $"{duration.Minutes}m "; }
                formattedTime += $"{duration.Seconds}s";

                Console.WriteLine("Duration of lock: " + formattedTime);
                Console.WriteLine();
                ShowNotification("Machine Unlocked", $"Your machine was locked for {formattedTime}");
                break;
        }
    }

    static void ShowNotification(string title, string message)
    {
        notifyIcon.BalloonTipTitle = title;
        notifyIcon.BalloonTipText = message;
        notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
        notifyIcon.ShowBalloonTip(5000);
    }
}