using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Hats
{
    /// <summary>
    /// Camera class responsible for controlling the viewport
    /// </summary>
    public class Camera
    {
        Vector2 position;
        Matrix viewMatrix;

        public Matrix ViewMatrix
        {
            get { return viewMatrix; }
        }

        /// <summary>
        /// updates the camera position
        /// </summary>
        /// <param name="playerPosition"></param>
        public void update(Vector2 playerPosition)
        {
            position.X = playerPosition.X - 400;
            position.Y = 0;

            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;

            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        }

        /// <summary>
        /// returns the camera position
        /// </summary>
        /// <returns></returns>
        public Vector2 getPosition()
        {
            return position;
        }
    }
}
