// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input;
using osu.Framework.Extensions.Color4Extensions;
using OpenTK;
using OpenTK.Graphics;

namespace Lovewing.Game.Graphics.UserInterface
{
    class LovewingButton : Button, IFilterable
    {
        private readonly Box hover;

        public Color4 HoverColour
        {
            get { return hover.Colour; }
            set { hover.FadeColour(value); }
        }

        public Color4 TextColour
        {
            get { return SpriteText.Colour; }
            set { SpriteText.FadeColour(value); }
        }

        public float TextY
        {
            get { return SpriteText.Y; }
            set { SpriteText.MoveToY(value); }
        }

        public LovewingButton(float textX = 0, float textY = 60, float textSize = 30)
        {
            Height = 40;
            Masking = true;
            CornerRadius = 5;
            AddInternal(new Drawable[]
            {
                Background = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                SpriteText = CreateText(),
                hover = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    BlendingMode = BlendingMode.Additive,
                    Colour = Color4.White.Opacity(0.1f),
                    Alpha = 0,
                },
            });

            SpriteText.X = textX;
            SpriteText.Y = textY;
            SpriteText.TextSize = textSize;
            SpriteText.Shadow = true;
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
            Content.ScaleTo(1.1f, 1000, EasingTypes.OutElastic);
            return base.OnMouseUp(state, args);
        }

        public string[] FilterTerms => new[] { Text };

        public bool MatchingFilter
        {
            set
            {
                FadeTo(value ? 1 : 0);
            }
        }
    }
}
