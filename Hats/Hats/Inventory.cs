using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Hats
{
    /// <summary>
    /// Inventory singleton
    /// </summary>
    public class Inventory
    {
        private static Inventory instance;
        private bool[] hatsOwned = new bool[5] {true, false, false, false, false};
        private int levelStars;
        private int playerLives;

        private Inventory() { }

        public static Inventory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Inventory();
                }
                return instance;
            }
        }

        /// <summary>
        /// returns the inventory array
        /// </summary>
        /// <returns>hatsOwned</returns>
        public bool[] getHatsOwned()
        {
            return hatsOwned;
        }

        /// <summary>
        /// overrites the hats owned array
        /// </summary>
        /// <param name="val"></param>
        public void setHatsOwned(bool[] val)
        {
            hatsOwned = val;
        }

        /// <summary>
        /// returns the number of collected stars
        /// </summary>
        /// <returns></returns>
        public int getLevelStars()
        {
            return levelStars;
        }

        /// <summary>
        /// adds a star to the counter
        /// </summary>
        public void addLevelStars()
        {
            levelStars++;
        }

        /// <summary>
        /// Resets the star counter to zero
        /// </summary>
        public void resetLevelStars()
        {
            levelStars = 0;
        }

        /// <summary>
        /// Returns the players remaining lives
        /// </summary>
        /// <returns></returns>
        public int getPlayerLives()
        {
            return playerLives;
        }

        /// <summary>
        /// Resets the players lives back to full
        /// </summary>
        public void resetPlayerLives()
        {
            playerLives = 3;
        }

        /// <summary>
        /// Removes a player life
        /// </summary>
        public void removePlayerLife()
        {
            if(playerLives !=0)
                playerLives--;
        }

        /// <summary>
        /// Adds a player life
        /// </summary>
        public void addLife()
        {
            playerLives++;
        }
    }
}
