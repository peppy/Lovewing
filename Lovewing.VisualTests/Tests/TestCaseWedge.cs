// Copyright (c) 2007-2017 Clara.
// Licensed under the MIT License

using Lovewing.Game.Screens.Main;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Testing;

namespace Lovewing.VisualTests.Tests
{
    class TestCaseWedge : TestCase
    {
        public override void Reset()
        {
            base.Reset();
            Wedge wedge = new HomeWedge { RelativeSizeAxes = Axes.Both };
            Add(wedge);

            AddToggleStep("Toogle Visibility", boolean => wedge.State = boolean ? Visibility.Visible : Visibility.Hidden);
        }
    }
}
