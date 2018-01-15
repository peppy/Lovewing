using Lovewing.Game.Graphics;
using Lovewing.Game.Graphics.Game;
using Lovewing.Game.Graphics.Overlay;
using Lovewing.Game.Online;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;
using osu.Framework.Screens;
using OpenTK;
using OpenTK.Input;

namespace Lovewing.Game.Screens.Game
{
    public class GameScreen : Screen
    {
        private readonly Background bg;
        private readonly PauseOverlay pauseOverlay;
        private readonly SongScore songScore;

        private readonly HitCircle circle1;
        private readonly HitCircle circle2;
        private readonly HitCircle circle3;
        private readonly HitCircle circle4;
        private readonly HitCircle circle5;
        private readonly HitCircle circle6;
        private readonly HitCircle circle7;
        private readonly HitCircle circle8;
        private readonly HitCircle circle9;

        public GameScreen()
        {
            AddRange(new Drawable[]
            {
                pauseOverlay = new PauseOverlay
                {
                    OnContinue = () =>
                    {
                        pauseOverlay.Hide();

                        // TODO: add countdown timer
                    },
                    OnRestart = () =>
                    {
                        // TODO
                    },
                    OnStop = Exit
                },
                bg = new Background(@"Backgrounds/game_default")
                {
                    FillMode = FillMode.Fill,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Children = new Drawable[]
                    {
                        songScore = new SongScore
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Margin = new MarginPadding
                            {
                                Top = 100
                            },
                            Score = 25
                        },
                        new MiddleCircle
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding
                            {
                                Bottom = 420
                            }
                        },
                        circle1 = new HitCircle
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding
                            {
                                Right = 850,
                                Bottom = 410
                            }
                        },
                        circle2 = new HitCircle
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding
                            {
                                Right = 760,
                                Bottom = 260
                            }
                        },
                        circle3 = new HitCircle
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding
                            {
                                Right = 545,
                                Bottom = 135
                            }
                        },
                        circle4 = new HitCircle
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding
                            {
                                Bottom = 35,
                                Right = 300
                            }
                        },
                        circle5 = new HitCircle
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding
                            {
                                Bottom = 10
                            }
                        },
                        circle6 = new HitCircle
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding
                            {
                                Bottom = 35,
                                Left = 300
                            }
                        },
                        circle7 = new HitCircle
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding
                            {
                                Left = 545,
                                Bottom = 135
                            }
                        },
                        circle8 = new HitCircle
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding
                            {
                                Left = 760,
                                Bottom = 260
                            }
                        },
                        circle9 = new HitCircle
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding
                            {
                                Left = 850,
                                Bottom = 410
                            }
                        }
                    }
                }
            });
        }

        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            switch (args.Key)
            {
                case Key.Number3:
                {
                    circle1.TriggerOnClick();
                    break;
                }

                case Key.E:
                {
                    circle2.TriggerOnClick();
                    break;
                }

                case Key.D:
                {
                    circle3.TriggerOnClick();
                    break;
                }

                case Key.C:
                {
                    circle4.TriggerOnClick();
                    break;
                }

                case Key.V:
                {
                    circle5.TriggerOnClick();
                    break;
                }

                case Key.B:
                {
                    circle6.TriggerOnClick();
                    break;
                }

                case Key.H:
                {
                    circle7.TriggerOnClick();
                    break;
                }

                case Key.U:
                {
                    circle8.TriggerOnClick();
                    break;
                }

                case Key.Number7:
                {
                    circle9.TriggerOnClick();
                    break;
                }

                case Key.Escape:
                {
                    pauseOverlay.Show();
                    break;
                }
            }

            return base.OnKeyDown(state, args);
        }

        protected override void LoadComplete()
        {
            var presence = new DiscordRpc.RichPresence
            {
                details = "Test Game",
                state = "In-Game",
                largeImageKey = "logo"
            };

            DiscordRpc.UpdatePresence(ref presence);

            bg.BlurTo(new Vector2(10));
        }
    }
}
