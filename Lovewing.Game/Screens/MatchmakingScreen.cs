using Lovewing.Game.Graphics;
using osu.Framework.Screens;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using OpenTK;
using OpenTK.Graphics;

namespace Lovewing.Game.Screens
{
    class MatchmakingScreen : Screen
    {
        private readonly LovewingColors colors = new LovewingColors();

        public MatchmakingScreen()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = new Color4(54, 54, 54, 255),
                }
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
