using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Hats
{
    /// <summary>
    /// Background class holds all the info relating to the background image
    /// </summary>
    public class Background
    {
        Texture2D background;

        float backgroundLeftX;

        public Background()
        {
            backgroundLeftX = 0;
        }

        /// <summary>
        /// loads the background image
        /// </summary>
        /// <param name="cManager"></param>
        /// <param name="backgroundLocation"></param>
        public void loadContent(ContentManager cManager, string backgroundLocation)
        {
            background = cManager.Load<Texture2D>(backgroundLocation);
        }


        /// <summary>
        /// Returns the background texture
        /// </summary>
        /// <returns></returns>
        public Texture2D getBackground()
        {
            return background;
        }

        /// <summary>
        /// Returns the left X coordinate of the image
        /// </summary>
        /// <returns></returns>
        public float getBackgroundX()
        {
            return backgroundLeftX;
        }

        /// <summary>
        /// Sets the backgrounds left X coordinate
        /// </summary>
        /// <param name="val"></param>
        public void setBackgroundX(float val)
        {
            backgroundLeftX = val;
        }
    }
}
