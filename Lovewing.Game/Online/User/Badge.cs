// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Graphics;
using OpenTK.Graphics;

namespace Lovewing.Game.Online.User
{
    class Badge
    {
        public string Name { get; set; } = @"Badge";
        public string Description { get; set; } = @"This is a badge";
        public FontAwesome Icon { get; set; } = FontAwesome.fa_circle;
        public Color4 Colour { get; set; } = Color4.White;
    }
}
