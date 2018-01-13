// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Graphics.Overlay;
using osu.Framework.Testing;
using osu.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Lovewing.Game.Tests.Visual
{
    internal class TestCaseToolbar : TestCase
    {
        public override IReadOnlyList<Type> RequiredTypes => new [] { typeof(LovewingToolbar) };

        public TestCaseToolbar()
        {
            Add(new LovewingToolbar
            {
                Origin = Anchor.TopRight,
                Anchor = Anchor.TopRight,
                RelativeSizeAxes = Axes.X,
                Height = 100
            });
        }
    }
}
