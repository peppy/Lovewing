// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Graphics;
using Lovewing.Game.Graphics.UserInterface;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using OpenTK;
using OpenTK.Graphics;

namespace Lovewing.Game.Screens
{
    public class MatchmakingScreen : LovewingScreen
    {

        public MatchmakingScreen()
        {
            AddRange(new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour  = new Color4(54, 54, 54, 255)
                },
                new IconButton
                {
                    Padding = new MarginPadding(30),
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    Colour = Color4.White,
                    Size = new Vector2(110, 130),
                    Action = Exit,
                    Icon = FontAwesome.fa_chevron_left,
                }
            });
        }
    }
}
