using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Hats
{
    public class View
    {
        /// <summary>
        /// Draws the player to the screen
        /// </summary>
        /// <param name="sBatch">Current sprite batch</param>
        /// <param name="player">Player to be drawn</param>
        /// <param name="sprites">The texture to apply to the player</param>
        public void drawPlayer(SpriteBatch sBatch, Player player, Texture2D sprites)
        {
            Vector2 position = player.getPosition();
            sBatch.Draw(player.getTexture(), new Rectangle((int)position.X, (int)position.Y, 32,32),player.getRect(),Color.White);
        }

        /// <summary>
        /// Draws the map tiles to the screen
        /// </summary>
        /// <param name="sBatch">Current sprite batch</param>
        /// <param name="map">Map object</param>
        public void drawMap(SpriteBatch sBatch, Map map)
        {
            //gets the map array
            int[,] mapArray = map.getMapArray();

            //draws each tile to the map depending on the value contained in mapArray for each 32x32 grid space
            int xCoOrd;
            int yCoOrd;
            for (int i = 0; i < mapArray.GetLength(0); i++)
            {
                for (int j = 0; j < mapArray.GetLength(1); j++)
                {
                    int currentTile = mapArray[i, j] - 1;
                    Rectangle tilePos = new Rectangle(j*32,i*32,32,32);
                    if (currentTile >= 0)
                    {
                        xCoOrd = (currentTile % 3) * 32;
                        yCoOrd = (currentTile / 3) * 32;
                        sBatch.Draw(map.getSprites(), tilePos, new Rectangle(xCoOrd, yCoOrd, 32, 32), Color.White);
                    }
                }
            }
        }

        /// <summary>
        /// Draws the clouds background to the screen
        /// </summary>
        /// <param name="sBatch">Current sprite batch</param>
        /// <param name="background">Background object</param>
        /// <param name="positionX">starting x position of the texture</param>
        public void drawBackground(SpriteBatch sBatch, Texture2D background, float positionX)
        {
            sBatch.Draw(background, new Vector2(positionX, 0), Color.White);
        }

        /// <summary>
        /// Draws the title to the menu screen
        /// </summary>
        /// <param name="sBatch">Current sprite batch</param>
        /// <param name="texture">The image to be drawn</param>
        /// <param name="position">Position of upper left corner</param>
        public void drawTitle(SpriteBatch sBatch, Texture2D texture, Vector2 position)
        {
            sBatch.Draw(texture, position, Color.White);
        }

        /// <summary>
        /// Draws the pause, win and loss screens to the window
        /// </summary>
        /// <param name="sbatch">Current sprite batch</param>
        /// <param name="texture">The image to be drawn</param>
        /// <param name="position">Position of upper left corner</param>
        public void drawPauseMenu(SpriteBatch sbatch, Texture2D texture, Vector2 position)
        {
            sbatch.Draw(texture, position, Color.White);
        }

        /// <summary>
        /// Draws the current star counter to the screen
        /// </summary>
        /// <param name="sBatch">Current Sprite batch</param>
        /// <param name="label">The label to be shown</param>
        /// <param name="numbers">The number sprites</param>
        /// <param name="tens">The source rectangle of the tens digit</param>
        /// <param name="singles">The source rectangle of the singles digit</param>
        /// <param name="position">The starting upper left coordinate</param>
        public void drawScores(SpriteBatch sBatch, Texture2D label, Texture2D numbers, Rectangle tens, Rectangle singles, Vector2 position)
        {
            sBatch.Draw(label, position, Color.White);
            sBatch.Draw(numbers, new Rectangle((int)position.X + 220, (int)position.Y + 10, 32, 32), tens, Color.White);
            sBatch.Draw(numbers, new Rectangle((int)position.X + 250, (int)position.Y + 10, 32, 32), singles, Color.White);
        }

        /// <summary>
        /// Draws the player remaining lives to the screen
        /// </summary>
        /// <param name="sBatch">Current sprite batch</param>
        /// <param name="numbers">The number sprites</param>
        /// <param name="num">Source rectangle of the number</param>
        /// <param name="position">Position of the upper left corner</param>
        public void drawLives(SpriteBatch sBatch, Texture2D numbers, Rectangle num, Vector2 position)
        {
            sBatch.Draw(numbers, new Rectangle((int)position.X + 650, (int)position.Y + 10, 32, 32), num, Color.White);
        }

        /// <summary>
        /// Draws hat to the player head
        /// </summary>
        /// <param name="sBatch">Current sprite batch</param>
        /// <param name="hat">Hat texture</param>
        /// <param name="hatPlace">Position of hat destination</param>
        /// <param name="hatSource">Position of hat source</param>
        /// <param name="playerPos">Players current position</param>
        public void drawHat(SpriteBatch sBatch, Texture2D hat, Rectangle hatPlace, Rectangle hatSource, Vector2 playerPos)
        {
            sBatch.Draw(hat, hatPlace, hatSource, Color.White);
        }
    }

}
