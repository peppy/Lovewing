// Copyright (c) 2007-2017 Clara.
// Licensed under the MIT License

using Lovewing.Game.Screens.Main;
using osu.Framework.Testing;
using OpenTK.Graphics;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Lovewing.VisualTests.Tests
{
    internal class TestCaseWedge : TestCase
    {
        public override void Reset()
        {
            base.Reset();

            List<Color4> colors = new List<Color4>
            {
                Color4.Red,
                Color4.Green,
                Color4.Blue,
            };

            for(int i = 0; i < colors.Count; i++)
            {
                Add(new CustomWedge(colors[i])
                {
                    RelativeSizeAxes = Axes.Both,
                    X = -50 * i,
                });
            }
        }

        private class CustomWedge : Wedge
        {
            protected override Color4 WedgeColor { get; } = Color4.Wheat;

            public CustomWedge(Color4 color)
            {
                WedgeColor = color;
            }
        }
    }
}
