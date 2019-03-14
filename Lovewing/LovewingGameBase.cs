using System.Drawing;
using osuTK;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;

namespace Lovewing
{
    public class LovewingGameBase : Game
    {
        private DependencyContainer dependencies;
        private Storage storage;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        public LovewingGameBase()
        {
            Name = @"Project Lovewing";
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new DllResourceStore(@"Lovewing.Game.Resources.dll"));

            dependencies.Cache(this);
            dependencies.Cache(storage);

            //Fonts.AddStore(new GlyphStore(Resources, @"Fonts/FontAwesome"));
            //Fonts.AddStore(new GlyphStore(Resources, @"Fonts/Muli_Light"));
            // Fonts.AddStore(new GlyphStore(Resources, @"Fonts/Noto_Sans_CJK_JP_Regular"));
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            storage = new DesktopStorage("lovewing", Host);
            var config = new FrameworkConfigManager(storage);

            config.Set(FrameworkSetting.WindowMode, WindowMode.Windowed);
            config.Set(FrameworkSetting.WindowedSize, new Size(1280, 720));

            Window.CursorState = CursorState.Hidden;
            Window.WindowBorder = WindowBorder.Fixed;
            Window.Title = @"Project Lovewing";

            Window.SetupWindow(config);
        }
    }
}
