// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Graphics.UserInterface;
using osu.Framework.Screens;
using osu.Framework.Graphics;
using OpenTK.Graphics;

namespace Lovewing.Game.Screens
{
    class MatchmakingScreen : Screen
    {

        public MatchmakingScreen()
        {
            Colour = new Color4(54, 54, 54, 255);

            Children = new Drawable[]
            {
                new Toolbar(),
            };
        }

        protected override void OnEntering(Screen last)
        {
            Content.FadeOut();
            Content.FadeIn(2000);
            base.OnEntering(last);
        }

        protected override bool OnExiting(Screen next)
        {
            Content.FadeOut(2000);
            return base.OnExiting(next);
        }
    }
}
