using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forhandlingsspil
{
    class Player : SpriteObject
    {
        private Dictionary<string, Statement> honestDic = new Dictionary<string, Statement>();
        private Dictionary<string, Statement> humorousDic = new Dictionary<string, Statement>();
        private Dictionary<string, Statement> sneakyDic = new Dictionary<string, Statement>();
        private int salary;
        private NegotiatingTrick negotiatingTrick;
        private Player instance;

        private Player(Vector2 position, float scale, float layer, Rectangle rect)
            : base(position, scale, layer, rect)
        {

        }

        public Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player(new Vector2(0, 0), 1, 1.0f, new Rectangle(0, 0, texture.Width, texture.Height));
                }
                return instance;
            }
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
