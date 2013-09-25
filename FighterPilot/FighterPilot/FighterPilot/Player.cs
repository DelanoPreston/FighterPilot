#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using GameLibrary;
#endregion
namespace FighterPilot
{
    class Player : Ship
    {
        #region Variables
        public Texture2D pTexture = null;//
        public Texture2D bTexture = null;
        public Texture2D epExhaTexture = null;

        public Rectangle pTextureSize = Rectangle.Empty;//
        public float _Rotation = 0f;
        public int fireCounter = 0;
        //public Vector2 pPosition = Vector2.Zero;
        public Vector2 pOrigin = Vector2.Zero;//
        public Vector2 pVelocity = Vector2.Zero;
        public float _Speed = 0f;
        public List<Bullet> pBullets = new List<Bullet>();
        public List<EngineParticle> pParticles = new List<EngineParticle>();
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
                if (_Rotation > MathHelper.Pi *2)
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
                if (_Speed > 2.5f)
                    _Speed = 2.5f;
            }
        }
        #endregion

        public Player(Random random)
            : base(random)
        {
            //pPosition = new Vector2(1000, 800);

        }

        #region Graphic Methods
        //public void LoadContent(GraphicsDevice inGraphics, ContentManager inContent)
        //{
        //    this.pTexture = inContent.Load<Texture2D>("BlueFighter");
        //    this.bTexture = inContent.Load<Texture2D>("Bullet");
        //    this.epExhaTexture = inContent.Load<Texture2D>("Exhaust");
        //    pOrigin = GetFrameCenter(pTexture);
        //    //this.pPosition = new Vector2(200, 200);
        //    this.pRotation = MathHelper.Pi / 2;
        //    this.pVelocity = new Vector2(1,0);
        //}
        //public void Draw(SpriteBatch inSpriteBatch)
        //{
        //    foreach (EngineParticle p in pParticles)
        //    {
        //        p.Draw(inSpriteBatch);
        //    }
        //    for (int i = 0; i < pBullets.Count; i++)
        //    {
        //        if (pBullets[i].bDead == true)
        //            pBullets.RemoveAt(i);
        //        else
        //            pBullets[i].Draw(inSpriteBatch);
        //    }
        //    inSpriteBatch.Draw(pTexture, pPosition, pTextureSize, Color.White, pRotation, pOrigin, 1.0f, SpriteEffects.None, 0f);
        //}
        public void PlayerInput(GameTime gameTime)
        {
            #region speed and rotation controls
            if (InputHandlerKeyboard.KeyDown(Keys.Right) || InputHandlerKeyboard.KeyDown(Keys.D))
            {
                currRotation += maxRotationSpeed;
            }
            if (InputHandlerKeyboard.KeyDown(Keys.Left) || InputHandlerKeyboard.KeyDown(Keys.A))
            {
                currRotation -= maxRotationSpeed;
            }
            if (InputHandlerKeyboard.KeyDown(Keys.Up) || InputHandlerKeyboard.KeyDown(Keys.W))
            {
                speed += maxAcceleration;
            }
            if (InputHandlerKeyboard.KeyDown(Keys.Down) || InputHandlerKeyboard.KeyDown(Keys.S))
            {
                speed -= maxAcceleration;
            }
            #endregion

            Update(gameTime);
        }
        #endregion
    }
}
