#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameLibrary;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
#endregion
namespace FighterPilot
{
    class Ship
    {
        #region Graphic Variables
        //private Texture2D texture = null;
        private Rectangle textureSize = Rectangle.Empty;
        public Vector2 SPosition = Vector2.Zero;
        public Vector2 origin = Vector2.Zero;//
        public Vector2 velocity = Vector2.Zero;
        private Texture2D bulletTexture;//need to do something with this - might not need this - laser module holds texture
        private Texture2D epExhaTexture;//need to do something with this
        public ModuleManager thisShip;// = new ModuleManager();
        public GraphicsDevice graphicsDevice;
        #endregion

        #region Behavior Variables
        public enumFighterState fighterState = enumFighterState.Null;
        public float _Rotation = 0f;
        public float _DesiRotation = 0f;
        public float _TempRotation = 0f;
        public float _Speed = 1f;
        public float maxSpeed = 3f;
        public Ship target;
        #endregion
        
        #region Other Variables
        public int fireCounter = 0;
        public List<Bullet> bullets = new List<Bullet>();
        public List<EngineParticle> particles = new List<EngineParticle>();//moved to modulemanager
        #endregion

        #region Team Variables
        public enumTeam team = enumTeam.Unassigned;
        public Color teamColor;
        #endregion

        #region Max Values
        public float maxRotationSpeed = .025f;
        public float maxAcceleration = .025f;
        #endregion

        #region Properties
        public float currRotation
        {
            get
            {
                return this._Rotation;
            }
            set
            {
                this._Rotation = value;
                if (this._Rotation > MathHelper.Pi * 2)
                    this._Rotation = this._Rotation % (MathHelper.Pi * 2);
                if (this._Rotation < 0)
                    this._Rotation = this._Rotation + (MathHelper.Pi * 2);
            }
        }
        public float desiRotation
        {
            get
            {
                return _DesiRotation;
            }
            set
            {
                _DesiRotation = value;
                if (_DesiRotation > MathHelper.Pi * 2)
                    _DesiRotation = _DesiRotation % (MathHelper.Pi * 2);
                if (_DesiRotation < 0)
                    _DesiRotation = _DesiRotation + (MathHelper.Pi * 2);
            }
        }
        public float temrotation
        {
            get
            {
                return _TempRotation;
            }
            set
            {
                _TempRotation = value;
                if (_TempRotation > MathHelper.Pi * 2)
                    _TempRotation = _TempRotation % (MathHelper.Pi * 2);
                if (_TempRotation < 0)
                    _TempRotation = _TempRotation + (MathHelper.Pi * 2);
            }
        }
        public float speed
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
                if (_Speed > maxSpeed)
                    _Speed = maxSpeed;
            }
        }
        #endregion

        public Ship(Random random)
        {
            SPosition = new Vector2(random.Next(100, 3900), random.Next(100, 3900));
            //this.SPosition = new Vector2(1000, 1000);
            thisShip = new ModuleManager(graphicsDevice,enumTeam.GreenTeam, enumCoreSize.Medium, this);
            team = enumTeam.RedTeam;
            switch (team)
            {
                case enumTeam.RedTeam: teamColor = new Color(255, 0, 0, 0); break;
                case enumTeam.GreenTeam: teamColor = new Color(0, 255, 0, 0); break;
                case enumTeam.BlueTeam: teamColor = new Color(0, 0, 255, 0); break;
                case enumTeam.Unassigned: teamColor = new Color(127, 127, 127, 0); break;
            }
        }
        #region Graphic Methods
        public void LoadContent(GraphicsDevice inGraphics, ContentManager inContent)
        {
            

            graphicsDevice = inGraphics;
            thisShip.LoadContent(inGraphics, inContent);
            //this.texture = inContent.Load<Texture2D>("BlueFighter");
            this.bulletTexture = inContent.Load<Texture2D>("Bullet");
            this.epExhaTexture = inContent.Load<Texture2D>("Exhaust");
            origin = thisShip.origin;//GetFrameCenter(texture);
            //this.position = new Vector2(200, 200);
            this.currRotation = 0f ;
            this.velocity = new Vector2(1, 0);

        }
        public void Draw(SpriteBatch inSpriteBatch)
        {
            foreach (EngineParticle p in particles)
            {
                p.Draw(inSpriteBatch);
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].bDead == true)
                    bullets.RemoveAt(i);
                else
                    bullets[i].Draw(inSpriteBatch);
            }
            thisShip.Draw(inSpriteBatch);
            //inSpriteBatch.Draw(texture, position, textureSize, Color.White, currRotation, origin, 1.0f, SpriteEffects.None, 0f);
        }
        public void Update(GameTime gameTime)
        {
            this.velocity.X = (float)Math.Sin(-currRotation + (Math.PI));
            this.velocity.Y = (float)Math.Cos(-currRotation + (Math.PI));
            SPosition += velocity * speed;

            thisShip.Update(gameTime, SPosition, currRotation);

            //#region fire Bullets
            //if (fireCounter > 0) fireCounter--;//you can hold down the spacebar to fire, but there is a counter
            //if (InputHandlerKeyboard.KeyDown(Keys.Space) && fireCounter == 0) ShootBullet(currRotation);
            //foreach (Bullet b in bullets)
            //{
            //    b.Update();
            //}
            //#endregion
        }
        public Vector2 GetFrameCenter(Texture2D texture)
        {
            int frameWidth = (int)GetFrameDimensions(texture).X;
            int frameHeight = (int)GetFrameDimensions(texture).Y;
            textureSize = new Rectangle(0, 0, frameWidth, frameHeight);
            return new Vector2(frameWidth / 2, frameHeight / 2);
        }
        public Vector2 GetFrameDimensions(Texture2D texture)
        {
            return new Vector2(texture.Width, texture.Height);
        }
        #endregion

        #region Fighter Methods//needs to have different inplementation for turrets
        public void ShootBullet(float inRotation)
        {
            if (fireCounter > 0)
                fireCounter--;//you can hold down the spacebar to fire, but there is a counter
            else if (UtilityFunctions.CalculateDistance(SPosition, target.SPosition) < 500 && fireCounter <= 0)
            {
                fireCounter = 30;
                Bullet bullet = new Bullet(SPosition, velocity, currRotation, speed);
                bullet.LoadContent(bulletTexture);
                bullets.Add(bullet);
            }
        }
        #endregion

    }
}
