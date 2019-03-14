using Lovewing.Graphics.Cursor;
using Lovewing.Graphics.Overlays;
using Lovewing.Screens;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace Lovewing
{
    public class LovewingGame : LovewingGameBase
    {
        private MainScreen mainScreen;
        public Toolbar Toolbar;
        public Sidebar Sidebar;

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Child = new LovewingCursor();

            AddRange(new Drawable[]
            {
                new ScreenStack(mainScreen = new MainScreen())
                {
                    RelativeSizeAxes = Axes.Both,
                },
                Sidebar = new Sidebar(),
                Toolbar = new Toolbar
                {
                    ButtonAction = () => Sidebar.Toggle()
                }
            });
        }
    }
}
