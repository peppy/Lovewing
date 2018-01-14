// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Online;
using Lovewing.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Platform;
using osu.Framework.IO.Stores;
using OpenTK;

namespace Lovewing.Game
{
    public class LovewingGameBase : osu.Framework.Game
    {
        protected override string MainResourceFile => "Lovewing.Game.Resources.dll";

        private DependencyContainer dependencies;
        private Storage storage = new DesktopStorage("lovewing");

        protected override IReadOnlyDependencyContainer CreateLocalDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateLocalDependencies(parent));

        public DiscordRpc.EventHandlers discordHandlers;

        [BackgroundDependencyLoader]
        private void load()
        {
            DiscordRpc.Initialize("384734966690611202", ref discordHandlers, true, "");

            dependencies.Cache(this);
            dependencies.Cache(new LovewingColours());
            dependencies.Cache(new UserData());

            Fonts.AddStore(new GlyphStore(Resources, @"Fonts/FontAwesome"));
            Fonts.AddStore(new GlyphStore(Resources, @"Fonts/Muli_Light"));
            Fonts.AddStore(new GlyphStore(Resources, @"Fonts/Noto_Sans_CJK_JP_Regular")); // - it cant find the font for some reason

            var t = Audio.Track.Get(@"mainmenu_aqours");

            if (t == null) return;

            t.Looping = true;
            t.Volume.Set(0.5);
            t.Start();
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            var config = new FrameworkConfigManager(storage);

            config.Set(FrameworkSetting.WindowMode, WindowMode.Windowed);
            config.Set(FrameworkSetting.Height, 720);
            config.Set(FrameworkSetting.Width, 1280);

            Window.CursorState = CursorState.Hidden;
            Window.WindowBorder = WindowBorder.Fixed;

            Window.SetTitle(@"Lovewing");
            Window.SetupWindow(config);
        }
    }
}
