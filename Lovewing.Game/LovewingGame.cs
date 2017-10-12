// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Screens;

namespace Lovewing.Game
{
    public class LovewingGame : LovewingGameBase
    {
        private readonly MainScreen mainScreen = new MainScreen();

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Add(mainScreen);

            mainScreen.Exited += _ => Scheduler.AddDelayed(Exit, 2000);
        }
    }
}