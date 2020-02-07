using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter
{
    public static class Collisions
    {
        /// <summary>
        /// Detects collisions between this BoundingRectangle and another BoundingRectangle
        /// </summary>
        /// <param name="a">This BoundingRectangle</param>
        /// <param name="b">The other BoundingRectangle</param>
        /// <returns>true if there is a collision, false otherwise</returns>
        public static bool CollidesWith(this BoundingRectangle a, BoundingRectangle b)
        {
            
            if(a.Y >= b.Y + b.Height && a.X >= b.X && a.X <= b.X + b.Width)
            {
                return true; 
            }
            

            return false; 
            
        }
    }
}
