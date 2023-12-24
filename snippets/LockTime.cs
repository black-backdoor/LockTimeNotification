using System;
using Microsoft.Win32;

class Program
{
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
                Console.WriteLine("Machine locked at: " + DateTime.Now);
                break;
            // Machine Unlocked
            case SessionSwitchReason.SessionUnlock:
                Console.WriteLine("Machine unlocked at: " + DateTime.Now);
                break;
        }
    }
}
