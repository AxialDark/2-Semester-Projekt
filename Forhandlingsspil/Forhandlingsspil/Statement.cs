using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forhandlingsspil
{
    class Statement : SpriteObject
    {
        private int salaryChangeValue;
        private short moodChangeValue;
        private string statementText;
        private StatementType type;

        public Statement(Vector2 position, float scale, float layer, Rectangle rect, StatementType type, string text)
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
