using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Hats
{
    public class Map
    {
        //textures applied to background and map tiles
        Texture2D spriteSheet;

        //int array to hold type of map tile on each 32*32 square
        int[,] mapArray;


        public Map()
        {
            //creates an array to hold map tiles and sets maximum map size
            mapArray = new int[27, 500];
        }

        /// <summary>
        /// Loads the content used by a map object
        /// </summary>
        /// <param name="cManager">Content manager</param>
        /// <param name="spriteLocation">Location of the sprite sheet</param>
        /// <param name="mapArrayLocation">Location of the map text file</param>
        public void loadContent(ContentManager cManager, string spriteLocation, string mapArrayLocation)
        {
            spriteSheet = cManager.Load<Texture2D>(spriteLocation);
            loadMap(mapArrayLocation);
        }
        
        /// <summary>
        /// Reads in the text file into the mapArray
        /// </summary>
        /// <param name="fileName"></param>
        public void loadMap(string fileName)
        {
            string[] lines = System.IO.File.ReadAllLines(fileName);

            int i = 0;
            int xcounter = 0;
            int ycounter = 0;
            foreach (string line in lines)
            {
                string currentLine = lines[i];
                for (int j = 0; j < currentLine.Length; j++)
                {
                    //Console.WriteLine(currentLine.Length);
                    if (!char.IsDigit(currentLine[j]))
                    {
                        if(j == 1)
                        {
                            mapArray[ycounter, xcounter] = currentLine[j-1] - '0';
                        }
                        else if (char.IsDigit(currentLine[j - 2]))
                        {
                            mapArray[ycounter, xcounter] = currentLine[j - 1] - '0';
                            mapArray[ycounter, xcounter] = mapArray[ycounter, xcounter] + 10;
                            //Console.Write(mapArray[ycounter, xcounter]);
                        }
                        else
                        {
                            mapArray[ycounter, xcounter] = currentLine[j - 1] - '0';
                        }
                        xcounter++;
                    }
                }
                xcounter = 0;
                ycounter++;
                i++;
            }
        }

        /// <summary>
        /// Removes an object from the map array
        /// </summary>
        /// <param name="locationString">location of object to be removed</param>
        public void removeObject(string locationString)
        {
            int locationX = 0;
            int locationY = 0;
            int split = 0;
            string current = "";
            for (int i = 0; i < locationString.Length; i++)
            {
                if (char.IsDigit(locationString[i]))
                {
                    current = current + locationString[i];
                }
                else
                {
                    if (split == 0)
                    {
                        locationX = Convert.ToInt16(current);
                        current = "";
                    }
                    else
                        locationY = Convert.ToInt16(current);
                    split = 1;
                }
                //Console.WriteLine(current);
            }
            //Console.WriteLine(locationString);
            mapArray[locationX, locationY] = 0;
        }

        /// <summary>
        /// Returns the mapArray
        /// </summary>
        /// <returns>mapArray</returns>
        public int[,] getMapArray()
        {
            return mapArray;
        }

        /// <summary>
        /// Sets a new value for the map array
        /// </summary>
        /// <param name="val">the new map array</param>
        public void setMapArray(int[,] val)
        {
            mapArray = val;
        }

        /// <summary>
        /// Returns the sprite sheet as a texture
        /// </summary>
        /// <returns>spriteSheet</returns>
        public Texture2D getSprites()
        {
            return spriteSheet;
        }
    }
}
