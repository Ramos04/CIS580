using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameWindowsStarter
{
    public struct BoundingRectangle
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;

        /// <summary>
        /// Gets or sets the right value
        /// </summary>
        public float Right
        {
            get => this.Right;
            set
            {
                this.Right = value;
            }
        }

        /// <summary>
        /// Gets or set the left value
        /// </summary>
        public float Left
        {
            get => this.Left;
            set
            {
                this.Left = value;
            }
        }

        private float _Bottom; 
        /// <summary>
        /// Gets or sets the bottom value
        /// </summary>
        public float Bottom
        {
            get => _Bottom;
            set
            {
                _Bottom = value;
            }
        }


        public BoundingRectangle(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this._Bottom = this.Height; 
        }

        /// <summary>
        /// Cast operator for casting into a Rectangle
        /// </summary>
        /// <param name="br"></param>
        public static implicit operator Rectangle(BoundingRectangle br)
        {
            return new Rectangle(
                (int)br.X,
                (int)br.Y,
                (int)br.Width,
                (int)br.Height);
        }
    }
}
