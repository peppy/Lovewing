// Copyright (c) 2007-2017 Clara.
// Licensed under the MIT License

using osu.Framework.Desktop;
using osu.Framework.Platform;
using System;

namespace Lovewing.Tests
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            using (GameHost host = Host.GetSuitableHost(@"lovewing visual-tests"))
            {
                host.Run(new LovewingTests());
            }
        }
    }
}
