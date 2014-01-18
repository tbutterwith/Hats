using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Hats
{
    class ResetGameState : GameState
    {
        Map map;
        Player player1;
        Player player2;

        public ResetGameState(Game1 game) : base(game)
        {
            map = game.getMap();
            player1 = game.getPlayer1();
            player2 = game.getPlayer2();

        }

        public override void LoadContent(ContentManager cManager)
        {

        }

        /// <summary>
        /// update function for the reset state
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Inventory.Instance.removePlayerLife();

            if (Inventory.Instance.getPlayerLives() != 0)
            {
                player1.setPosition(new Vector2(65, 500));
                player2.setPosition(new Vector2(25, 500));

                Inventory.Instance.resetLevelStars();

                game.setCurrentState((int)gamestates.playing);
            }
            else
                game.setCurrentState((int)gamestates.gameOver);
        }

        /// <summary>
        /// draw function for the reset state
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            
        }

    }
}
