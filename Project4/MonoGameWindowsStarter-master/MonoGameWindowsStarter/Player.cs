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
    enum HorizontalState
    {
        Idle, 
        Left, 
        Right
    }

    enum VerticalState
    {
        Ground,
        Jumping,
        Falling
    }

    public class Player
    {
        /// <summary>
        /// The width of the animation frames
        /// </summary>
        const int FRAME_WIDTH = 140;

        /// <summary>
        /// The hieght of the animation frames
        /// </summary>
        const int FRAME_HEIGHT = 80;

        const int JUMP_TIME = 150;

        /// <summary>
        /// private variables
        /// </summary>
        Game1 game;                             // The game object

        public BoundingRectangle Bounds;        // This ships's bounds

        Texture2D texture;                      // This ships's texture

        SpriteFont font;                        // font drawing

        private float speed = (float)0.30;      // Speed multiplier 
        private float vSpeed = (float)1.0;      // Speed multiplier 


        int frame;

        Vector2 position;

        TimeSpan jumpTimer;


        VerticalState verticalState = VerticalState.Ground;
        HorizontalState horizontalState = HorizontalState.Idle;

        public Player(Game1 game)
        {
            this.game = game;
        }

        public void Initialize()
        {
            Bounds.Width = 49;
            Bounds.Height = 60;
            Bounds.X = game.GraphicsDevice.Viewport.Width / 2 - Bounds.Width / 2;
            Bounds.Y = game.GraphicsDevice.Viewport.Height - Bounds.Height;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("player");
        }

        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            switch (verticalState)
            {
                case VerticalState.Ground:
                    if (keyboardState.IsKeyDown(Keys.Space))
                    {
                        verticalState = VerticalState.Jumping;
                        jumpTimer = new TimeSpan(0);
                    }
                    break;
                case VerticalState.Jumping:
                    jumpTimer += gameTime.ElapsedGameTime;
                    // Simple jumping with platformer physics
                    Bounds.Y -= (250 / (float)jumpTimer.TotalMilliseconds);
                    if (jumpTimer.TotalMilliseconds >= JUMP_TIME) verticalState = VerticalState.Falling;
                    break;
                case VerticalState.Falling:
                    Bounds.Y += vSpeed;
                    // TODO: This needs to be replaced with collision logic
                    if (Bounds.Y > game.GraphicsDevice.Viewport.Height || Bounds.Y == game.GraphicsDevice.Viewport.Height)
                    {
                        verticalState = VerticalState.Ground;
                        Bounds.Y = game.GraphicsDevice.Viewport.Height;
                    }
                    break;
            }

            // horizontal movement
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                // move left
                horizontalState = HorizontalState.Left;
                Bounds.X -= speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                // move right
                horizontalState = HorizontalState.Right;
                Bounds.X += speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else
            {
                horizontalState = HorizontalState.Idle;
            }

            // Stop the ship from going off-screen
            /*
            if (Bounds.X < 0)
            {
                Bounds.X = 0;
            }
            if (Bounds.X > game.GraphicsDevice.Viewport.Width - Bounds.Width)
            {
                Bounds.X = game.GraphicsDevice.Viewport.Width - Bounds.Width;
            }
            */
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Bounds, Color.White);
        }
    }
}
