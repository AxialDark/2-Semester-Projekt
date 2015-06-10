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
        #region Fields
        protected Vector2 position;
        protected float scale;
        protected float layer;
        protected Vector2 origin;
        protected Rectangle rect;
        protected Texture2D texture;
        protected Color color;
        #endregion

        public SpriteObject(Vector2 position, float scale, float layer, Rectangle rect)
        {
            this.position = position;
            this.scale = scale;
            this.layer = layer;
            this.rect = rect;
            this.origin = Vector2.Zero;
            this.color = Color.White;
        }

        public virtual void LoadContent(ContentManager content)
        {

        }
        public virtual void Update(GameTime gameTime) 
        {
 
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rect, color, 0f, origin, scale, SpriteEffects.None, layer);
        }
    }
}
