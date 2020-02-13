using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGameWindowsStarter
{
    public class Rock
    {
        /// <summary>
        /// The game object
        /// </summary>
        Game1 game;

        /// <summary>
        /// This rock's bounds
        /// </summary>
        public BoundingRectangle Bounds;

        /// <summary>
        /// This rock's texture
        /// </summary>
        Texture2D texture;

        /// <summary>
        /// If the rock is removed from the game 
        /// </summary>
        public bool IsRemoved;

        /// <summary>
        /// Speed multiplier 
        /// </summary>
        private float speed;// = (float)0.60;

        SoundEffect missSFX;                   // A sound effect of dodging the rock

        /// <summary>
        /// Creates a rock
        /// </summary>
        /// <param name="game">The game this rock belongs to</param>
        public Rock(Game1 game, ContentManager content)
        {
            this.game = game;
            speed = game.Random.Next(5, 10) * (float)0.1;
            texture = content.Load<Texture2D>("sprites/rock");
            missSFX = content.Load<SoundEffect>("sounds/miss");
            IsRemoved = false;

            // set the size of the rock
            Bounds.Width = 25;
            Bounds.Height = 25;

            // randomly position the rock 
            Bounds.X = (float)game.Random.Next(0, (int)(game.GraphicsDevice.Viewport.Width - Bounds.Width));
            Bounds.Y = 0;
        }

        /// <summary>
        /// Initializes the rock, setting its initial size 
        /// and centering it on the bottom of the screen.
        /// </summary>
        public void Initialize()
        {

        }

        /// <summary>
        /// Loads the rock's content
        /// </summary>
        /// <param name="content">The ContentManager to use</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("rock");
        }

        /// <summary>
        /// Updates the rock
        /// </summary>
        /// <param name="gameTime">The game's GameTime</param>
        public void Update(GameTime gameTime)
        {
            // move the rock down 
            Bounds.Y += speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Stop the paddle from going off-screen
            if (Bounds.Y > game.GraphicsDevice.Viewport.Height)
            {
                IsRemoved = true; 
            }
        }

        /// <summary>
        /// Draw the rock
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch to draw the rock with.  This method should 
        /// be invoked between SpriteBatch.Begin() and SpriteBatch.End() calls.
        /// </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Bounds, Color.White);
        }

        public void Miss()
        {
            missSFX.Play();
        }
    }
}
