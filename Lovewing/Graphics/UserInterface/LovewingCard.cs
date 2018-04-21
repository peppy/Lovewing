using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace Lovewing.Graphics.UserInterface
{
    public class LovewingCard : BufferedContainer
    {
        public LovewingCard()
        {
            CornerRadius = 10;
            Masking = true;
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Radius = 10,
                Colour = ColourInfo.SingleColour(Color4.Black).MultiplyAlpha(0.2f)
            };
        }
    }
}
