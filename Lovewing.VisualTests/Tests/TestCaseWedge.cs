// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Screens.Main;
using osu.Framework.Testing;
using OpenTK.Graphics;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using System.Linq;
using osu.Framework.Graphics.Shapes;
using OpenTK;

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
                Color4.Wheat,
                Color4.Aquamarine,
            };

            for(int i = colors.Count - 1; i >= 0; i--)
            {
                CustomWedge wedge = new CustomWedge(colors[i])
                {
                    Origin = Anchor.BottomRight,
                    Anchor = Anchor.BottomRight,
                    RelativeSizeAxes = Axes.Both,
                    Width = 0.5f,
                    Depth = i,
                    Margin = new MarginPadding { Right = i * 50 },
                };
                wedge.StateChanged += selectWedge;

                wedge.Add(new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.5f),
                    Colour = colors[i],
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                });

                Add(wedge);
                Add(wedge.CreateButton());
            }
        }

        private void selectWedge(VisibilityContainer con, Visibility vis)
        {
            if (vis == Visibility.Visible)
            {
                Children.Where(child => child != con).OfType<Wedge>().ToList().ForEach(wedge => wedge.Hide());
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
