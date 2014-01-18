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
    class MenuState : GameState
    {
        //Creates a new intance of the objects required
        Camera camera;
        View mainView;
        Texture2D logo;
        Texture2D options;
        Texture2D helpScreen;
        Player menuMan;

        //creates an enum with the menu options
        enum menuOptions { menu, play, help, exit };
        //integer values for the selected and highlighted items
        int currentOption;
        int selectedOption;

        //creates an instance to hold the old keyboard state
        KeyboardState oldKeyState;

        
        public MenuState(Game1 game) : base(game)
        {
            mainView = game.getView();
            camera = game.getCamera();
            menuMan = new Player();

            menuMan.setPosition(new Vector2(220,220));
            currentOption = (int)menuOptions.play;
            selectedOption = (int)menuOptions.menu;
            menuMan.setDirection("down");
            menuMan.switchFrame = 200;
        }

        /// <summary>
        /// loads the content for the current state
        /// </summary>
        /// <param name="cManager">current content manager</param>
        public override void LoadContent(ContentManager cManager)
        {
            logo = cManager.Load<Texture2D>("logo");

            options = cManager.Load<Texture2D>("options");
            menuMan.loadContent(cManager, "playerTexture");
            helpScreen = cManager.Load<Texture2D>("help");
        }

        /// <summary>
        /// update function for the menu state
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        public override void Update(GameTime gameTime)
        {
            camera.update(new Vector2(0, 0));
            menuMan.animateSprite(gameTime);
            moveToggle();
        }

        /// <summary>
        /// Draw function for the menu state
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        public override void Draw(GameTime gameTime)
        {
            //draws the backgrounds to the screen
            drawbackgrounds();
            //draws the appropriate section of the menu based on user input
            if (selectedOption == (int)menuOptions.menu)
            {
                mainView.drawTitle(game.getSpriteBatch(), logo, new Vector2(260, 55));
                mainView.drawTitle(game.getSpriteBatch(), options, new Vector2(260, 200));
                mainView.drawPlayer(game.getSpriteBatch(), menuMan, menuMan.getTexture());
            }
            else if (currentOption == (int)menuOptions.help)
            {
                mainView.drawPauseMenu(game.getSpriteBatch(), helpScreen, new Vector2(0, 0));
            }
        }

        /// <summary>
        /// moves the menu toggle between options on the menu screen
        /// </summary>
        private void moveToggle()
        {
            KeyboardState keyState = Keyboard.GetState();
            //int goTo = 0;

            if (selectedOption ==  (int)menuOptions.menu)
            {


                if (keyState.IsKeyDown(Keys.Up) && !oldKeyState.IsKeyDown(Keys.Up) && currentOption != (int)menuOptions.play)
                {
                    if (currentOption == (int)menuOptions.help)
                    {
                        currentOption = (int)menuOptions.play;
                        menuMan.setPosition(new Vector2(220, 220));
                        //goTo = 220;
                    }
                    else if (currentOption == (int)menuOptions.exit)
                    {
                        currentOption = (int)menuOptions.help;
                        menuMan.setPosition(new Vector2(220, 305));
                        //goTo = 305;
                    }

                    //moveToggleUp();
                }
                else if (!keyState.IsKeyDown(Keys.Up) && oldKeyState.IsKeyDown(Keys.Up))
                {
                    // the player was holding the key down, but has just let it go
                }
                else if (keyState.IsKeyDown(Keys.Down) && !oldKeyState.IsKeyDown(Keys.Down) && currentOption != (int)menuOptions.exit)
                {
                    if (currentOption == (int)menuOptions.play)
                    {
                        currentOption = (int)menuOptions.help;
                        menuMan.setPosition(new Vector2(220, 305));
                        //goTo = 305;
                    }
                    else if (currentOption == (int)menuOptions.help)
                    {
                        currentOption = (int)menuOptions.exit;
                        menuMan.setPosition(new Vector2(220, 390));
                        //goTo = 390;
                    }

                    //moveToggleDown(goTo);
                }
                else if (!keyState.IsKeyDown(Keys.Down) && oldKeyState.IsKeyDown(Keys.Down))
                {
                    // the player was holding the key down, but has just let it go
                }

                oldKeyState = keyState;

                if (keyState.IsKeyDown(Keys.Enter))
                {
                    if (currentOption == (int)menuOptions.play)
                    {
                        game.setCurrentState((int)gamestates.playing);
                        selectedOption = (int)menuOptions.menu;
                    }
                    else if (currentOption == (int)menuOptions.exit)
                        game.Exit();
                    else
                        selectedOption = currentOption;
                    //go to some screen thing
                } 
            }
            else if (selectedOption == (int)menuOptions.help)
            {
                if (keyState.IsKeyDown(Keys.Back))
                    selectedOption = (int)menuOptions.menu;
            }
        }
    }
}
