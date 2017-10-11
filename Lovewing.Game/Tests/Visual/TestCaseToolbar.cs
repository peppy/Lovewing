// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Graphics.UserInterface;
using osu.Framework.Testing;
using osu.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Lovewing.Game.Tests.Visual
{
    internal class TestCaseToolbar : TestCase
    {
        public override IReadOnlyList<Type> RequiredTypes => new [] { typeof(Toolbar) };

        public TestCaseToolbar()
        {
            Add(new Toolbar
            {
                Origin = Anchor.TopRight,
                Anchor = Anchor.TopRight,
                RelativeSizeAxes = Axes.X,
                Height = 100,
            });
        }
    }
}
