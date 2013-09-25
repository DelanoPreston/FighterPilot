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
    class Mountable
    {
        public Vector2 position = Vector2.Zero;
        public Texture2D texture = null;
        public Rectangle textureSize = Rectangle.Empty;
        public Vector2 origin = Vector2.Zero;
        public MountModule parent;

        public Mountable(GraphicsDevice inGraphics, MountModule inParent)
        {
            this.parent = inParent;
            this.position = inParent.position + parent.position;
            
        }
        public virtual void LoadContent(GraphicsDevice inGraphics, ContentManager inContent)
        {

        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch inSpriteBatch)
        {

        }
    }
}
