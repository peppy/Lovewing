using Lovewing.Beatmaps;
using Lovewing.Graphics;
using Lovewing.Graphics.Containers;
using Lovewing.Graphics.UserInterface;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input;
using osu.Framework.Screens;
using System.Collections.Generic;

namespace Lovewing.Screens.Game
{
    public class SongSelectorScreen : Screen
    {
        private Bindable<Beatmap> selected = new Bindable<Beatmap>();
        private Bindable<List<Beatmap>> beatmaps = new Bindable<List<Beatmap>>(new List<Beatmap>());

        private SpriteText titleText;
        private SpriteText artistText;
        private FillFlowContainer beatmapContainer;

        public SongSelectorScreen()
        {
            beatmaps.Value.AddRange(new Beatmap[]
            {
                new Beatmap
                {
                    Title = @"Questions",
                    Artists = { @"Tristam" }
                },
                new Beatmap
                {
                    Title = @"Snow Halation",
                    Artists = { @"Muse" }
                },
                new Beatmap
                {
                    Title = @"Moving Hectic",
                    Artists = { @"Slippy", @"Harry Shota" }
                },
                new Beatmap
                {
                    Title = @"Broken Bones",
                    Artists = { @"Pixl", @"Cassandra Kay" }
                }
            });

            selected.Value = beatmaps.Value[0];
        }

        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            var maps = beatmaps.Value;

            int i;

            switch (args.Key)
            {
                case Key.Left:
                    i = maps.IndexOf(selected) - 1;

                    if (i > -1)
                        selected.Value = maps[i];

                    return true;

                case Key.Right:
                    i = maps.IndexOf(selected) + 1;

                    if (i < maps.Count)
                        selected.Value = maps[i];

                    return true;

                default:
                    return base.OnKeyDown(state, args);
            }
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            selected.ValueChanged += val =>
            {
                titleText.Text = val.Title;
                artistText.Text = string.Join(", ", val.Artists);
            };

            AddRange(new Drawable[]
            {
                new Background(@"https://cdn.discordapp.com/attachments/251718122447437824/403549744951066636/bg2.jpg"),
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.X,
                    Height = 500,
                    Children = new Drawable[]
                    {
                        // TODO: Add carousel
                    }
                },
                new Container
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.X,
                    Height = 200,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Colour = ColourInfo.SingleColour(Color4.Black).MultiplyAlpha(0.5f),
                            RelativeSizeAxes = Axes.Both
                        },
                        titleText = new SpriteText
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Margin = new MarginPadding { Top = 20 },
                            Text = selected.Value.Title,
                            TextSize = 40,
                            AllowMultiline = false
                        },
                        artistText = new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Margin = new MarginPadding { Bottom = 20 },
                            Text = string.Join(", ", selected.Value.Artists),
                            AllowMultiline = false
                        },
                        new LovewingButton
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding { Bottom = 20 },
                            CornerRadius = 0,
                            Text = @"M.U.S.I.C. START!",
                            TextSize = 20,
                            BackgroundColour = LovewingColours.Blue,
                            Height = 50,
                            Width = 200
                        }
                    }
                }
            });

            base.LoadComplete();
        }
    }
}
