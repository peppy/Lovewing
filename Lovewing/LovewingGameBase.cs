using OpenTK;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;

namespace Lovewing
{
    public class LovewingGameBase : Game
    {
        protected override string MainResourceFile => "Lovewing.Game.Resources.dll";

        private DependencyContainer dependencies;
        private readonly Storage storage = new DesktopStorage("lovewing");

        protected override IReadOnlyDependencyContainer CreateLocalDependencies(IReadOnlyDependencyContainer parent) => 
            dependencies = new DependencyContainer(base.CreateLocalDependencies(parent));

        public LovewingGameBase()
        {
            Name = @"Project Lovewing";
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            dependencies.Cache(this);
            dependencies.Cache(storage);

            Fonts.AddStore(new GlyphStore(Resources, @"Fonts/FontAwesome"));
            Fonts.AddStore(new GlyphStore(Resources, @"Fonts/Muli_Light"));
            // Fonts.AddStore(new GlyphStore(Resources, @"Fonts/Noto_Sans_CJK_JP_Regular"));
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
            Window.Title = @"Project Lovewing";

            Window.SetupWindow(config);
        }
    }
}
