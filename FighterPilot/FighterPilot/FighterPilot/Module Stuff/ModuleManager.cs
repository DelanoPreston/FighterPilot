using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary;
using FighterPilot.Module_Stuff;
using Microsoft.Xna.Framework.Content;

namespace FighterPilot
{
    class ModuleManager
    {
        public Texture2D textureCore;
        public Rectangle coreRecSize;
        public List<ShipModule> modules = new List<ShipModule>();
        public enumCoreSize coreSize = enumCoreSize.Small;
        public Vector2[] attachmentNodes;
        public Vector2 MMPosition = Vector2.Zero;
        public Vector2 origin = Vector2.Zero;
        public float rotation = 0f;
        public enumTeam team;
        public GraphicsDevice graphicsDevice;
        public Ship parent;
        //public List<EngineParticle> particles = new List<EngineParticle>();

        public ModuleManager(GraphicsDevice inGraphics, enumTeam inTeamColor, enumCoreSize inCoreSize, Ship inParent)
        {
            //graphicsDevice = inGraphics;
            MMPosition = new Vector2(500, 900);
            coreRecSize = GetCoreSize(inCoreSize);
            parent = inParent;
            origin = new Vector2(coreRecSize.Width/2, coreRecSize.Height/2);
            rotation = 0f;
            team = inTeamColor;
        }

        public void LoadContent(GraphicsDevice inGraphics, ContentManager inContent)
        {
            

            graphicsDevice = inGraphics;
            textureCore = MakeTexture(inGraphics, team, coreRecSize);
            attachmentNodes = FindAttachmentPoints(rotation);
            //modules.Add(new ShipModule(attachmentNodes[0], this));

            //modules.Add(new ShipModule(new Vector2(16, 0), this));//front
            //modules.Add(new ShipModule(new Vector2(-16, 0), this));//end one
            //modules.Add(new ShipModule(new Vector2(0, 16), this));//
            //modules.Add(new ShipModule(new Vector2(0, -16), this));//

            modules.Add(new ShipModule(new Vector2(-16, -16), this));//front
            modules.Add(new ShipModule(new Vector2(16, -16), this));//front
            modules.Add(new ShipModule(new Vector2(0, -32), this));//front
            modules.Add(new ShipModule(new Vector2(16, 16), this));//front
            modules.Add(new ShipModule(new Vector2(-16, 16), this));//front
            modules.Add(new ThrusterModule(new Vector2(16, 32), this));//front
            modules.Add(new ThrusterModule(new Vector2(-16, 32), this));//front
            modules.Add(new ShipModule(new Vector2(32, 16), this));//front
            modules.Add(new ShipModule(new Vector2(-32, 16), this));//front

            modules.Add(new MountModule(attachmentNodes[0], this));//front
            modules.Add(new MountModule(attachmentNodes[1], this));//end one
            modules.Add(new ShipModule(attachmentNodes[2], this));//end one
            modules.Add(new MountModule(attachmentNodes[3], this));//

            foreach (ShipModule mod in modules)
            {
                mod.LoadContent(inGraphics, inContent);
            }
        }
        public void Draw(SpriteBatch inSpriteBatch)
        {
            inSpriteBatch.Draw(textureCore, MMPosition, coreRecSize, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0f);
            foreach(ShipModule mod in modules)
            {
                mod.Draw(inSpriteBatch);
            }
        }
        public void Update(GameTime gameTime, Vector2 inPosition, float inRotation)
        {
            this.MMPosition = parent.SPosition;
            this.rotation = parent.currRotation;
            attachmentNodes = FindAttachmentPoints(inRotation);
            foreach (ShipModule mod in modules)
            {
                mod.Update(gameTime);
            }
        }
        public void AddModule(ShipModule inModule, int inSlotPosition)
        {
            //modules.Add(inModule);
            //modules.Add(new ThrusterModule(this));
        }
        public Rectangle GetCoreSize(enumCoreSize inCoreSize)
        {
            Rectangle temp;
            switch (inCoreSize)
            {
                case enumCoreSize.Small: temp = new Rectangle(0, 0, 8, 8); break;
                case enumCoreSize.Medium: temp = new Rectangle(0, 0, 16, 16); break;
                case enumCoreSize.Large: temp = new Rectangle(0, 0, 24, 24); break;
                default: temp = new Rectangle(0, 0, 8, 8); break;
            }
            return temp;
        }
        public Texture2D MakeTexture(GraphicsDevice inGraphics, enumTeam inTeamColor, Rectangle inTextureSize)
        {
            int inWidth = inTextureSize.Width;
            int inHeight = inTextureSize.Height;
            
            Color[] tempColor = new Color[inWidth * inHeight];
            for (int i = 0; i < inWidth * inHeight; i++)
            {
                //if (i < inWidth * 2 || i > inWidth * (inHeight - 2) || i % inWidth < 2 || i % inWidth > inWidth - 3)
                if (i < inWidth * 2 || i > inWidth * (inWidth - 2) || i % inWidth < 2 || i % inWidth > inWidth - 3)
                    tempColor[i] = new Color(255, 0, 0, 255); //(64, 64, 64, 64);
                else
                    tempColor[i] = new Color(128, 128, 128, 255);
            }
            Texture2D tempTexture = new Texture2D(inGraphics, inWidth, inHeight, false, SurfaceFormat.Color);
            tempTexture.SetData<Color>(tempColor);
            return tempTexture;
        }
        public Vector2[] CalcAttachmentPoints(float inRotation)
        {
            Vector2[] temp = new Vector2[4];

            //this.velocity.X = (float)Math.Sin(-rotation + (Math.PI));
            //this.velocity.Y = (float)Math.Cos(-rotation + (Math.PI));

            //temp[0] = new Vector2(origin.Y +/* (textureCore.Width) */ (float)Math.Cos(-rotation + (Math.PI)), origin.Y + (textureCore.Height) * (float)Math.Sin(-rotation + (Math.PI)));
            //temp[1] = new Vector2(origin.Y + (textureCore.Width) * (float)Math.Cos(-rotation + (Math.PI)), 0);
            //temp[2] = new Vector2(0, origin.Y - (textureCore.Height) * (float)Math.Sin(-rotation + (Math.PI)));
            //temp[3] = new Vector2(origin.Y - (textureCore.Width) * (float)Math.Cos(-rotation + (Math.PI)), 0);

            temp[0] = new Vector2(8, origin.Y + (float)(textureCore.Height));
            temp[1] = new Vector2(origin.Y + (float)(textureCore.Width), 8);
            temp[2] = new Vector2(8, origin.Y - (float)(textureCore.Height));
            temp[3] = new Vector2(origin.Y - (float)(textureCore.Width), 8);
            return temp;
        }
        public Vector2[] FindAttachmentPoints(float inRotation)
        {
            Vector2[] temp = new Vector2[4];

            //this.velocity.X = (float)Math.Sin(-rotation + (Math.PI));
            //this.velocity.Y = (float)Math.Cos(-rotation + (Math.PI));

            //temp[0] = new Vector2(origin.Y +/* (textureCore.Width) */ (float)Math.Cos(-rotation + (Math.PI)), origin.Y + (textureCore.Height) * (float)Math.Sin(-rotation + (Math.PI)));
            //temp[1] = new Vector2(origin.Y + (textureCore.Width) * (float)Math.Cos(-rotation + (Math.PI)), 0);
            //temp[2] = new Vector2(0, origin.Y - (textureCore.Height) * (float)Math.Sin(-rotation + (Math.PI)));
            //temp[3] = new Vector2(origin.Y - (textureCore.Width) * (float)Math.Cos(-rotation + (Math.PI)), 0);

            //temp[0] = new Vector2(0, origin.Y + (float)(textureCore.Height ));
            //temp[1] = new Vector2(origin.X + (float)(textureCore.Width ), 0);
            //temp[2] = new Vector2(0, origin.Y - (float)(textureCore.Height ));
            //temp[3] = new Vector2(origin.X - (float)(textureCore.Width ), 0);

            temp[0] = new Vector2(0, -16);
            temp[1] = new Vector2(16, 0);
            temp[2] = new Vector2(0, 16);
            temp[3] = new Vector2(-16, 0);
            return temp;
        }
        public void AddParticles(Vector2 inposition, Vector2 inDirection, float inRotation)
        {
            //EngineParticle particle = new EngineParticle(graphicsDevice, MMPosition, rotation, new Rectangle(0, 0, 2, 2), /*velocity*/new Vector2(0,0), /*enumTeam.RedTeam*/ Color.Red, 80);
            //particle.LoadContent(epExhaTexture);
            //particles.Add(particle);
        }
    }
}
