using System;
using System.Threading;

namespace GameServer
{
    class Program
    {
        private static bool isRunning = false;

        static void Main(string[] args)
        {
            Console.Title = "Game Server";
            isRunning = true;

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            Server.Start(50, 26950);
            // List of commonly used ports: https://www.youtube.com/redirect?event=video_description&v=uh8XaC0Y5MA&redir_token=QUFFLUhqbEdlUVZMblc2a0llUzdRODd2NHoxdTBCWWpSUXxBQ3Jtc0trb2VxV1pxMmNUaWVTeklwcXc4TzIxMWM0V3IwYWR5Tnl4UW8tTE5jRmFKLUYzWEpCLVRTTVZMUEh0d1A0bmltWURjM0Y3dENwZkZmWUtTSFg2Uk5TQnlQRzdXbUNwSlVSbmZYS0VDX09CYUtGb3Bzbw%3D%3D&q=https%3A%2F%2Fen.wikipedia.org%2Fwiki%2FList_of_TCP_and_UDP_port_numbers
        }

        private static void MainThread()
        {
            Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SEC} ticks per second.");
            DateTime _nextLoop = DateTime.Now;

            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    GameLogic.Update();

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }
    }
}
