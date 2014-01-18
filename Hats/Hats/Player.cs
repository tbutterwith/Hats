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
    public class Player
    {
        //variables to set players position
        private Vector2 position = new Vector2(65,500);
        
        //variables to set the sprite texture
        Rectangle spriteRect = new Rectangle(32,0,32,32);
        enum direction{down, left, right, up};
        private Texture2D texture;

        //variables to control the sprites movement
        Vector2 speed = new Vector2(0,0);
        int moveSpeed = 5;
        float fallspeed = 0.7f;
        float jumpHeight = -13f;
        int baseHeight = 740;
        bool floor = false;
        bool top = false;
        public bool isJumping = false;
        bool canLeft = true;
        bool canRight = true;
        int wearingHat = 0;
        int curDirection;

        //variables to control sprite animation
        float timer = 0;
        public int switchFrame = 100;
        int currentFrame = 1;

        public Player()
        {
            
        }

        /*
         * Loads a texture for the player
         */
        public void loadContent(ContentManager cManager, string location)
        {
            texture = cManager.Load<Texture2D>(location);
        }

        /*
         * Controls the players movement
         * Takes a keyboard input and moves the sprite accordingly. Also calls the animation
         * function to cycle through the sprite sheet
         */
        public void moveSprite(GameTime gameTime, int currentHat)
        {
            //resets x-axis speed to 0. Stops the player accelerating each frame
            speed.X = 0;
            // changes player attributes when wearing hat
            if (currentHat == 1)
            {
                jumpHeight = -13f;
                fallspeed = 0.5f;
            }
            else if (currentHat == 2)
            {
                jumpHeight = -4f;
                fallspeed = 0.4f;
                isJumping = false;
            }
            else
            {
                jumpHeight = -13f;
                fallspeed = 0.7f;
            }
            
            if (position.Y < 0)
                position.Y = 0;
            //creates a new keyboard state
            KeyboardState keyState = Keyboard.GetState();

            //checks is player is above the ground height
            if (position.Y < baseHeight)
            {
                speed.Y = speed.Y + fallspeed;
                //canRight = true;
                //canLeft = true;
            }
            else
            {
                isJumping = false;
                speed.Y = 0;
            }
            if (floor)
            {
                isJumping = false;
                speed.Y = 0;
            }
            if (top)
            {
                speed.Y = 0;
                speed.Y = speed.Y + fallspeed;
            }

            //takes keyboard input for the right key and moves player right
            if (keyState.IsKeyDown(Keys.Right) && canRight)
            {
                speed.X = speed.X + moveSpeed;
                spriteRect.Y = (int)direction.right * 32;
                animateSprite(gameTime);
                canLeft = true;
                curDirection = 1;
            }
            //takes keyboard input for the left key and moves the player left
            //sets boundry at 0px on left hand side of screen
            else if (keyState.IsKeyDown(Keys.Left) && canLeft)
            {
                if (position.X >= 0)
                {
                    speed.X = speed.X - moveSpeed;
                    spriteRect.Y = (int)direction.left * 32;
                    animateSprite(gameTime);
                    canRight = true;
                    curDirection = 0;
                }
            }

            //takes keyboard input from space bar
            if (keyState.IsKeyDown(Keys.Space) || keyState.IsKeyDown(Keys.Up))
            {
                if(!isJumping)
                speed.Y = jumpHeight;

                isJumping = true;
            }

            //adjusts players vertical position after jump, taking gravity into account
            position = position + speed;
        }

        //cycles through the sprite sheet in animate the player
        public void animateSprite(GameTime gt)
        {
            timer += (float)gt.ElapsedGameTime.TotalMilliseconds;

            if (timer >= switchFrame)
            {
                timer = 0;
                currentFrame++;
                if (currentFrame > 2)
                    currentFrame = 0;

                spriteRect.X = 32 * currentFrame;
            }
        }

        //returns the players position on the screen
        public Vector2 getPosition()
        {
            return position;
        }

        //sets the players position
        public void setPosition(Vector2 newPos)
        {
            position = newPos;
        }
        
        //returns the applied sprite texture
        public Texture2D getTexture()
        {
            return texture;
        }

        //returns the sprite rectangle
        public Rectangle getRect()
        {
            return spriteRect;
        }

        //returns the player speed
        public Vector2 getSpeed()
        {
            return speed;
        }

        //sets the players speed
        public void setSpeed(Vector2 val)
        {
            speed = val;
        }

        //returns whether the player can move left
        public bool getCanLeft()
        {
            return canLeft;
        }
        //sets whether the player can move left
        public void setCanLeft(bool val)
        {
            canLeft = val;
        }

        //returns whether the player can move right
        public bool getCanRight()
        {
            return canLeft;
        }

        //sets whether the player can move right
        public void setCanRight(bool val)
        {
            canRight = val;
        }

        //returns if player is touching the floor
        public bool getFloor()
        {
            return floor;
        }

        //sets if the player is touching the floor
        public void setFloor(bool val)
        {
            floor = val;
        }

        //gets if the player can move higher
        public bool getTop()
        {
            return top;
        }

        //sets if the player can move higher
        public void setTop(bool val)
        {
            top = val;
        }

        //sets the value of the current players hat
        public void setWearingHat(int val)
        {
            wearingHat = val;
        }

        //returns the value of current hat worn
        public int getWearingHat()
        {
            return wearingHat;
        }

        //sets the players current direction of motion
        public void setDirection(string val)
        {
            if (val == "up")
                spriteRect.Y = (int)direction.up * 32;
            else if (val == "down")
                spriteRect.Y = (int)direction.down * 32;
        }

        //gets the current direct of travel
        public int getDirection()
        {
            return curDirection;
        }
    }
}
