using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forhandlingsspil
{
    class NegotiatingTrick : SpriteObject
    {
        private bool isTalkWithColleague;
        private bool isTradeUnion;
        private int salaryChangeValue;
        private short moodChangeValue;
        private string useText;

        public NegotiatingTrick(Vector2 position, float scale, float layer, Rectangle rect)
            : base(position, scale, layer, rect)
        {

        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
