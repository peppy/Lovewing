// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using OpenTK.Graphics;

namespace Lovewing.Game.Screens.Liveshow.Matchmaking
{
    public class LobbyCard : ClickableContainer
    {
        public string LobbyName { get; set; }
        public string LobbyDescription { get; set; }
        public LobbyType Type { get; set; }
        public int MaxPlayers { get; set; }
        public int Players { get; set; }

        public LobbyCard()
        {
            Height = 300;
            Width = 400;
            Colour = Color4.White;

            Children = new Drawable[]
            {
                new Box
                {
                    Colour = Color4.White,
                    RelativeSizeAxes = Axes.Both,
                    Depth = 2,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre
                },
                new Box
                {
                    Colour = Color4.Blue,
                    RelativeSizeAxes = Axes.X,
                    Height = 100,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre
                }
            };
        }
    }
}
