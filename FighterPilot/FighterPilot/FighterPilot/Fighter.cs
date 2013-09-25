using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameLibrary;

namespace FighterPilot
{
    class Fighter
    {
        #region Variables
        public Texture2D pTexture = null;//
        public Texture2D bTexture = null;//not yet

        public Texture2D epExhaTexture = null;

        public Rectangle pTextureSize = Rectangle.Empty;//
        public float _Rotation = 0f;
        public int fireCounter = 0;//-------------------------------------
        public Vector2 pPosition = Vector2.Zero;
        public Vector2 pOrigin = Vector2.Zero;//
        public Vector2 pDirection = Vector2.Zero;
        public float _Speed = 0f;
        public List<Bullet> pBullets = new List<Bullet>();
        public List<EngineParticle> pParticles = new List<EngineParticle>();

        public Vector2 targetDirection = Vector2.Zero;
        #endregion

        #region Properties
        public float pRotation
        {
            get
            {
                return _Rotation;
            }
            set
            {
                _Rotation = value;
                if (_Rotation > 1)
                    _Rotation = _Rotation % (MathHelper.Pi * 2);
                if (_Rotation < 0)
                    _Rotation = _Rotation + (MathHelper.Pi * 2);
            }
        }
        public float pSpeed
        {
            get
            {
                return _Speed;
            }
            set
            {
                _Speed = value;
                if (_Speed < 0f)
                    _Speed = 0f;
                if (_Speed > 3f)
                    _Speed = 3f;
            }
        }
        #endregion

        public Fighter()
        {
            pPosition = new Vector2(1000, 1000);
        }    
        #region Graphic Methods
        public void LoadContent(ContentManager inContent)
        {
            this.pTexture = inContent.Load<Texture2D>("Fighter");
            this.bTexture = inContent.Load<Texture2D>("Bullet");
            this.epExhaTexture = inContent.Load<Texture2D>("EngineExhaust");
            pOrigin = GetFrameCenter(pTexture);
            //this.pPosition = new Vector2(200, 200);
            this.pRotation = MathHelper.Pi / 2;
            this.pDirection = new Vector2(1,0);
        }
        public void Draw(SpriteBatch inSpriteBatch)
        {
            foreach (EngineParticle p in pParticles)
            {
                p.Draw(inSpriteBatch);
            }
            foreach (Bullet b in pBullets)
            {
                b.Draw(inSpriteBatch);
            }
            inSpriteBatch.Draw(pTexture, pPosition, null, Color.White, pRotation, pOrigin, 1.0f, SpriteEffects.None, 0f);
        }
        public void Update(GameTime gameTime)
        {
            pRotation = (float)Math.Asin((double)(pPosition.Y - targetDirection.Y));
            pPosition = pPosition + pDirection * pSpeed;
            //pPosition += pDirection;
            if (fireCounter > 0)
                fireCounter--;//you can hold down the spacebar to fire, but there is a counter
            if (InputHandlerKeyboard.KeyDown(Keys.Space) && fireCounter == 0)
                ShootBullet(pRotation);
            foreach (Bullet b in pBullets)
            {
                b.Update();
            }
            AddParticles(pPosition, pDirection, pRotation);
            foreach(EngineParticle p in pParticles)
            {
                //if(p.epDead == true)
                //    pParticles.Remove(p);
                //else
                    p.Update();
            }
        }
        private Vector2 GetFrameCenter(Texture2D texture)
        {
            int frameWidth = (int)GetFrameDimensions(texture).X;
            int frameHeight = (int)GetFrameDimensions(texture).Y;
            pTextureSize = new Rectangle(0, 0, frameWidth, frameHeight);
            return new Vector2(frameWidth / 2, frameHeight / 2);
        }
        private Vector2 GetFrameDimensions(Texture2D texture)
        {
            return new Vector2(texture.Width, texture.Height);
        }
        #endregion

        #region Fighter Methods
        public void ShootBullet(float inRotation)
        {
            fireCounter = 10;
            Bullet bullet = new Bullet(pPosition, pDirection, pRotation, pSpeed);
            bullet.LoadContent(bTexture);
            pBullets.Add(bullet);
            bullet = new Bullet(pPosition, pDirection, pRotation, pSpeed);
            bullet.LoadContent(bTexture);
            pBullets.Add(bullet);
        }
        public void AddParticles(Vector2 inPosition, Vector2 inDirection, float inRotation)
        {
            //EngineParticle particle = new EngineParticle(Globals.graphics.GraphicsDevice, inPosition, inDirection, inRotation);
            //particle.LoadContent(epExhaTexture);
            //pParticles.Add(particle);
        }

        #endregion

    }
}
