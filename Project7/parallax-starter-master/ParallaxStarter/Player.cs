using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace ParallaxStarter
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

    public class Player : ISprite
    {

        Game1 game;                             // The game object
        /// <summary>
        /// A spritesheet containing a helicopter image
        /// </summary>
        Texture2D spritesheet;
        TimeSpan jumpTimer;
        const int JUMP_TIME = 150;


        /// <summary>
        /// The portion of the spritesheet that is the helicopter
        /// </summary>
        Rectangle sourceRect = new Rectangle
        {
            X = 0,
            Y = 0,
            Width = 49,
            Height = 60
        };

        private float speed = (float)0.30;      // Speed multiplier 
        private float vSpeed = (float)1.0;      // Speed multiplier
        VerticalState verticalState = VerticalState.Ground;
        HorizontalState horizontalState = HorizontalState.Idle;

        /// <summary>
        /// The origin of the helicopter sprite
        /// </summary>
        Vector2 origin;

        float jumpHeight;

        /// <summary>
        /// The angle the helicopter should tilt
        /// </summary>
        float angle = 0;

        /// <summary>
        /// The player's position in the world
        /// </summary>
        public Vector2 Position { get; set; }

        public Vector2 oldPosition { get; set; }


        /// <summary>
        /// How fast the player moves
        /// </summary>
        public float Speed { get; set; } = 100;

        /// <summary>
        /// Constructs a player
        /// </summary>
        /// <param name="spritesheet">The player's spritesheet</param>
        public Player(Texture2D spritesheet, int viewportWidth, int viewportHeight)
        {
            this.spritesheet = spritesheet;
            origin = new Vector2(-150,15);
            this.Position = new Vector2(viewportWidth - sourceRect.Width, viewportHeight - sourceRect.Width);
            this.oldPosition = Position;
            //this.Position = new Vector2(200, 200);
            //origin = new Vector2(viewportWidth / 2 - sourceRect.Width / 2, viewportHeight - sourceRect.Height);

        }

        /// <summary>
        /// Updates the player position based on GamePad or Keyboard input
        /// </summary>
        /// <param name="gameTime">The GameTime object</param>
        public void Update(GameTime gameTime)
        {
            Vector2 direction = Vector2.Zero;
            
            // Use GamePad for input
            var gamePad = GamePad.GetState(0);

            // The thumbstick value is a vector2 with X & Y between [-1f and 1f] and 0 if no GamePad is available
            direction.X = gamePad.ThumbSticks.Left.X;

            // We need to inverty the Y axis
            direction.Y = -gamePad.ThumbSticks.Left.Y;
            var keyboard = Keyboard.GetState();

            switch (verticalState)
            {
                case VerticalState.Ground:
                    if (keyboard.IsKeyDown(Keys.Space))
                    {
                        verticalState = VerticalState.Jumping;
                        jumpTimer = new TimeSpan(0);
                    }
                    break;
                case VerticalState.Jumping:
                    jumpTimer += gameTime.ElapsedGameTime;
                    direction.Y -= (250 / (float)jumpTimer.TotalMilliseconds);
                    jumpHeight = (250 / (float)jumpTimer.TotalMilliseconds);
                    if (jumpTimer.TotalMilliseconds >= JUMP_TIME) verticalState = VerticalState.Falling;
                    break;
                case VerticalState.Falling:
                    direction.Y += jumpHeight;
                    break;
            }

            // Override with keyboard input
            if(keyboard.IsKeyDown(Keys.Left) || keyboard.IsKeyDown(Keys.A))
            {
                direction.X -= 1;
            }
            if (keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.D)) 
            {
                direction.X += 1;
            }
            //if(keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.W))
           // {
            //    direction.Y -= 1;
           // }
            //if(keyboard.IsKeyDown(Keys.Down) || keyboard.IsKeyDown(Keys.S))
           // {
           //     direction.Y += 1;
           // }


            angle = 0;
 
            Position += (float)gameTime.ElapsedGameTime.TotalSeconds * Speed * direction;

            if(Position.Y > origin.Y && verticalState == VerticalState.Falling)
            {
                Position = new Vector2(Position.X, oldPosition.Y);
                verticalState = VerticalState.Ground;
            }
        }

        /// <summary>
        /// Draws the player sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Render the helicopter, rotating about the rotors
            spriteBatch.Draw(spritesheet, Position, sourceRect, Color.White, angle, origin, 1f, SpriteEffects.None, 0.7f);
        }

    }
}
