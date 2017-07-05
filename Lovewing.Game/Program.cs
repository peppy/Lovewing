// Copyright (c) 2007-2017 Clara.
// Licensed under the MIT License

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
