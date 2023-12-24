using System;
using Microsoft.Win32;

class Program
{
    private static DateTime lockTime;
    private static DateTime unlockTime;

    static void Main()
    {
        SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
        Console.ReadLine();
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
                Console.WriteLine("Duration of lock: " + duration.ToString());
                Console.WriteLine("Days: {0}", duration.Days);
                Console.WriteLine("Hours: {0}", duration.Hours);
                Console.WriteLine("Minutes: {0}", duration.Minutes);
                Console.WriteLine("Seconds: {0}", duration.Seconds);
                break;
        }
    }
}
