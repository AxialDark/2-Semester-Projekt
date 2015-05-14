using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forhandlingsspil
{
    class SpriteObject
    {
        protected Vector2 position;
        protected float scale;
        protected float layer;
        protected Rectangle rect;
        protected Texture2D texture;

        public SpriteObject(Vector2 position, float scale, float layer, Rectangle rect)
        {

        }

        public virtual void LoadContent(ContentManager content)
        {

        }
        public virtual void Update(GameTime gameTime) 
        {
 
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
