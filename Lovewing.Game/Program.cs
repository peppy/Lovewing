// Copyright (c) 2014-2017 Clara.
// Licensed under Eclipse Public License 1.0

using osu.Framework.Desktop;
using osu.Framework.Platform;

namespace Lovewing.Game
{
    public static class Program
    {
        public static void Main(string[] args)
        {

            using (GameHost host = Host.GetSuitableHost(""))
            {
                host.Run(new LovewingGame());
            }
        }
    }
}
