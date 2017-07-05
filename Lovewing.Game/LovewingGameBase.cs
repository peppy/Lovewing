// Copyright (c) 2007-2017 Clara.
// Licensed under the MIT License

using Lovewing.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Platform;

namespace Lovewing.Game
{
    public class LovewingGameBase : osu.Framework.Game
    {
        protected override string MainResourceFile => "Lovewing.Game.exe";

        [BackgroundDependencyLoader]
        private void load()
        {
            Dependencies.Cache(new LovewingColors());
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            Window.SetTitle(@"Lovewing");
        }
    }
}
