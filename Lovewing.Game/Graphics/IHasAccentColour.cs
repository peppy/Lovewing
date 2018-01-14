// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Transforms;

namespace Lovewing.Game.Graphics
{
    public interface IHasAccentColour : IDrawable
    {
        Color4 AccentColour { get; set; }
    }

    public static class AccentedColourExtensions
    {
        public static TransformSequence<T> FadeAccent<T>(this T accentedDrawable, Color4 newColour, double duration = 0, Easing easing = Easing.None)
            where T : IHasAccentColour
            => accentedDrawable.TransformTo(nameof(accentedDrawable.AccentColour), newColour, duration, easing);

        public static TransformSequence<T> FadeAccent<T>(this TransformSequence<T> t, Color4 newColour, double duration = 0, Easing easing = Easing.None)
            where T : Drawable, IHasAccentColour
            => t.Append(o => o.FadeAccent(newColour, duration, easing));
    }
}
