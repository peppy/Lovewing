// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Graphics;
using OpenTK.Graphics;
using osu.Framework.Allocation;

namespace Lovewing.Game.Screens.Main
{
    public class HomeWedge : Wedge
    {
        private Color4 wedgeColour;
        private Color4 buttonColour;
        protected override Color4 WedgeColour => wedgeColour;
        protected override Color4 ButtonColour => buttonColour;
        protected override FontAwesome ButtonIcon => FontAwesome.fa_home;
        protected override string ButtonText => @"Home";

        [BackgroundDependencyLoader]
        private void load(LovewingColors colors)
        {
            wedgeColour = colors.Magenta;
            buttonColour = colors.LightMagenta;
        }
    }
}
