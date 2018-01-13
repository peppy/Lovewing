using Lovewing.Game.Graphics.Game;
using osu.Framework.Graphics;
using osu.Framework.Testing;

namespace Lovewing.Game.Tests.Visual
{
    public class TestCaseHitCircle : TestCase
    {
        public TestCaseHitCircle()
        {
            AddInternal(new HitCircle
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Height = 128
            });
        }
    }
}
