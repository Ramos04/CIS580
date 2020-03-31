using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace MonoGameWindowsStarter
{
    /// <summary>et
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Camera camera;
        Player player;

        Texture2D background;
        Vector2 backgroundPosition;

        // keyboard state variables
        KeyboardState oldKeyboardState;
        KeyboardState newKeyboardState;

        public Random Random = new Random();

        float oldTime;
        float currentTime;


        ParticleSystem playerParticleSystem;
        ParticleSystem rainParticleSystem;
        ParticleSystem cometParticleSystem;



        Texture2D particleTexture;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            camera = new Camera(this);
            player = new Player(this); 
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
            graphics.PreferredBackBufferWidth = 1042;
            graphics.PreferredBackBufferHeight = 768;
            oldTime = 0.0f;

            player.Initialize(); 

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

            // load particle content
            player.LoadContent(Content);

            particleTexture = Content.Load<Texture2D>("particle");

            // The player particle system
            //----------------------------------------------------------------------------------
            // Set the SpawnParticle method
            playerParticleSystem = new ParticleSystem(this.GraphicsDevice, 1000, particleTexture);
            playerParticleSystem.SpawnParticle = (ref Particle particle) =>
            {
                //MouseState mouse = Mouse.GetState();
                //particle.Position = new Vector2(mouse.X, mouse.Y);
                particle.Position = new Vector2((GraphicsDevice.Viewport.Width/2) - player.Bounds.Width + 15, GraphicsDevice.Viewport.Height - 40);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(-50, 50, (float)Random.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(0, 100, (float)Random.NextDouble()) // Y between 0 and 100
                );
                particle.Acceleration = 0.1f * new Vector2(0, (float)-Random.NextDouble());
                if (player.verticalState == VerticalState.Jumping) { particle.Color = Color.Gold; }
                else if(player.verticalState == VerticalState.Ground && player.horizontalState != HorizontalState.Idle) { particle.Color = Color.Green; }
                else { particle.Color = Color.Red; }
                particle.Scale = 1f;
                particle.Life = 1.0f;
            };

            // Set the UpdateParticle method
            playerParticleSystem.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position += deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };
            playerParticleSystem.SpawnPerFrame = 4;

            // The rain particle system
            //----------------------------------------------------------------------------------
            rainParticleSystem = new ParticleSystem(this.GraphicsDevice, 1000, particleTexture);
            rainParticleSystem.SpawnParticle = (ref Particle particle) =>
            {
                particle.Position = new Vector2(
                    MathHelper.Lerp(0, GraphicsDevice.Viewport.Width, (float)Random.NextDouble()
                                    ), 
                    
                    MathHelper.Lerp(0, GraphicsDevice.Viewport.Height , (float)Random.NextDouble())
                    );
                particle.Velocity = new Vector2(0,
                    //MathHelper.Lerp(-50, 50, (float)Random.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(0, 100, (float)Random.NextDouble()) // Y between 0 and 100
                    );
                particle.Acceleration = 0.1f * new Vector2(0, (float)Random.NextDouble());
                particle.Color = Color.Blue;
                particle.Scale = 0.45f;
                particle.Life = 1.0f;
            };

            // Set the UpdateParticle method
            rainParticleSystem.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position += deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };
            rainParticleSystem.SpawnPerFrame = 3;

            //----------------------------------------------------------------------------------
            cometParticleSystem = new ParticleSystem(this.GraphicsDevice, 1000, particleTexture);

            cometParticleSystem.SpawnParticle = (ref Particle particle) =>
            {
                particle.Position = new Vector2(
                                        MathHelper.Lerp(0, GraphicsDevice.Viewport.Width, (float)Random.NextDouble()),
                                        MathHelper.Lerp(0, GraphicsDevice.Viewport.Height / 5, (float)Random.NextDouble())
                    );
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(-50, 50, (float)Random.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(0, 10, (float)Random.NextDouble()) // Y between 0 and 100
                    );
                particle.Acceleration = 0.1f * new Vector2((float)-Random.NextDouble(), (float)-Random.NextDouble());
                particle.Color = Color.Gold;
                particle.Scale = 1f;
                particle.Life = 1.0f;
            };

            // Set the UpdateParticle method
            cometParticleSystem.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position += deltaT * new Vector2(0, 0);//particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };
            cometParticleSystem.SpawnPerFrame = 4;

            //----------------------------------------------------------------------------------

            background = Content.Load<Texture2D>("background");
            backgroundPosition = new Vector2(0, -175);

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

            // update player
            player.Update(gameTime);
            camera.Follow(player);

            //update particle system
            playerParticleSystem.Update(gameTime);

            rainParticleSystem.Update(gameTime);
            cometParticleSystem.Update(gameTime);

            oldKeyboardState = newKeyboardState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin(transformMatrix: camera.Transform);
            spriteBatch.Draw(background, backgroundPosition, Color.White);
            playerParticleSystem.Draw();
            rainParticleSystem.Draw();
            
            //currentTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //if(currentTime - oldTime >= 5) {
              //  oldTime = currentTime;
                //cometParticleSystem.Draw();
            //}
            player.Draw(spriteBatch); 
            spriteBatch.End(); 
            base.Draw(gameTime);
        }
    }
}
