using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary;
using FighterPilot.Mountables;
using Microsoft.Xna.Framework.Content;

namespace FighterPilot.Module_Stuff
{
    class MountModule : ShipModule
    {
        public float thrust = 0f;
        //public Vector2 position;
        ModuleManager parent;


        public List<Mountable> mounted = new List<Mountable>();

        public MountModule(Vector2 inAttachment, ModuleManager inParent)
            : base(inAttachment, inParent)
        {
            this.position = inAttachment;
            parent = inParent;
            AddMountable(inParent.graphicsDevice);
        }
        public void LoadThrusterContent(GraphicsDevice inGraphics)
        {

        }
        public override void LoadContent(GraphicsDevice inGraphics, ContentManager inContent)
        {
            this.texture = inContent.Load<Texture2D>("tempMountModule");
            foreach (Mountable mod in mounted)
            {
                mod.LoadContent(inGraphics, inContent);
            }
        }
        public override void Update(GameTime gameTime)
        {
            UpdateModuleLocation(gameTime);
            foreach (Mountable mounts in mounted)
            {
                mounts.Update(gameTime);
            }
        }
        public override void Draw(SpriteBatch inSpriteBatch)
        {
            inSpriteBatch.Draw(texture, position, textureSize, Color.White, rotation, origin, 1.0f, SpriteEffects.None, .75f);
            foreach (Mountable mounts in mounted)
            {
                mounts.Draw(inSpriteBatch);
            }
            
        }
        public void AddParticles(Vector2 inposition, Vector2 inDirection, float inRotation)
        {
            //maybe some small particles for the bullet fire

            //EngineParticle particle = new EngineParticle(parent.graphicsDevice, SMPosition, rotation, new Rectangle(0, 0, 2, 2), /*velocity*/new Vector2(0, 0), /*enumTeam.RedTeam*/ Color.Red, 80);
            ////particle.LoadContent(epExhaTexture);
            //particles.Add(particle);
        }
        public void AddMountable(GraphicsDevice inGraphics)
        {
            MountableGun temp = new MountableGun(inGraphics, this);
            mounted.Add(temp);
        }
    }
}
