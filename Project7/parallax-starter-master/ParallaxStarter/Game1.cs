using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ParallaxStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            // PLAYER
            //-----------------------------------------------------------
            var spritesheet = Content.Load<Texture2D>("player");
            player = new Player(spritesheet, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            var playerLayer = new ParallaxLayer(this);
            playerLayer.ScrollController = new PlayerTrackingScrollController(player, 1.0f);
            playerLayer.Sprites.Add(player);
            playerLayer.DrawOrder = 5;
            Components.Add(playerLayer);

            // BACKGROUND
            //-----------------------------------------------------------
            var backgroundTexture = Content.Load<Texture2D>("finalNight");
            var backgroundSprite = new StaticSprite(backgroundTexture);
            var backgroundLayer = new ParallaxLayer(this);
            backgroundLayer.ScrollController = new PlayerTrackingScrollController(player, 0.1f);
            backgroundLayer.Sprites.Add(backgroundSprite);
            backgroundLayer.DrawOrder = 0;
            Components.Add(backgroundLayer);


            var midgroundTexture = Content.Load<Texture2D>("sky");
            var midgroundSprite = new StaticSprite(midgroundTexture);
            var midgroundLayer = new ParallaxLayer(this);
            midgroundLayer.ScrollController = new PlayerTrackingScrollController(player, 0.2f);
            midgroundLayer.Sprites.Add(midgroundSprite);
            midgroundLayer.DrawOrder = 1;
            Components.Add(midgroundLayer);

            // Foreground
            //-----------------------------------------------------------
            var foregroundTexture = new Texture2D[] {
                Content.Load<Texture2D>("trees1"),
                Content.Load<Texture2D>("trees1")
            };

            var foregroundSprite = new StaticSprite[]
            {
                new StaticSprite(foregroundTexture[0]),
                new StaticSprite(foregroundTexture[1], new Vector2(3500, 0))
            };

            var foregroundLayer = new ParallaxLayer(this);
            foregroundLayer.ScrollController = new PlayerTrackingScrollController(player, 0.5f);

            foregroundLayer.Sprites.AddRange(foregroundSprite);
            foregroundLayer.DrawOrder = 2;
 
            Components.Add(foregroundLayer);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
