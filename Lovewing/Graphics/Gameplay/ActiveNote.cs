using System;
using System.Collections.Generic;
using System.Text;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Containers;
using OpenTK;
using OpenTK.Graphics;
using Lovewing.Gameplay;
using osu.Framework.MathUtils;
using osu.Framework.Input;

namespace Lovewing.Graphics.Gameplay
{
    class ActiveNote : CircularContainer
    {
        public const float Radius = 67.5f;

        public ActiveNote()
        {
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
            Console.WriteLine("clicc");
            (Parent as Container).Remove(this);
            return base.OnMouseDown(state, args);
        }
    }
}
