using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Hats
{
    class GameOverState : GameState
    {
        View mainView;
        Player player1;
        Player player2;
        Map map;
        Camera camera;

        Texture2D screen;

        public GameOverState(Game1 game) : base(game)
        {
            mainView = game.getView();
            player1 = game.getPlayer1();
            player2 = game.getPlayer2();
            map = game.getMap();
            camera = game.getCamera();
        }

        /// <summary>
        /// loads the content for the game over screen
        /// </summary>
        /// <param name="cManager"></param>
        public override void LoadContent(ContentManager cManager)
        {
            screen = cManager.Load<Texture2D>("gameOver");
        }

        /// <summary>
        /// update function for the game over state
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Escape))
                game.Exit();
        }

        /// <summary>
        /// draw function for the game over state
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = game.getSpriteBatch();

            drawbackgrounds();
            mainView.drawMap(spriteBatch, map);
            mainView.drawPlayer(spriteBatch, player1, map.getSprites());
            mainView.drawPlayer(spriteBatch, player2, map.getSprites());
            mainView.drawPauseMenu(spriteBatch, screen, camera.getPosition());
        }
    }
}
