// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;
using osu.Framework.Extensions.Color4Extensions;
using OpenTK;
using OpenTK.Graphics;
using System.Collections.Generic;

namespace Lovewing.Game.Graphics.UserInterface
{
    public class LovewingDoubleButton : Button, IFilterable
    {
        private readonly Box hover;
        private readonly LovewingColors colors = new LovewingColors();

        public LovewingDoubleButton(float rotation = -0.7f, float x = 200, float textX = -200, float textSize = 40)
        {
            Height = 40;
            Masking = true;
            CornerRadius = 5;
            SpriteText.X = textX;
            SpriteText.Y = 50f;
            SpriteText.TextSize = textSize;
            SpriteText.Shadow = true;
            BackgroundColour = colors.Magenta;

            AddRange(new Drawable[]
            {
                hover = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.White.Opacity(0.1f),
                    Alpha = 0,
                },
                new Box
                {
                    X = x,
                    RelativeSizeAxes = Axes.Both,
                    Origin = Anchor.CentreRight,
                    Anchor = Anchor.CentreRight,
                    Colour = colors.LightMagenta,
                    Shear = new Vector2(rotation, 0),
                    Alpha = 0.3f,
                },
            });
        }

        protected override bool OnClick(InputState state)
        {
            return base.OnClick(state);
        }

        protected override bool OnHover(InputState state)
        {
            hover.FadeIn(200);
            return base.OnHover(state);
        }

        protected override void OnHoverLost(InputState state)
        {
            hover.FadeOut(200);
            base.OnHoverLost(state);
        }

        protected override bool OnMouseUp(InputState state, MouseUpEventArgs args)
        {
            Content.ScaleTo(1, 1000, Easing.OutElastic);
            return base.OnMouseUp(state, args);
        }

        public IEnumerable<string> FilterTerms => new[] { Text };

        public bool MatchingFilter
        {
            set
            {
                this.FadeTo(value ? 1 : 0);
            }
        }
    }
}
