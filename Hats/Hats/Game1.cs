using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Hats
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //creates an instance of each of my objects
        View mainView;
        Player player;
        Player player1;
        Player player2;
        Map map;
        Camera camera;
        Collision collision;
        //create two instances of backgound to overlap screen
        Background background1;
        Background background2;


        Song backgroundSong;
        bool songstart;

        public enum gamestates { menu, resetGame, playing, paused, gameOver, win }
        int numberOfStates;
        int currentState;

        private GameState[] States;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            //sets the size of the window
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            Content.RootDirectory = "Content";

            player = new Player();
            player1 = new Player();
            player2 = new Player();
            mainView = new View();
            map = new Map();
            camera = new Camera();
            collision = new Collision();
            background1 = new Background();
            background2 = new Background();

            numberOfStates = 6;
            States = new GameState[numberOfStates];
            States[(int)gamestates.menu] = new MenuState(this);
            States[(int)gamestates.resetGame] = new ResetGameState(this);
            States[(int)gamestates.playing] = new PlayingState(this);
            States[(int)gamestates.paused] = new PausedState(this);
            States[(int)gamestates.gameOver] = new GameOverState(this);
            States[(int)gamestates.win] = new winState(this);

            songstart = false;
            MediaPlayer.Volume = 0.4f;
            currentState = (int)gamestates.menu;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non- graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            background2.setBackgroundX(background1.getBackground().Width);
            Inventory.Instance.resetLevelStars();
            Inventory.Instance.resetPlayerLives();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Loads in each of the textures
            player1.loadContent(this.Content, "playerTexture");
            player2.loadContent(this.Content, "player2");
            background1.loadContent(this.Content, "background");
            background2.loadContent(this.Content, "background");
            map.loadContent(this.Content, "level", "Content/level_one.txt");
            States[(int)gamestates.menu].LoadContent(this.Content);
            States[(int)gamestates.paused].LoadContent(this.Content);
            States[(int)gamestates.gameOver].LoadContent(this.Content);
            States[(int)gamestates.playing].LoadContent(this.Content);
            States[(int)gamestates.win].LoadContent(this.Content);

            backgroundSong = Content.Load<Song>("backgroundSong");  
            MediaPlayer.IsRepeating = true; 
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// 
        /// Constitutes the controller part of the program, dealing with all of the game logic
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (!songstart)
            {
                MediaPlayer.Play(backgroundSong);
                songstart = true;
            }
            KeyboardState keyState = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            States[currentState].Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.ViewMatrix);
            States[currentState].Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /*
         * A series of get and set functions for the game objects
         */

        public Player getPlayer()
        {
            return player;
        }

        public Player getPlayer1()
        {
            return player1;
        }
        public Player getPlayer2()
        {
            return player2;
        }

        public Map getMap()
        {
            return map;
        }

        public Background getBackground1()
        {
            return background1;
        }

        public Background getBackground2()
        {
            return background2;
        }

        public View getView()
        {
            return mainView;
        }

        public Camera getCamera()
        {
            return camera;
        }

        public Collision getCollision()
        {
            return collision;
        }

        public SpriteBatch getSpriteBatch()
        { 
            return spriteBatch;
        }

        public void setCurrentState(int val)
        {
            currentState = val;
        }

    }
    
}
