using System;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Platform;
using OpenTK.Graphics;

namespace Lovewing.Game.Graphics.Game
{
    public class SongProgress : Container
    {
        private readonly Box progressBox;

        public double Progress
        {
            get => Math.Floor(progressBox.Width / ((GameHost.Instance.Window.Width - 100.0) / 100));
            set => progressBox.Width = (float) ((GameHost.Instance.Window.Width - 100.0) / 100 * value);
        }

        public SongProgress()
        {
            Height = 10;
            Width = GameHost.Instance.Window.Width - 100;
            Masking = true;
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Glow,
                Radius = 20,
                Colour = Color4.Blue.Opacity(0.1f)
            };

            AddRangeInternal(new Drawable[]
            {
                new Box
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.LightGray
                },
                progressBox = new Box
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    RelativeSizeAxes = Axes.Y,
                    Width = GameHost.Instance.Window.Width - 100,
                    Colour = Color4.LightSkyBlue
                }
            });
        }
    }
}
