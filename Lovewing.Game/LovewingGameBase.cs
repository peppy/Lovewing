// Copyright (c) 2007-2017 Clara.
// Licensed under the MIT License

using Lovewing.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Platform;
using osu.Framework.IO.Stores;

namespace Lovewing.Game
{
    public class LovewingGameBase : osu.Framework.Game
    {
        protected override string MainResourceFile => "Lovewing.Game.exe";

        [BackgroundDependencyLoader]
        private void load()
        {
            Dependencies.Cache(new LovewingColors());

            Fonts.AddStore(new GlyphStore(Resources, @"Fonts/FontAwesome"));

            var t = Audio.Track.Get(@"mainmenu_muse");

            t?.Start();
            t.Looping = true;
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            Window.SetTitle(@"Lovewing");
        }
    }
}
