// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Graphics.UserInterface;
using osu.Framework.Screens;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using OpenTK;
using OpenTK.Graphics;

namespace Lovewing.Game.Screens
{
    public class MatchmakingScreen : Screen
    {

        public MatchmakingScreen()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour  = new Color4(54, 54, 54, 255)
                },
                new Toolbar(),
                new LovewingButton
                {
                    BorderThickness = 5,
                    BackgroundColour = Color4.Transparent,
                    BorderColour = Color4.White,
                    CornerRadius = 75,
                    Size = new Vector2(150, 150),
                    Action = Exit
                }
            };
        }

        protected override void OnEntering(Screen last)
        {
            Content.FadeOut()
            .FadeIn(400);
            base.OnEntering(last);
        }

        protected override bool OnExiting(Screen next)
        {
            Content.FadeOut(200);
            return base.OnExiting(next);
        }
    }
}
