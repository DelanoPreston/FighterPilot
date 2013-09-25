using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameLibrary
{
    public class Camera
    {
        public Matrix cTransform;
        //public Vector2 cViewportOrigin;
        public Vector2 cPosition;
        public Vector2 cOrigin;
        public float cZoom = 2f;
        public float cOldZoom = 2f;
        public float rotation = 0;
        float prevWheelValue = 0f;
        float currWheelValue = 0f;
        public Vector2 posistion
        {
            get
            {
                return cPosition;
            }
            set
            {
                cPosition = value;
                if (cPosition.X <= 0)
                    cPosition.X = 0;
                if (cPosition.Y <= 0)
                    cPosition.Y = 0;
                if (cPosition.X >= 2000)
                    cPosition.X = 2000;
                if (cPosition.Y >= 2000)
                    cPosition.Y = 2000;
            }
        }
        public float zoom
        {
            get
            {
                return cZoom;
            }
            set
            {
                cZoom = value;
                if (cZoom < .3f)
                    cZoom = .3f;
                if (cZoom > 2f)
                    cZoom = 2f;
            }
        }

        #region Testing
        #endregion

        public Camera()
        {
            zoom = 1f;
            cPosition = new Vector2(1000, 1000);
        }
        public void Update(GraphicsDevice inGraphicsDevice, Vector2 inPlayerPosition)
        {
            cOrigin = inPlayerPosition;

            currWheelValue = Mouse.GetState().ScrollWheelValue;
            if (currWheelValue < prevWheelValue)
                zoom -= .1f;
            if (currWheelValue > prevWheelValue)
                zoom += .1f;
            prevWheelValue = currWheelValue;

            #region not needed for this game
            //if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
            //    posistion = new Vector2(posistion.X, (posistion.Y - 15.0f));
            //if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            //    posistion = new Vector2((posistion.X - 15.0f), posistion.Y);
            //if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
            //    posistion = new Vector2(posistion.X, (posistion.Y + 15.0f));
            //if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            //    posistion = new Vector2((posistion.X + 15.0f), posistion.Y);
            //cViewportOrigin = new Vector2(posistion.X, posistion.Y);
            #endregion

            cTransform = TransformMatrix(inGraphicsDevice);
        }
        public Matrix TransformMatrix(GraphicsDevice inGraphicsDevice)
        {
            cTransform = Matrix.CreateTranslation(new Vector3(-cOrigin.X, -cOrigin.Y, 0)) * Matrix.CreateScale(new Vector3(zoom, zoom, 1)) * Matrix.CreateTranslation(new Vector3((float)(inGraphicsDevice.Viewport.Width * .5f), (float)(inGraphicsDevice.Viewport.Height * .5f), 0));
            return cTransform;
        }
    }
}
