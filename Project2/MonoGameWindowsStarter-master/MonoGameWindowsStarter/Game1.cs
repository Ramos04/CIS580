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

        public Random Random = new Random();
        private List<Rock> rocks;
        Ship ship;
        Rock rock;

        private float timer; 

        KeyboardState oldKeyboardState;
        KeyboardState newKeyboardState;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // create the ship 
            ship = new Ship(this);

            // create list of rocks
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
            // Set the game screen size
            graphics.PreferredBackBufferWidth = 1042;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();

            ship.Initialize();

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

            // TODO: use this.Content to load your game content here
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
            // TODO: Add your update logic here
            newKeyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (newKeyboardState.IsKeyDown(Keys.Escape))
                Exit();

            // update the time
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // update the shp 
            ship.Update(gameTime, rocks);

            // update rocks
            foreach (Rock rock in rocks)
            {
                rock.Update(gameTime);
                if (Collisions.CollidesWith(rock.Bounds, ship.Bounds))
                {
                    ship.HasCrashed = true; 
                } 
            }

            if(ship.HasCrashed == true)
            {
                ship.Crash(); 
                System.Threading.Thread.Sleep(5000);
                this.Exit(); 
            }

            // check the timer and add a new rock if it is less than .4 seconds
            if (timer > 0.20f)
            {
                timer = 0;
                rocks.Add(new Rock(this, Content));
            }

            // remove rocks from the list that are past the screen
            for (int i = 0; i < rocks.Count; i++)
            {
                var rock = rocks[i];
                if (rock.IsRemoved)
                {
                    rocks.RemoveAt(i);
                    i--;
                    rock.Miss();
                }
            }

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

            spriteBatch.Begin();

            // draw the ship 
            ship.Draw(spriteBatch);
            
            // loop through the rocks
            foreach(Rock rock in rocks)
            {
                rock.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
