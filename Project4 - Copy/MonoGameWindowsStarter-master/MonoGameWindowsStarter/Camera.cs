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
    public class Camera
    {
        /*
        Texture2D texture;
        */
        Game1 game;
        

        public Camera(Game1 game)
        {
            this.game = game;
        }

        public Matrix Transform
        {
            get;
            private set; 
        }

        /*
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("background");
        }
        */

        public void Follow(Player player)
        {
           var position = Matrix.CreateTranslation(
                -player.Bounds.X - (player.Bounds.Width / 2),
                -player.Bounds.Y - (player.Bounds.Height / 2),
                0);

            var offset = Matrix.CreateTranslation(
                (game.GraphicsDevice.Viewport.Width / 2) - (player.Bounds.Width / 2),
                game.GraphicsDevice.Viewport.Height - player.Bounds.Height,
                0);

            Transform = position * offset; 
        }

        /*
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Bounds, Color.White);
        }
        */
    }
}
