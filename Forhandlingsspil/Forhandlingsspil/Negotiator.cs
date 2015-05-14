using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forhandlingsspil
{
    class Negotiator : SpriteObject
    {
        private short moodValue;
        private NegotiatorMood mood;
        private Dictionary<string, string> responses = new Dictionary<string, string>();
        private Negotiator instance;

        private Negotiator(Vector2 position, float scale, float layer, Rectangle rect)
            : base(position, scale, layer, rect)
        {

        }

        public Negotiator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Negotiator(new Vector2(0, 0), 1, 1.0f, new Rectangle(0, 0, texture.Width, texture.Height));
                }
                return instance;
            }
        }
        public Dictionary<string, string> Responses
        {
            get
            {
                return responses;
            }
        }
        public short MoodValue
        {
            get { return moodValue; }
            set { moodValue = value; }
        }

        public void CheckMood()
        {

        }
        private void SwitchMood()
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
