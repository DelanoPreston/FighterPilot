using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameLibrary
{
    public class EngineParticle
    {
        #region Variables
        public Texture2D texture = null;
        public Rectangle textureSize = Rectangle.Empty;//
        public int tTL;//time to live
        public float rotation = 0f;
        public float rotationSpeed = 0f;
        public Vector2 position = Vector2.Zero;
        public Vector2 origin = Vector2.Zero;//
        public Vector2 velocity = Vector2.Zero;
        public bool dead;
        public Color color = new Color(0, 0, 255, 255);
        #endregion

        #region Constructors
        public EngineParticle(GraphicsDevice inGraphics, Vector2 inPosition, float inRotation, Rectangle inSize,
            Vector2 inParentVelocity, Color inColor, int inTTL, Texture2D inTexture, 
            float rotationSpeed)
        {
            texture = inTexture;
            textureSize = inSize;
            position = inPosition;
            rotation = inRotation;
            velocity = inParentVelocity;
            color = inColor;
            tTL = inTTL;
            dead = false;
        }
        /// <summary>
        /// you do not need to input a texture for this, this is mostly for engine exhaust
        /// </summary>
        /// <param name="inPosition"></param>
        /// <param name="inRotation"></param>
        /// <param name="inParentVelocity"></param>
        /// <param name="inColor"></param>put the team color here
        /// <param name="inTTL"></param>
        public EngineParticle(GraphicsDevice inGraphics, Vector2 inPosition, float inRotation, Rectangle inSize,
            Vector2 inParentVelocity, Color inColor, int inTTL)
        {
            texture = ConstructTexture2D(inGraphics, inSize, inColor);
            textureSize = inSize;
            origin = GetFrameCenter(texture);
            position = inPosition;
            rotation = inRotation;
            velocity = inParentVelocity;
            color = inColor;
            tTL = inTTL;
            dead = false;

        }
        public EngineParticle(GraphicsDevice inGraphics, Vector2 inPosition, float inRotation, Rectangle inSize,
            Vector2 inParentVelocity)
        {
            textureSize = inSize;
            position = inPosition;
            rotation = inRotation;
            tTL = 125;
            dead = false;
        }
        #endregion

        #region Graphic Methods
        public void LoadContent(Texture2D inTexture)
        {
            this.texture = inTexture;
            origin = GetFrameCenter(inTexture);
        }
        public void Draw(SpriteBatch inSpriteBatch)
        {
            if (!dead)
                inSpriteBatch.Draw(texture, position, null, color, rotation, origin, 1.0f, SpriteEffects.None, 0f);
        }
        public Texture2D ConstructTexture2D(GraphicsDevice inGraphics, Rectangle inSize, Color inColor)
        {
            int width = inSize.Width;
            int height = inSize.Height;
            Texture2D testTexture = new Texture2D(inGraphics, width, height);
            Color[] color = new Color[width * height];
            for (int i = 0; i < width * height; i++)
            {
                color[i] = inColor;
            }
            testTexture.SetData<Color>(color);
            return testTexture;
        }
        public void Update()
        {
            if (color.R > 0) { color.R--; color.R--; color.R--; }
            if (color.B > 0) { color.B--; color.B--; color.B--; }
            if (color.G > 0) { color.G--; color.G--; color.G--; }
            if (color.R < 3 && color.B < 3 && color.G < 3)
                dead = true;

            //or something with TTL
        }
        private Vector2 GetFrameCenter(Texture2D texture)
        {
            int frameWidth = (int)GetFrameDimensions(texture).X;
            int frameHeight = (int)GetFrameDimensions(texture).Y;
            textureSize = new Rectangle(0, 0, frameWidth, frameHeight);
            return new Vector2(frameWidth / 2, frameHeight / 2);
        }
        private Vector2 GetFrameDimensions(Texture2D texture)
        {
            return new Vector2(texture.Width, texture.Height);
        }
        #endregion
    }
}