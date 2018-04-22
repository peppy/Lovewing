using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;

namespace Lovewing.Graphics.UserInterface
{
    public class LovewingTextBox : TextBox
    {
        public Color4 CommitColour { get; set; }
        public Color4 FocusedColour { get; set; }
        public Color4 UnfocusedColour { get; set; }
        public Color4 PlaceholderColour { get; set; }
        public Color4 TextColour { get; set; }

        protected override float LeftRightPadding => 10;

        protected override Color4 BackgroundCommit => CommitColour;
        protected override Color4 BackgroundFocused => FocusedColour;
        protected override Color4 BackgroundUnfocused => UnfocusedColour;

        protected override SpriteText CreatePlaceholder() => new SpriteText
        {
            Colour = PlaceholderColour,
            Margin = new MarginPadding { Left = 2 }
        };

        public LovewingTextBox()
        {
            Height = 40;
            TextContainer.Height = 0.5f;

            Current.DisabledChanged += disabled => Alpha = disabled ? 0.3f : 1;
        }

        protected override Drawable GetDrawableCharacter(char c) => new SpriteText
        {
            Colour = TextColour,
            Text = c.ToString(),
            TextSize = CalculatedTextSize
        };
    }
}
