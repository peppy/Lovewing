// Copyright (c) 2007-2017 Clara.
// Licensed under the MIT License

using Lovewing.Game.Screens.Main;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using System.Linq;

namespace Lovewing.Game.Screens
{
    public class MainScreen : LovewingScreen
    {
        private readonly Sprite background;
        private readonly Container<Wedge> wedgeContainer;

        public MainScreen()
        {
            Wedge home, management;
            Children = new Drawable[]{
                background = new Sprite
                {
                    FillMode = FillMode.Fill,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                wedgeContainer = new Container<Wedge>
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Wedge[]
                    {
                        home = new HomeWedge
                        {
                            Anchor = Anchor.BottomRight,
                            Origin = Anchor.BottomRight,
                            RelativeSizeAxes = Axes.Both,
                            Width = 0.5f,
                        },
                        management = new ManagementWedge
                        {
                            Anchor = Anchor.BottomRight,
                            Origin = Anchor.BottomRight,
                            RelativeSizeAxes = Axes.Both,
                            Width = 0.5f,
                            Margin = new MarginPadding { Right = 50 }
                        }
                    }
                },
            };

            home.StateChanged += selectWedge;
            management.StateChanged += selectWedge;

            Add(new Container
            {
                RelativeSizeAxes = Axes.Both,
                Children = new[]
                {
                    home.CreateButton(),
                    management.CreateButton(),
                },
            });
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore texStore)
        {
            background.Texture = texStore.Get(@"Backgrounds/mainmenu");
        }

        private void selectWedge(VisibilityContainer con, Visibility vis)
        {
            if(vis == Visibility.Visible)
            {
                wedgeContainer.Children.Where(child => child != con).ToList().ForEach(wedge => wedge.Hide());
            }
        }
    }
}
