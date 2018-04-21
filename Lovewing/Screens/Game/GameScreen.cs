using Lovewing.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace Lovewing.Screens.Game
{
    public class GameScreen : Screen
    {
        protected override void LoadComplete()
        {
            AddRange(new Drawable[]
            {
                new Background(@"Backgrounds/game_default")
            });
        }
    }
}
