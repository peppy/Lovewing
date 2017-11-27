// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Online;
using Lovewing.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Platform;
using osu.Framework.IO.Stores;

namespace Lovewing.Game
{
    public class LovewingGameBase : osu.Framework.Game
    {
        protected override string MainResourceFile => "Lovewing.Game.Resources.dll";

        private DependencyContainer dependencies;

        private DiscordRpc.RichPresence defaultPresence = new DiscordRpc.RichPresence
        {
            state = "Idle",
            details = "Main Menu"
        };

        protected override IReadOnlyDependencyContainer CreateLocalDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateLocalDependencies(parent));

        public DiscordRpc.EventHandlers discordHandlers;

        [BackgroundDependencyLoader]
        private void load()
        {
            DiscordRpc.Initialize("384734966690611202", ref discordHandlers, true, "");

            DiscordRpc.UpdatePresence(ref defaultPresence);

            dependencies.Cache(this);
            dependencies.Cache(new LovewingColours());
            dependencies.Cache(new UserData());

            Fonts.AddStore(new GlyphStore(Resources, @"Fonts/FontAwesome"));
            Fonts.AddStore(new GlyphStore(Resources, @"Fonts/Muli_Light"));
            // Fonts.AddStore(new GlyphStore(Resources, @"Fonts/Noto_Sans_CJK_JP_Regular")); - it cant find the font for some reason

            var t = Audio.Track.Get(@"mainmenu_muse");

            if (t == null) return;

            t.Looping = true;
            t.Volume.Set(0);
            t.Start();
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            Window.SetTitle(@"Lovewing");
        }
    }
}
