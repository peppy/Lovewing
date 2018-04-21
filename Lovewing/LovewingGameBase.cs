using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.IO.Stores;

namespace Lovewing
{
    public class LovewingGameBase : Game
    {
        protected override string MainResourceFile => "Lovewing.Game.Resources.dll";

        private DependencyContainer dependencies;

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

            Fonts.AddStore(new GlyphStore(Resources, @"Fonts/FontAwesome"));
            Fonts.AddStore(new GlyphStore(Resources, @"Fonts/Muli_Light"));
            // Fonts.AddStore(new GlyphStore(Resources, @"Fonts/Noto_Sans_CJK_JP_Regular"));
        }
    }
}
