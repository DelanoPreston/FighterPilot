#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameLibrary;
#endregion
#region Notes
//maybe a current target Timer - impatient bad guys (change target if one is nearby after timer is up)
#endregion
namespace FighterPilot
{
    class NPC : Ship
    {
        #region Variables
        //public Texture2D enemyTexture = null;//
        public Texture2D bulletTexture = null;
        public enumFighterState fighterState = enumFighterState.Follow;
        //public Texture2D epExhaTexture = null;
        //public float _Rotation = 0f;
        //public float _DesRotation = 0f;
        //public float _TempRotation = 0f;
        //public float _Speed = 1f;
        //public Rectangle pTextureSize = Rectangle.Empty;//
        public int fireCounter = 0;
        

        public int counter = 0;
        #endregion

        #region Properties
        public float eCurrRotation
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
        public float eDesiRotation
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
        public float eTempRotation
        {
            get
            {
                return this._TempRotation;
            }
            set
            {
                this._TempRotation = value;
                if (this._TempRotation > MathHelper.Pi * 2)
                    this._TempRotation = this._TempRotation % (MathHelper.Pi * 2);
                if (this._TempRotation < 0)
                    this._TempRotation = this._TempRotation + (MathHelper.Pi * 2);
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
                if (_Speed > 1.5f)
                    _Speed = 1.5f;
            }
        }
        #endregion

        public NPC(Random random)
            : base(random)
        {
            
        }
        #region Graphic Methods
        public void Update(GameTime gameTime, Ship inTargetPosition)
        {
            #region NPC State
            target = inTargetPosition;
            switch (fighterState)
            {
                case enumFighterState.Arrive:

                    break;
                case enumFighterState.Follow:
                    eDesiRotation = UtilityFunctions.CalculateAngle(SPosition, target.SPosition);
                    break;
                case enumFighterState.Flee:
                    eDesiRotation = UtilityFunctions.CalculateAngle(SPosition, target.SPosition) + (float)Math.PI;
                    break;
            }
            #endregion

            #region Follow Player Stuff
            eTempRotation = eCurrRotation - eDesiRotation;
            if (_TempRotation <= (float)Math.PI && _TempRotation > 0f)
                eCurrRotation -= maxRotationSpeed;
            else
                eCurrRotation += maxRotationSpeed;

            this.velocity.X = (float)Math.Sin(-eCurrRotation + Math.PI);
            this.velocity.Y = (float)Math.Cos(-eCurrRotation + Math.PI);

            SPosition += velocity * pSpeed;
            #endregion

            BulletBehavior(target.SPosition);

            this.velocity.X = (float)Math.Sin(-currRotation + (Math.PI));
            this.velocity.Y = (float)Math.Cos(-currRotation + (Math.PI));
            SPosition += velocity * speed;

            thisShip.Update(gameTime, SPosition, currRotation);
        }
        #endregion
        private void BulletBehavior(Vector2 inTargetPosition)
        {
            
                ShootBullet(eCurrRotation);

            foreach (Bullet b in bullets)
            {
                b.Update();
            }
        }
    }
}
