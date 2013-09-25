using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace FighterPilot
{
    class Bullet
    {
        #region Variables
        public Texture2D bTexture = null;//
        public Rectangle bTextureSize = Rectangle.Empty;//
        public int bDamageAmount;
        public float bRotation = 0f;
        public float bSpeed;
        public Vector2 bPosition = Vector2.Zero;
        public Vector2 bOrigin = Vector2.Zero;//
        public Vector2 bVelocity = Vector2.Zero;
        public Rectangle bSource;
        public bool bDead;
        #endregion

        

        #region Constructors
        public Bullet(Vector2 inPosition ,Vector2 inDirection, float inRotation, float inParentShipSpeed)
        {
            bPosition = inPosition + inDirection;
            bVelocity = inDirection * inParentShipSpeed + new Vector2(inDirection.X * 3, inDirection.Y * 3);
            bRotation = inRotation;
            bDead = false;
        }
        #endregion

        #region Graphic Methods
        public void LoadContent(Texture2D inTexture)
        {
            this.bTexture = inTexture;
            bOrigin = GetFrameCenter(bTexture);
            //this.bPosition = new Vector2(300, 300);
            //this.bVelocity = new Vector2(3, 3);
        }
        public void Draw(SpriteBatch inSpriteBatch)
        {
            inSpriteBatch.Draw(bTexture, bPosition, null, Color.White, bRotation, bOrigin, 1.0f, SpriteEffects.None, 1f);
        }
        public void Update()
        {
            bPosition += bVelocity;
            if (bPosition.Y > 4000 || bPosition.Y < 0 || bPosition.X > 4000 || bPosition.X < 0)
                bDead = true;
        }
        private Vector2 GetFrameCenter(Texture2D texture)
        {
            int frameWidth = (int)GetFrameDimensions(texture).X;
            int frameHeight = (int)GetFrameDimensions(texture).Y;
            bTextureSize = new Rectangle(0, 0, frameWidth, frameHeight);
            return new Vector2(frameWidth / 2, frameHeight / 2);
        }
        private Vector2 GetFrameDimensions(Texture2D texture)
        {
            return new Vector2(texture.Width, texture.Height);
        }
        #endregion
    }
}
