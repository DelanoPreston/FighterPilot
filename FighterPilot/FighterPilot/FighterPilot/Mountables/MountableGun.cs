using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary;
using FighterPilot.Module_Stuff;
using Microsoft.Xna.Framework.Content;

namespace FighterPilot.Mountables
{
    class MountableGun : Mountable
    {
        public float rateOfFire = 0f;
        public float rotation = 0f;

        public MountableGun(GraphicsDevice inGraphics, MountModule inParent)
            : base(inGraphics,inParent)
        {
            
        }
        public override void LoadContent(GraphicsDevice inGraphics, ContentManager inContent)
        {
            this.texture = inContent.Load<Texture2D>("FitTexSizeMountableGun");
            textureSize = UtilityFunctions.GetTexSizeRec(texture);
            origin = new Vector2(textureSize.Width / 2, textureSize.Height - (textureSize.Width / 2));
        }
        public override void Update(GameTime gameTime)
        {
            if (parent.parent.parent.target != null)
                rotation = UtilityFunctions.CalculateAngle(parent.parent.MMPosition, parent.parent.parent.target.SPosition);
            //rotation += .01f;
            
        }
        public override void Draw(SpriteBatch inSpriteBatch)
        {
            inSpriteBatch.Draw(this.texture, parent.position, this.textureSize, Color.White, this.rotation, this.origin, 1.0f, SpriteEffects.None, 0f);
        }
        
    }
}
