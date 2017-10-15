// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using System.Collections.Generic;
using Lovewing.Game.Online.User;

namespace Lovewing.Game.Online
{
    class UserData
    {
        public string Username { get; set; } = @"Guest";
        public string Token { get; set; } = @"secret.guest";
        public string Avatar { get; set; } = @"https://owo.whats-th.is/455c65.png";
        public string UserBackground { get; set; } = @"https://cat.girlsare.life/49e126.png";
        public string Idol { get; set; } = @"Idols/kotori";
        public List<Badge> Badges { get; set; } = new List<Badge>();
        public int Loveca { get; set; } = 0;
        public int Coins { get; set; } = 0;
        public int Lives { get; set; } = 0;
        public int Level { get; set; } = 1;
        public float XP { get; set; } = 0;
    }
}
