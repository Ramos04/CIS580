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
    /// <summary>
    /// enum representing the direction the ship is going 
    /// </summary>
    enum State
    {
        Idle = 0,
        Left = 1,
        Right = 2
    }

    public class Ship
    {
        /// <summary>
        /// The width of the animation frames
        /// </summary>
        const int FRAME_WIDTH = 140;

        /// <summary>
        /// The hieght of the animation frames
        /// </summary>
        const int FRAME_HEIGHT = 80;

        /// <summary>
        /// private variables
        /// </summary>
        Game1 game;                             // The game object
        public BoundingRectangle Bounds;        // This ships's bounds
        Texture2D texture;                      // This ships's texture
        public bool HasCrashed;                 // If the ship has crashed
        private float speed = (float)0.30;      // Speed multiplier 
        SoundEffect moveSFX;                    // A sound effect of the ship moving
        SoundEffect crashSFX;                   // A sound effect of the ship moving
        SpriteFont font;                        // font drawing
        State state;                            // Which direction the ship is moving
        int frame;
        Vector2 position;
        TimeSpan timer; 



        /// <summary>
        /// Creates a ship
        /// </summary>
        /// <param name="game">The game this ship belongs to</param>
        public Ship(Game1 game)
        {
            this.game = game;
        }

        /// <summary>
        /// Initializes the ship, setting its initial size 
        /// and centering it on the bottom of the screen.
        /// </summary>
        public void Initialize()
        {
            HasCrashed = false;

            Bounds.Width = 35;
            Bounds.Height = 20;
            Bounds.X = game.GraphicsDevice.Viewport.Width / 2 - Bounds.Width / 2;
            Bounds.Y = game.GraphicsDevice.Viewport.Height - Bounds.Height;
            state = State.Idle;
        }

        /// <summary>
        /// Loads the ship's content
        /// </summary>
        /// <param name="content">The ContentManager to use</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("sprites/ship");
            moveSFX = content.Load<SoundEffect>("sounds/movement");
            crashSFX = content.Load<SoundEffect>("sounds/crash");
        }

        /// <summary>
        /// Updates the ship
        /// </summary>
        /// <param name="gameTime">The game's GameTime</param>
        public void Update(GameTime gameTime, List<Rock> rocks)
        {
            var keyboardState = Keyboard.GetState();

            //// check for collisions
            //foreach(Rock rock in rocks)
            //{
            //    if(Collisions.CollidesWith(this.Bounds, rock.Bounds))
            //    {
            //        HasCrashed = true;
            //    }
            //}

            // Move the paddle up if the up key is pressed
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                // move left
                state = State.Left;
                Bounds.X -= speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                moveSFX.Play(); 
            }

            // Move the paddle down if the down key is pressed
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                // move right
                state = State.Right; 
                Bounds.X += speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                moveSFX.Play();
            }
            else
            {
                state = State.Idle; 
            }

            // Stop the paddle from going off-screen
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
            // determine the source rectagle of the sprite's current frame
            
            var source = new Rectangle(
                (int)state * FRAME_WIDTH,   // X value 
                0,                          // Y value
                FRAME_WIDTH,                // Width 
                FRAME_HEIGHT                // Height
                );
            
            //var source = new Rectangle(0, 0, 140, 80); 
            //spriteBatch.Draw(texture, new Vector2(Bounds.X, Bounds.Y), source, Color.White);
            //spriteBatch.Draw(texture, Bounds, Color.White);
            // render the sprite
            spriteBatch.Draw(texture, new Vector2(Bounds.X, Bounds.Y), source, Color.White, 0f, Vector2.Zero, 0.25f, SpriteEffects.None, 0f);

        }

        public void Crash()
        {
            crashSFX.Play();
        }
    }
}
