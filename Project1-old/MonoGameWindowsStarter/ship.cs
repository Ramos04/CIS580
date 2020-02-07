using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGameWindowsStarter
{
    public class Ship
    {
        // game object
        Game1 game;

        // ships bounds
        public BoundingRectangle Bounds;

        // ships texture
        Texture2D texture;

        double speed; 

        /// <summary>
        /// creates ship 
        /// </summary>
        /// <param name="game"></param>
        public Ship(Game1 game)
        {
            this.game = game; 
        }

        /// <summary>
        /// Initialize the ship, set in initial size, 
        /// and centers on the bottom of screen
        /// </summary>
        public void Initialize()
        {
            speed = 1.0;
            Bounds.Width = 25;
            Bounds.Height = 25;
            Bounds.Y = game.GraphicsDevice.Viewport.Height - Bounds.Height;
            Bounds.X = game.GraphicsDevice.Viewport.Width / 2 - Bounds.Width / 2;
        }

        /// <summary>
        /// Loads ship's content
        /// </summary>
        /// <param name="content">Content manager</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("ship");
        }

        /// <summary>
        /// Updates the ship
        /// </summary>
        /// <param name="gameTime">Games gamtime to use</param>        
        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            // Move the ship left if the left arrow key is pressed
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                // move up
                Bounds.X -= (float)speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            // Move the ship right if theright arrow key is pressed
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                // move down
                Bounds.X += (float)speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            // Stop the ship from going off-screen
            if (Bounds.X < 0)
            {
                Bounds.X = 0;
            }
            if (Bounds.X > game.GraphicsDevice.Viewport.Width - Bounds.Width)
            {
                Bounds.X = game.GraphicsDevice.Viewport.Width - Bounds.Width;
            }
        }

        /// <summary>
        /// Draw the ship
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch to draw the ship with.  This method should 
        /// be invoked between SpriteBatch.Begin() and SpriteBatch.End() calls.
        /// </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Bounds, Color.White);
        }
    }
}
