using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary;

namespace FighterPilot
{
    class ThrusterModule : ShipModule
    {
        public float thrust = 0f;
        //public Vector2 position;
        ModuleManager parent;

        public List<EngineParticle> particles = new List<EngineParticle>();

        public ThrusterModule(Vector2 inAttachment, ModuleManager inParent)
            : base(inAttachment, inParent)
        {
            this.position = inAttachment;
            this.parent = inParent;
        }
        public void LoadThrusterContent(GraphicsDevice inGraphics)
        {

        }
        public override void Update(GameTime gameTime)
        {
            UpdateModuleLocation(gameTime);
            AddParticles(this.position, new Vector2(0, 0), 0f);
            foreach (EngineParticle par in particles)
            {
                par.Update();
            }
        }
        public override void Draw(SpriteBatch inSpriteBatch)
        {
            
            foreach (EngineParticle par in particles)
            {
                par.Draw(inSpriteBatch);
            }
            inSpriteBatch.Draw(this.texture, this.position, this.textureSize, Color.White, this.rotation, this.origin, 1.0f, SpriteEffects.None, 0f);
        }
        public void AddParticles(Vector2 inposition, Vector2 inDirection, float inRotation)
        {
            EngineParticle particle = new EngineParticle(parent.graphicsDevice, position, rotation, new Rectangle(0, 0, 2, 2), /*velocity*/new Vector2(0, 0), /*enumTeam.RedTeam*/ Color.Red, 80);
            //particle.LoadContent(epExhaTexture);
            particles.Add(particle);
        }
    }
}
