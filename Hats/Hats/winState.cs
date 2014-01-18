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
    /// <summary>
    /// Controls the win state
    /// </summary>
    class winState : GameState
    {
        View mainView;
        Player player1;
        Player player2;
        Map map;
        Camera camera;

        Texture2D screen;

        public winState(Game1 game)
            : base(game)
        {
            mainView = game.getView();
            player1 = game.getPlayer1();
            player2 = game.getPlayer2();
            map = game.getMap();
            camera = game.getCamera();
        }

        /// <summary>
        /// Loads the image to be displayed at the win screen
        /// </summary>
        /// <param name="cManager">Current content manager</param>
        public override void LoadContent(ContentManager cManager)
        {
            screen = cManager.Load<Texture2D>("WinScreen");
        }

        /// <summary>
        /// Checks for user input on screen
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Escape))
                game.Exit();
        }

        /// <summary>
        /// Draws function for the win state
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = game.getSpriteBatch();

            //draws the win screen and the current game behind it
            drawbackgrounds();
            mainView.drawMap(spriteBatch, map);
            mainView.drawPlayer(spriteBatch, player1, map.getSprites());
            mainView.drawPlayer(spriteBatch, player2, map.getSprites());
            mainView.drawPauseMenu(spriteBatch, screen, camera.getPosition());
        }
    }
}
