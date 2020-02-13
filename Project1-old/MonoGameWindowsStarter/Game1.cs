using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // declare ship object
        Ship ship;
        List<Rock> rocks; 

        // keyboard state variables
        KeyboardState oldKeyboardState;
        KeyboardState newKeyboardState;

        public Random Random = new Random();

        private float timer; 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // create the ship 
            ship = new Ship(this);
            rocks = new List<Rock>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // Set the game screen size
            //graphics.PreferredBackBufferWidth = 1042;
            //graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 700;
            graphics.PreferredBackBufferHeight = 400;
            graphics.ApplyChanges();
            ship.Initialize();
            //rocks[0].Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // load ship content
            ship.LoadContent(Content);

            // Adds the first rock to the list
            rocks.Add(new Rock(this, Content));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            newKeyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (newKeyboardState.IsKeyDown(Keys.Escape))
                Exit();

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // update rocks
            foreach(Rock rock in rocks)
            {
                rock.Update(gameTime, ship); 
            }

            // update ship
            ship.Update(gameTime);

            if(timer > 0.30f)
            {
                timer = 0;
                rocks.Add(new Rock(this, Content));
            }

            for(int i = 0; i < rocks.Count; i++)
            {
                var rock = rocks[i];
                if (rock.IsRemoved)
                {
                    rocks.RemoveAt(i);
                    i--; 
                }
            }

            // update keyboard state
            oldKeyboardState = newKeyboardState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //GraphicsDevice.Clear(Color.Black);

            // spritebatch draw
            spriteBatch.Begin();
            ship.Draw(spriteBatch);
            //rock.Draw(spriteBatch);
            foreach(Rock rock in rocks)
            {
                rock.Draw(spriteBatch);
            }
            spriteBatch.End(); 

            base.Draw(gameTime);
        }
    }
}
