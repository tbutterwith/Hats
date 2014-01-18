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
    abstract class GameState
    {
        protected readonly Game1 game;

        //create two instances of backgound to overlap screen
        View mainView;
        Camera camera;
        Background background1;
        Background background2;
        public enum gamestates { menu, resetGame, playing, paused, gameOver, win }
               

  
        protected GameState(Game1 game)  
        {  
            this.game = game;

            mainView = game.getView();
            camera = game.getCamera();
            background1 = game.getBackground1();
            background2 = game.getBackground2();

        }

        public abstract void LoadContent(ContentManager cManager);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);

        protected void adjustBackground()
        {
            float background1X = background1.getBackgroundX();
            float background2X = background2.getBackgroundX();

            background1.setBackgroundX(background1X + 0.5f);
            background2.setBackgroundX(background2X + 0.5f);
        }

        /// <summary>
        /// Draws the background to the screen. Implements the two seperate images so they appear
        /// to scroll seemlessly
        /// </summary>
        protected void drawbackgrounds()
        {
            SpriteBatch spriteBatch = game.getSpriteBatch();

            adjustBackground();

            Vector2 cameraPos = camera.getPosition();
            int screenEnd = (int)cameraPos.X + 800;
            float background1RightX = background1.getBackgroundX() + background1.getBackground().Width;
            float background2RightX = background2.getBackgroundX() + background2.getBackground().Width;

            if (background2RightX <= screenEnd)
            {
                background1.setBackgroundX(background2.getBackgroundX());
                background2.setBackgroundX(background2RightX);
            }
            else if (background1.getBackgroundX() > cameraPos.X)
            {
                background2.setBackgroundX(background1.getBackgroundX());
                background1.setBackgroundX(background2.getBackgroundX() - background2.getBackground().Width);
            }

            mainView.drawBackground(spriteBatch, background1.getBackground(), background1.getBackgroundX());
            mainView.drawBackground(spriteBatch, background2.getBackground(), background2.getBackgroundX());
        }
    }
}
