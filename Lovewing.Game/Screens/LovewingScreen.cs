// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Graphics.Overlay;
using osu.Framework.Screens;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;

namespace Lovewing.Game.Screens
{
    /// <summary>
    /// This class should be used to add general properties and methods
    /// </summary>
    public class LovewingScreen : Screen
    {
        protected readonly LovewingSidebar Sidebar;
        protected readonly LovewingToolbar Toolbar;

        protected void ToggleSideBar()
        {
            if (Sidebar.State == Visibility.Visible)
                Sidebar.Hide();
            else
                Sidebar.Show();
        }

        public LovewingScreen()
        {
            AddRangeInternal(new Drawable[]
            {
                Sidebar = new LovewingSidebar
                {
                    Origin = Anchor.CentreRight,
                    Anchor = Anchor.CentreRight,
                    RelativeSizeAxes = Axes.Y,
                    Depth = -2,
                },
                Toolbar = new LovewingToolbar
                {
                    Depth = -1,
                    ButtonAction = ToggleSideBar,
                },
            });
        }

        protected override void OnEntering(Screen last)
        {
            Content.FadeInFromZero(400);
            base.OnEntering(last);
        }

        protected override bool OnExiting(Screen next)
        {
            Content.FadeOutFromOne(400);
            return base.OnExiting(next);
        }

        protected override bool OnClick(InputState state)
        {
            if (Sidebar.State == Visibility.Visible)
                Sidebar.Hide();

            return base.OnClick(state);
        }
    }
}
