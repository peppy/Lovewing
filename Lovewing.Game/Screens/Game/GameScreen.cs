using Lovewing.Game.Graphics;
using Lovewing.Game.Graphics.Game;
using Lovewing.Game.Online;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input;
using osu.Framework.Screens;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

namespace Lovewing.Game.Screens.Game
{
    public class GameScreen : Screen
    {
        private HitCircle circle1;
        private HitCircle circle2;
        private HitCircle circle3;
        private HitCircle circle4;
        private HitCircle circle5;
        private HitCircle circle6;
        private HitCircle circle7;
        private HitCircle circle8;
        private HitCircle circle9;

        public GameScreen()
        {
            AddRange(new Drawable[]
            {
                new Background(@"Backgrounds/mainmenu")
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
                        circle1 = new HitCircle
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding
                            {
                                Right = 850,
                                Bottom = 500
                            }
                        },
                        circle2 = new HitCircle
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding
                            {
                                Right = 750,
                                Bottom = 350
                            }
                        },
                        circle3 = new HitCircle
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding
                            {
                                Right = 545,
                                Bottom = 225
                            }
                        },
                        circle4 = new HitCircle
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding
                            {
                                Bottom = 125,
                                Right = 300
                            }
                        },
                        circle5 = new HitCircle
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding
                            {
                                Bottom = 100
                            }
                        },
                        circle6 = new HitCircle
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding
                            {
                                Bottom = 125,
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
                                Bottom = 225
                            }
                        },
                        circle8 = new HitCircle
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding
                            {
                                Left = 750,
                                Bottom = 350
                            }
                        },
                        circle9 = new HitCircle
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding
                            {
                                Left = 850,
                                Bottom = 500
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
        }
    }
}
