using Lovewing.Graphics.Overlays;
using Lovewing.Screens;
using Lovewing.Screens.Game;
using osu.Framework.Graphics;
using osu.Framework.Input;

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

            AddRange(new Drawable[]
            {
                mainScreen = new MainScreen(),
                Sidebar = new Sidebar(),
                Toolbar = new Toolbar
                {
                    ButtonAction = () => Sidebar.Toggle()
                }
            });
        }
    }
}
