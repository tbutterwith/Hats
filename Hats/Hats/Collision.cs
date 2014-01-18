using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Hats
{
    /*
     * Collision class
     * Contains a series of functions to check collisions between a player and the map.
     * The functions either return the type of collision or the coordinates of the collision.
     */
    public class Collision
    {
        public Collision()
        {
        }

        /// <summary>
        /// Checks the players x coordinate against collisions
        /// </summary>
        /// <param name="player">Player sprite</param>
        /// <param name="map">Map object</param>
        /// <returns>Returns a string based on collision type</returns>
        public string checkCollisionX(Player player, Map map)
        {
            Vector2 playerPos = player.getPosition();
            int[,] mapArray = map.getMapArray();

            int leftX = ((int)playerPos.X) / 32;
            int rightX = ((int)playerPos.X + 32) / 32;
            int middleY = ((int)playerPos.Y + 16) / 32;

            int leftArrayPosition = checkArray(map, leftX, middleY);
            int rightArrayPosition = checkArray(map, rightX, middleY);

            //check for collisions with regular map tiles
            if (leftArrayPosition > 0 && leftArrayPosition < 7)
            {
                if (leftArrayPosition == 4 || leftArrayPosition == 6)
                    return "dead";
                else
                    return "left";
            }
            else if (rightArrayPosition > 0 && rightArrayPosition < 7)
            {
                if (rightArrayPosition == 4 || rightArrayPosition == 6)
                    return "dead";
                else
                    return "right";
            }

            
            string local;
            //check for object collision
            if (leftArrayPosition >= 9)
                return local = middleY.ToString() + "," + leftX.ToString() + ",;" + mapArray[middleY, leftX];
            else if (rightArrayPosition >= 9)
                return local = middleY.ToString() + "," + rightX.ToString() + ",;" + mapArray[middleY, rightX];

            return "";
        }

        /// <summary>
        /// Checks for collisions on the players Y axis
        /// </summary>
        /// <param name="player">Current player</param>
        /// <param name="map">Map</param>
        /// <returns>Value based on collision type</returns>
        public string checkCollisionY(Player player, Map map)
        {
            Vector2 playerPos = player.getPosition();
            int[,] mapArray = map.getMapArray();

            int X = ((int)playerPos.X + 16) / 32;
            int lowerY = ((int)playerPos.Y + 32) / 32;
            int upperY = ((int)playerPos.Y) / 32;

            int lowerYArray = checkArray(map, X, lowerY);
            int upperYArray = checkArray(map, X, upperY);

            if (lowerYArray > 0 && lowerYArray < 7)
            {
                if (lowerYArray == 4 || lowerYArray == 6)
                    return "dead";
                else
                    return "lower";
            }
            else if (upperYArray > 0 && upperYArray < 7)
            {
                if (upperYArray == 4 || upperYArray == 6)
                    return "dead";
                else
                    return "upper";
            }

            string local;
            //check for object collision
            if (lowerYArray >= 9)
                return local = lowerY.ToString() + "," + X.ToString() + ",;" + mapArray[lowerY, X];
            else if (upperYArray >= 9)
                return local = upperY.ToString() + "," + X.ToString() + ",;" + mapArray[upperY, X];

            return "";
        }

        /// <summary>
        /// Checks if the player is near the floor
        /// </summary>
        /// <param name="player">Current player</param>
        /// <param name="map">Map</param>
        /// <returns>true if player is near floor</returns>
        public bool checkNearFloor(Player player, Map map)
        {
            Vector2 playerPos = player.getPosition();
            int[,] mapArray = map.getMapArray();

            int X = ((int)playerPos.X + 16) / 32;
            int Y = ((int)playerPos.Y + 40) / 32;

            if (mapArray[Y, X] > 0 && mapArray[Y, X] < 7)
                return true;
            return false;
        }

        /// <summary>
        /// Checks if an array value has an object in it
        /// </summary>
        /// <param name="map">Map</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>int in map location</returns>
        public int checkArray(Map map, int x, int y)
        {
            int[,] mapArray = map.getMapArray();

            return mapArray[y, x];
        }

        /// <summary>
        /// Checks if both the players have collided with the end of the level
        /// </summary>
        /// <param name="p1">player 1</param>
        /// <param name="p2">player 2</param>
        /// <param name="map">map</param>
        /// <returns>true if both player present</returns>
        public bool checkWin(Player p1, Player p2, Map map)
        {
            Vector2 player1Pos = p1.getPosition();
            Vector2 player2Pos = p2.getPosition();
            int[,] mapArray = map.getMapArray();

            int leftX1 = ((int)player1Pos.X) / 32;
            int rightX1 = ((int)player1Pos.X + 32) / 32;
            int middleY1 = ((int)player1Pos.Y + 16) / 32;

            int leftArrayPosition1 = checkArray(map, leftX1, middleY1);
            int rightArrayPosition1 = checkArray(map, rightX1, middleY1);

            int leftX2 = ((int)player2Pos.X) / 32;
            int rightX2 = ((int)player2Pos.X + 32) / 32;
            int middleY2 = ((int)player2Pos.Y + 16) / 32;

            int leftArrayPosition2 = checkArray(map, leftX2, middleY2);
            int rightArrayPosition2 = checkArray(map, rightX2, middleY2);

            if (leftArrayPosition1 == 7 && leftArrayPosition2 == 7)
                return true;
            else if (leftArrayPosition1 == 7 && rightArrayPosition2 == 7)
                return true;
            else if (rightArrayPosition1 == 7 && rightArrayPosition2 == 7)
                return true;
            else if (rightArrayPosition1 == 7 && leftArrayPosition2 == 7)
                return true;
            else
                return false;
        }
    }
}
