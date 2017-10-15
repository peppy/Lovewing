// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Graphics;
using OpenTK.Graphics;
using osu.Framework.Allocation;

namespace Lovewing.Game.Screens.Main
{
    public class IdolManagementWedge : Wedge
    {
        private Color4 wedgeColour;
        private Color4 buttonColour;
        protected override Color4 WedgeColour => wedgeColour;
        protected override Color4 ButtonColour => buttonColour;
        protected override FontAwesome ButtonIcon => FontAwesome.fa_group; //The idols icon needs to be added to the fonts
        protected override string ButtonText => @"Idols";

        [BackgroundDependencyLoader]
        private void load(LovewingColors colors)
        {
            wedgeColour = colors.LightYellow;
            buttonColour = colors.Yellow;
        }
    }
}
