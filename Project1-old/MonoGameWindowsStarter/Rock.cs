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
    public class Rock
    {
        // game object
        Game1 game;

        // ships bounds
        public BoundingRectangle Bounds;

        // ships texture
        Texture2D texture;

        /// <summary>
        /// The ball's velocity vector
        /// </summary>
        public Vector2 Velocity;

        public bool IsRemoved = false; 

        public Rock(Game1 game, ContentManager content)
        {
            this.game = game;
            texture = content.Load<Texture2D>("rock");

            // set the size of the rock
            Bounds.Width = 25;
            Bounds.Height = 25;

            // position the ball in the center of the screen
            Bounds.X = game.Random.Next((int)(0 + Bounds.Width), (int)(game.GraphicsDevice.Viewport.Width - Bounds.Width));
            Bounds.Y = 0;// - Bounds.Height;
            Bounds.Bottom = Bounds.Y + Bounds.Height;

            // give the ball a random velocity
            Velocity = new Vector2(
                (float)game.Random.NextDouble() * (float)0.3,
                (float)game.Random.NextDouble() * (float)0.3
            );

        }

        /// <summary>
        /// Initializes the ball, placing it in the center 
        /// of the screen and giving it a random velocity
        /// vector of length 1.
        /// </summary>
        public void Initialize()
        {
            // set the size of the rock
            Bounds.Width = 25;
            Bounds.Height = 25;

            // position the ball in the center of the screen
            Bounds.X = game.Random.Next((int)(0 + Bounds.Width), (int)(game.GraphicsDevice.Viewport.Width - Bounds.Width));
            Bounds.Y = 0;// - Bounds.Height;
            Bounds.Bottom = Bounds.Y + Bounds.Height;

            // give the ball a random velocity
            Velocity = new Vector2(
                (float)game.Random.NextDouble() * (float)0.8,
                (float)game.Random.NextDouble() * (float)0.8
            );
            //Velocity = new Vector2((float).1, (float).1);
            //Velocity.Normalize();
        }

        /// <summary>
        /// Loads rocks's content
        /// </summary>
        /// <param name="content">Content manager</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("rock");
        }

        /// <summary>
        /// Updates the rock
        /// </summary>
        /// <param name="gameTime">Games gamtime to use</param>        
        public void Update(GameTime gameTime, Ship ship)
        {
            var viewport = game.GraphicsDevice.Viewport;

            Bounds.Bottom += 0.5f * (float)gameTime.ElapsedGameTime.TotalSeconds * Velocity.Y;

            if(Collisions.CollidesWith(this.Bounds, ship.Bounds))
            {
                game.Exit();
            }
            // if the rock is at the bottom 
            if (Bounds.Bottom >= viewport.Height)
            {
                IsRemoved = true;
            }

            // if we havent hit the bottom 
            if (Bounds.Bottom < viewport.Height)
            {
                //float delta = viewport.Height - Bounds.Bottom - Bounds.Y;
                //Bounds.Y += 2 * delta; 
                Bounds.Y += Velocity.Y * Bounds.Height;
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

    }
}
