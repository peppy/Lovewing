using System;
using System.Collections.Generic;
using System.Text;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Containers;
using OpenTK;
using OpenTK.Graphics;
using Lovewing.Gameplay;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input;

namespace Lovewing.Graphics.Gameplay
{
    class ActiveNote : CircularContainer
    {
        public const float Radius = 67.5f;
        private Vector2 target;

        public ActiveNote(Vector2 t)
        {
            target = t;

            Masking = true;
            Origin = Anchor.Centre;
            Height = Radius * 2;
            Width = Radius * 2;
            Colour = Color4.White;
            BorderColour = Color4.White;
            BorderThickness = 5.0f;
            Child = new Box
            {
                Colour = Color4.Transparent,
                RelativeSizeAxes = Axes.Both
            };
        }

        protected override bool OnMouseDown(InputState state, MouseDownEventArgs args)
        {
            Vector2 absTarget = Parent.RelativeToAbsoluteFactor * target;
            Vector2 absPos = Parent.RelativeToAbsoluteFactor * Position;
            double distance = Vector2.Distance(absTarget, absPos);
            if (distance <= Radius * 2.0) {
                // Currently within scoring range
                double score = 1.0 - (distance / (Radius * 2.0));

                // Add code here to apply score

                Container parent = Parent as Container;
                SpriteText scoreText = new SpriteText
                {
                    Font = @"Noto Sans CJK JP Regular",
                    TextSize = 32,
                    Colour = Color4.White,
                    Alpha = 1.0f,
                    Text = "+" + Math.Floor(score * 100) + "!"
                };
                scoreText.Position = absPos;
                parent.Add(scoreText);
                scoreText.MoveToOffset(new Vector2(0.0f, -100.0f), 1000, Easing.OutExpo).OnComplete(text => (text.Parent as Container).Remove(text));
                scoreText.FadeOut(2000, Easing.OutExpo);
                (Parent as Container).Remove(this);
            }

            return base.OnMouseDown(state, args);
        }
    }
}
