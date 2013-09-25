using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace FighterPilot
{
    class ShipModule
    {
        List<ShipModule> attachedModules = new List<ShipModule>();
        public Vector2 offset = Vector2.Zero;
        public Vector2 tempOffset = Vector2.Zero;
        public Texture2D texture = null;
        public Rectangle textureSize;
        public Vector2 position = Vector2.Zero;//this should be relative to the core, not the world
        public Vector2 origin = Vector2.Zero;//
        public float rotation = 0f;
        public ModuleManager parent;

        public ShipModule(Vector2 inAttachment, ModuleManager inParent)
        {
            offset = inAttachment;
            parent = inParent;
            textureSize = inParent.coreRecSize;
            origin = new Vector2(textureSize.Width / 2, textureSize.Height / 2);
        }
        public virtual void LoadContent(GraphicsDevice inGraphics, ContentManager inContent)
        {
            texture = DrawShipModuleship(inGraphics, parent.team, textureSize);
            //texture = parent.MakeTexture(inGraphics, parent.team, textureSize);//can access parent draw method
        }
        public virtual void Draw(SpriteBatch inSpriteBatch)
        {
            inSpriteBatch.Draw(texture, position, textureSize, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0.5f);
            foreach (ShipModule mod in attachedModules)
            {
                mod.Draw(inSpriteBatch);
            }
        }
        public virtual void Update(GameTime gameTime)
        {
            UpdateModuleLocation(gameTime);
        }
        public void UpdateModuleLocation(GameTime gameTime)
        {
            this.rotation = parent.rotation;
            this.position = RotateAboutOrigin(this.offset, parent.MMPosition, rotation);
        }
        public Vector2 RotateAboutOrigin(Vector2 inpoint, Vector2 inorigin, float inrotation)
        {
            return Vector2.Transform(inpoint, Matrix.CreateRotationZ(inrotation)) + inorigin;
        } 
        private Texture2D DrawShipModuleship(GraphicsDevice inGraphics, enumTeam inTeamColor, Rectangle inTextureSize)
        {
            int inWidth = inTextureSize.Width;
            int inHeight = inTextureSize.Height;
            Texture2D tempTexture = new Texture2D(inGraphics, inWidth, inHeight);
            Color[] tempColor = new Color[inWidth * inHeight];
            for (int i = 0; i < inWidth * inHeight; i++)
            {
                if (i < inWidth*2 || i > inWidth * (inHeight - 2) || i % inWidth < 2 || i % inWidth > inWidth - 3)
                    tempColor[i] = new Color(64, 64, 64, 255);
                else
                    tempColor[i] = new Color(128, 128, 128, 255);
            }
            tempTexture.SetData<Color>(tempColor);
            return tempTexture;
        }
    }
}
