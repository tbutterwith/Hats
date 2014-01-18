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
    /// Class for the playing state, the main game loop
    /// </summary>
    class PlayingState : GameState
    {
        //creates an instance of each of object
        View mainView;
        Player player;
        Player player1;
        Player player2;
        Map map;
        Camera camera;
        Collision collision;
        Background background1;
        Background background2;

        bool[] hats;
        int currentHat;
        KeyboardState oldKeyState;

        Texture2D scoreLabel;
        Texture2D scores;

        int activePlayer;

        public PlayingState(Game1 game) : base(game)
        {
            player1 = game.getPlayer1();
            player2 = game.getPlayer2();
            player = player1;
            mainView = game.getView();
            map = game.getMap();
            camera = game.getCamera();
            collision = game.getCollision();
            background1 = game.getBackground1();
            background2 = game.getBackground2();
            currentHat = 0;
            activePlayer = 1;
        }

        /// <summary>
        /// loads the content for the playing state
        /// </summary>
        /// <param name="cManager"></param>
        public override void LoadContent(ContentManager cManager)
        {
            scoreLabel = cManager.Load<Texture2D>("starCount");
            scores = cManager.Load<Texture2D>("numbers");
        }

        /// <summary>
        /// main game logic
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //updates the inventory
            hats = Inventory.Instance.getHatsOwned();

            //check if player has lives left
            KeyboardState keystate = Keyboard.GetState();

            //checks for key presses
            if(keystate.IsKeyDown(Keys.Escape))
                game.setCurrentState((int)gamestates.paused);
            if (keystate.IsKeyDown(Keys.X) && !oldKeyState.IsKeyDown(Keys.X))
                swapHat();
            else if (!keystate.IsKeyDown(Keys.X) && oldKeyState.IsKeyDown(Keys.X))
            {
                //
            }

            if (keystate.IsKeyDown(Keys.C) && !oldKeyState.IsKeyDown(Keys.C))
                swapPlayers();
            else if (!keystate.IsKeyDown(Keys.C) && oldKeyState.IsKeyDown(Keys.C))
            {
                //
            }

            oldKeyState = keystate;
            //detects collisions cause by the players movements in the last frame
            detectCollisions();

            //moves the player
            player.moveSprite(gameTime, currentHat);

            //updates the camera to the players new position
            camera.update(player.getPosition());

            //checks if player is below level line and resets level
            if (player.getPosition().Y > 650)
                game.setCurrentState((int)gamestates.resetGame);

            if (activePlayer == 1)
                player1 = player;
            else
                player2 = player;
        }

        /// <summary>
        /// draws the player and the map to the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = game.getSpriteBatch();

            drawbackgrounds();
            mainView.drawMap(spriteBatch, map);
            mainView.drawPlayer(spriteBatch, player1, map.getSprites());
            mainView.drawPlayer(spriteBatch, player2, map.getSprites());
            if(currentHat != 0)
                mainView.drawHat(spriteBatch, map.getSprites(), hatPlacement(), hatRect(), player.getPosition());
            starCounter(spriteBatch);
            lifeCounter(spriteBatch);
        }

        /// <summary>
        /// swaps the active player
        /// </summary>
        protected void swapPlayers()
        {
            if (activePlayer == 1)
            {
                player = player2;
                activePlayer = 2;
            }
            else
            {
                player = player1;
                activePlayer = 1;
            }
        }

        /// <summary>
        /// sets the source rectangle to draw the hat
        /// </summary>
        /// <returns></returns>
        public Rectangle hatRect()
        {
            Rectangle hatRect = new Rectangle();
            hatRect.Width = hatRect.Height = 32;

            if (currentHat == 1)
            {
                hatRect.X = 0;
                hatRect.Y = 3 * 32;
            }
            else if (currentHat == 2)
            {
                hatRect.Y = 3 * 32;

                if (player.getDirection() == 0)
                    hatRect.X = 2 * 32;
                else
                    hatRect.X = 1 * 32;
            }

            return hatRect;
        }

        /// <summary>
        /// sets the destination rectangle for the hat
        /// </summary>
        /// <returns></returns>
        public Rectangle hatPlacement()
        {
            Rectangle placement = new Rectangle();
            placement.Width = placement.Height = 32;
            if (currentHat == 1 && player.getDirection() == 1)
            {
                placement.X = (int)player.getPosition().X + 2;
                placement.Y = (int)player.getPosition().Y - 23;

            }
            else if (currentHat == 1 && player.getDirection() == 0)
            {
                placement.X = (int)player.getPosition().X + 3;
                placement.Y = (int)player.getPosition().Y - 23;

            }
            else if (currentHat == 2 && player.getDirection() == 0)
            {
                placement.X = (int)player.getPosition().X - 1;
                placement.Y = (int)player.getPosition().Y - 23;
            }
            else if (currentHat == 2 && player.getDirection() == 1)
            {
                placement.X = (int)player.getPosition().X + 3;
                placement.Y = (int)player.getPosition().Y - 23;
            }


            return placement;
        }

        /// <summary>
        /// swaps the current hat
        /// </summary>
        protected void swapHat()
        {

            int i = currentHat + 1;

            while (!hats[i])
            {
                i++;
                if (i > 4)
                    i = 0;

            }
            currentHat = i;
        }

        /// <summary>
        /// calls the collison detection class, updates the game according to returned values
        /// </summary>
        protected void detectCollisions()
        {
            //checks if the player has won
            if(collision.checkWin(player1, player2, map))
            {
                game.setCurrentState((int)gamestates.win);
            }
            //if no win, continue logic
            else
            {
                if (collision.checkNearFloor(player, map))
                    player.setSpeed(new Vector2(player.getSpeed().X, player.getSpeed().Y / 7));
                string collReturn = collision.checkCollisionX(player, map);
                if (collReturn != "")
                {
                    //collision is will map block
                    if (collReturn == "left")
                        player.setCanLeft(false);
                    else if (collReturn == "right")
                        player.setCanRight(false);
                    else if (collReturn == "dead")
                        game.setCurrentState((int)gamestates.resetGame);
                    //collision is with collectable
                    else
                    {
                        int length = collReturn.Length;
                        int objectType = 0;
                        length--;
                        //extracts the object type from the string
                        if (char.IsDigit(collReturn[length - 1]))
                        {
                            objectType = collReturn[length] - '0';
                            objectType += 10;
                        }
                        else if (!char.IsDigit(collReturn[length - 1]))
                        {
                            objectType = collReturn[length] - '0';
                        }

                        //removes the object type from the returned string
                        collReturn = collReturn.Remove(collReturn.IndexOf(';'));

                        //passes the location to the map object to remove it from the array
                        map.removeObject(collReturn);

                        //if object collected is a star, adds the star to the star count
                        if (objectType == 9)
                        {
                            Inventory.Instance.addLevelStars();
                        }
                        //if object is not a star, it must be a hat
                        else
                        {
                            //creates a temporary copy of the inventory
                            bool[] inv = Inventory.Instance.getHatsOwned();

                            //adds the hat to the appropriate slot
                            if (objectType == 10)
                                inv[1] = true;
                            else if (objectType == 11)
                                inv[2] = true;

                            //writes the temp inventory to the inventory
                            Inventory.Instance.setHatsOwned(inv);
                        }
                    }
                }
                else
                {
                    player.setCanLeft(true);
                    player.setCanRight(true);
                }

                string collReturnY = collision.checkCollisionY(player, map);
                if (collReturnY != "")
                {
                    //collision is will map block
                    if (collReturnY == "lower")
                        player.setFloor(true);
                    else if (collReturnY == "upper")
                        player.setTop(true);
                    else if (collReturnY == "dead")
                        game.setCurrentState((int)gamestates.resetGame);
                    //collision is with collectable
                    else
                    {
                        int length = collReturnY.Length;
                        int objectType = 0;
                        length--;
                        //extracts the object type from the string
                        if (char.IsDigit(collReturnY[length - 1]))
                        {
                            objectType = collReturnY[length] - '0';
                            objectType += 10;
                        }
                        else if (!char.IsDigit(collReturnY[length - 1]))
                        {
                            objectType = collReturnY[length] - '0';
                        }

                        //removes the object type from the returned string
                        collReturnY = collReturnY.Remove(collReturnY.IndexOf(';'));

                        //passes the location to the map object to remove it from the array
                        map.removeObject(collReturnY);

                        //if object collected is a star, adds the star to the star count
                        if (objectType == 9)
                        {
                            Inventory.Instance.addLevelStars();
                        }
                        //if object is not a star, it must be a hat
                        else
                        {
                            //creates a temporary copy of the inventory
                            bool[] inv = Inventory.Instance.getHatsOwned();

                            //adds the hat to the appropriate slot
                            if (objectType == 10)
                                inv[1] = true;
                            else if (objectType == 11)
                                inv[2] = true;

                            //writes the temp inventory to the inventory
                            Inventory.Instance.setHatsOwned(inv);
                        }
                    }
                }
                else
                {
                    player.setFloor(false);
                    player.setTop(false);
                }
            }

        }

        /// <summary>
        /// calls the draw function for the star counter
        /// </summary>
        /// <param name="sBatch"></param>
        protected void starCounter(SpriteBatch sBatch)
        {
            Rectangle tensRect;
            Rectangle singlesRect;
            int levelStars = Inventory.Instance.getLevelStars();

            if (levelStars >= 100)
            {
                Inventory.Instance.addLife();
                Inventory.Instance.resetLevelStars();

                levelStars = 0;
            }

            int tens = (levelStars / 10) - 1;
            int singles = (levelStars % 10) - 1;

            if(tens < 0)
                tensRect = new Rectangle((9 % 3) * 32, (9 / 3) * 32, 32, 32);
            else
                tensRect = new Rectangle((tens % 3) * 32, (tens / 3) * 32, 32, 32);

            if(singles < 0)
                singlesRect = new Rectangle((9 % 3) * 32, (9 / 3) * 32, 32, 32);
            else
                singlesRect = new Rectangle((singles % 3) * 32, (singles / 3) * 32, 32, 32);

            mainView.drawScores(sBatch, scoreLabel, scores, tensRect, singlesRect, camera.getPosition());
        }

        /// <summary>
        /// calls the draw function for the life counter
        /// </summary>
        /// <param name="sBatch"></param>
        protected void lifeCounter(SpriteBatch sBatch)
        {
            int lives = Inventory.Instance.getPlayerLives();
            Rectangle num;

            lives--;

            num = new Rectangle((lives % 3) * 32, (lives / 3) * 32, 32, 32);
            mainView.drawLives(sBatch, scores, num, camera.getPosition());
        }
    }
}
