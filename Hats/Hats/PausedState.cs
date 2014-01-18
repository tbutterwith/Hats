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
    class PausedState : GameState
    {
        View mainView;
        Player player1;
        Player player2;
        Map map;
        Camera camera;
        Player menuMan;
        KeyboardState oldKeyState;

        int currentOption;

        enum menuOptions{ play, exit };

        Texture2D screen;

        public PausedState(Game1 game) : base(game)
        {
            mainView = game.getView();
            player1 = game.getPlayer1();
            player2 = game.getPlayer2();
            map = game.getMap();
            camera = game.getCamera();
            menuMan = new Player();

            menuMan.setPosition(new Vector2(230, 288));
            menuMan.switchFrame = 200;
        }

        /// <summary>
        /// loads the content for the paused menu
        /// </summary>
        /// <param name="cManager"></param>
        public override void LoadContent(ContentManager cManager)
        {
            screen = cManager.Load<Texture2D>("pauseMenu");
            menuMan.loadContent(cManager, "playerTexture");
        }

        /// <summary>
        /// update function for the pause menu
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            /*
             * This function checks the selected menu item from the paused menu
             */
            Vector2 menuManPosition = menuMan.getPosition();
            menuManPosition.X = camera.getPosition().X + 230;
            menuMan.setPosition(menuManPosition);

            menuMan.animateSprite(gameTime);

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Up) && !oldKeyState.IsKeyDown(Keys.Up) && currentOption != (int)menuOptions.play)
            {
                currentOption = (int)menuOptions.play;
                menuMan.setPosition(new Vector2(camera.getPosition().X + 230, 288));
            }
            else if (keyState.IsKeyDown(Keys.Down) && !oldKeyState.IsKeyDown(Keys.Down) && currentOption != (int)menuOptions.exit)
            {
                currentOption = (int)menuOptions.exit;
                menuMan.setPosition(new Vector2(camera.getPosition().X + 230, 380));
            }
            if (keyState.IsKeyDown(Keys.Enter))
            {
                if (currentOption == (int)menuOptions.play)
                {
                    game.setCurrentState((int)gamestates.playing);
                }
                else
                {
                    game.Exit();
                }
            }
        }

        /// <summary>
        /// draw function for the pause state
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
            mainView.drawPlayer(spriteBatch, menuMan, map.getSprites());
            
            
        }
    }
}
