using System;
using Lovewing.Game.Graphics.UserInterface;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using OpenTK.Graphics;

namespace Lovewing.Game.Graphics.Overlay
{
    public class PauseOverlay : OverlayContainer
    {
        private readonly LovewingButton continueButton;
        private readonly LovewingButton restartButton;
        private readonly LovewingButton stopButton;

        public Action OnContinue
        {
            get => continueButton.Action;
            set => continueButton.Action = value;
        }

        public Action OnRestart
        {
            get => restartButton.Action;
            set => restartButton.Action = value;
        }

        public Action OnStop
        {
            get => stopButton.Action;
            set => stopButton.Action = value;
        }

        public PauseOverlay()
        {
            RelativeSizeAxes = Axes.Both;
            Depth = -1;

            AddRangeInternal(new Drawable[]
            {
                new Box
                {
                    Colour = new Color4(0, 0, 0, 100),
                    RelativeSizeAxes = Axes.Both,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre
                },
                continueButton = new LovewingButton
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BackgroundColour = Color4.Green,
                    Height = 120,
                    Width = 420,
                    Text = "Continue",
                    Margin = new MarginPadding
                    {
                        Bottom = 250
                    }
                },
                restartButton = new LovewingButton
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BackgroundColour = Color4.Orange,
                    Height = 120,
                    Width = 420,
                    Text = "Restart"
                },
                stopButton = new LovewingButton
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BackgroundColour = Color4.Red,
                    Height = 120,
                    Width = 420,
                    Text = "Stop",
                    Margin = new MarginPadding
                    {
                        Top = 250
                    }
                }
            });
        }

        protected override void PopIn()
        {
            Content.FadeInFromZero(250);
        }

        protected override void PopOut()
        {
            Content.FadeOutFromOne(250);
        }
    }
}
