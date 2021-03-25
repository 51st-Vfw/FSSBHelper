using System;

namespace FSSBHelper
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting FSSBHelper");
                Run();
            }
            catch (Exception ex)
            {
                WaitToExit($"{ex.Message}{Environment.NewLine}{ex.StackTrace}");
            }
            finally
            {
                Console.WriteLine("Closing FSSBHelper");
            }
        }

        private static void Run()
        {
            var settings = new Settings();
            using (var joystick = new JoystickHelper(settings))
            {
                if (settings.Debug)
                    Console.WriteLine($"*DEBUG MODE ENABLED* - {joystick}");

                using (var audio = new AudioHelper(settings))
                    using (var monitor = new JoystickMonitor(joystick, audio, settings))
                    {
                        monitor.Start();
                        WaitToExit($"Monitoring ({settings.JoystickName}) every {settings.IntervalMS} milliseconds");
                    }
            }

            if (settings.Debug)
            {
                Console.WriteLine($"*DEBUG MODE COMPLETE*");
                System.Threading.Thread.Sleep(2000);
            }

        }

        private static void WaitToExit(string message)
        {
            Console.WriteLine("");
            Console.WriteLine(message);
            Console.WriteLine("");
            Console.WriteLine("Press Enter to Exit");
            Console.WriteLine("");
            Console.ReadLine(); //hold program!
        }
    }
}
