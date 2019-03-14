using osu.Framework.Platform;
using osu.Framework;
using System;

namespace Lovewing
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using (Game game = new LovewingGame())
            using (GameHost host = Host.GetSuitableHost(@"Project Lovewing"))
                host.Run(game);
        }
    }
}
