// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;
using osu.Framework.Extensions.Color4Extensions;
using OpenTK;
using OpenTK.Graphics;
using System.Collections.Generic;
using System;

namespace Lovewing.Game.Graphics.UserInterface
{
    public class LovewingButton : Button, IFilterable
    {
        private readonly Box hover;
        private readonly SpriteIcon icon;

        public Color4 HoverColour
        {
            get { return hover.Colour; }
            set { hover.Colour = value.Opacity(0.5f); }
        }

        public Color4 TextColour
        {
            get { return SpriteText.Colour; }
            set { SpriteText.FadeColour(value); }
        }

        public float TextX
        {
            get { return SpriteText.X; }
            set { SpriteText.MoveToX(value); }
        }

        public float TextY
        {
            get { return SpriteText.Y; }
            set { SpriteText.MoveToY(value); }
        }

        public float TextSize
        {
            get { return SpriteText.TextSize; }
            set { SpriteText.TextSize = value; }
        }

        public FontAwesome Icon
        {
            get { return icon.Icon; }
            set { icon.Icon = value; }
        }

        public Color4 IconColour
        {
            get { return icon.Colour; }
            set { icon.Colour = value; }
        }

        public float IconX
        {
            get { return icon.X; }
            set { icon.MoveToX(value); }
        }

        public float IconY
        {
            get { return icon.Y; }
            set { icon.MoveToY(value); }
        }

        public Vector2 IconSize
        {
            get { return icon.Size; }
            set { icon.Size = value; }
        }

        public LovewingButton()
        {
            Height = 40;
            Masking = true;
            CornerRadius = 5;

            AddRangeInternal(new Drawable[]
            {
                icon = new SpriteIcon
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                hover = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.White.Opacity(0.5f),
                    Alpha = 0,
                }
            });

            SpriteText.Shadow = true;
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

        protected override bool OnClick(InputState state)
        {

            var x = state.Mouse.Position.X;
            var y = state.Mouse.Position.Y;
            Circle ripple;

            Add(ripple = new Circle
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                X = x,
                Y = y,
                Height = 10,
                Width = 10,
                Colour = Color4.Gray,
                Blending = BlendingMode.Additive
            });

            ripple
                .ScaleTo(Math.Max(Size.X, Size.Y) / 5, 450, Easing.OutCirc)
                .FadeOut(450)
                .Expire();

            return base.OnClick(state);
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
