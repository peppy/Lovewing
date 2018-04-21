using System;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Platform;
using OpenTK.Graphics;

namespace Lovewing.Game.Graphics.Game
{
    public class SongScore : Container
    {
        private Box progressBox;

        public double Score
        {
            get => Math.Floor(progressBox.Width / ((host.Window.Width - 100.0) / 100));
            set => progressBox.Width = (float) ((host.Window.Width - 100.0) / 100 * value);
        }

        private GameHost host;

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            this.host = host;
            Height = 10;
            Width = host.Window.Width - 100;
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
                    Width = host.Window.Width - 100,
                    Colour = Color4.LightSkyBlue
                }
            });
        }
    }
}
