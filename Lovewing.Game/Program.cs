// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using System;
using osu.Framework;
using osu.Framework.Platform;

namespace Lovewing.Game
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            using (GameHost host = Host.GetSuitableHost(@"Lovewing"))
            {
                if (args.Length > 0 && args[0] == "--tests")
                    host.Run(new LovewingTests());
                else
                    host.Run(new LovewingGame());
            }
        }
    }
}
