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
        string curText = "Hello";
        Color textColor = Color.Black;

        private short moodValue;
        private NegotiatorMood mood = NegotiatorMood.Neutral;
        private Dictionary<string, string> responses = new Dictionary<string, string>();
        private static Negotiator instance;

        private Negotiator(Vector2 position, float scale, float layer, Rectangle rect)
            : base(position, scale, layer, rect)
        {
            responses.Add("0", "kald det kaerlighed");
            responses.Add("1", "kald det lige hvad du vil");
            responses.Add("2", "ouuaaaaaah der findes ingen ord");
            responses.Add("3", "ingen ord der helt slaar til");
            responses.Add("4", "saa kald det lige hvad du vil");
            curText = responses["0"];
        }

        public static Negotiator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Negotiator(new Vector2(100, 10), 1, 1.0f, new Rectangle(0, 0, 20, 20));
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
        public void SwitchMood(int value)
        {
            if (Convert.ToInt32(mood) == 1)
            {
                mood += value;
            }
            else if (Convert.ToInt32(mood) == 0 && value >= 0)
            {
                mood += value;
            }
            else if (Convert.ToInt32(mood) == 2 && value <= 0)
            {
                mood += value;
            }

            switch (mood)
            {
                case NegotiatorMood.Dissatisfied:
                    textColor = Color.Red;
                    break;
                case NegotiatorMood.Neutral:
                    textColor = Color.Black;
                    break;
                case NegotiatorMood.Satisfied:
                    textColor = Color.Green;
                    break;
                default:
                    break;
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
            spriteBatch.DrawString(GameWorld.font, curText, position, Color.Black);
            spriteBatch.DrawString(GameWorld.font, mood.ToString(), new Vector2(0, 20), textColor);
            //base.Draw(spriteBatch);
        }
        public void SwitchText(string key)
        {
            if (responses.Count > GameWorld.RoundCounter)
                curText = responses[key];
        }
    }
}
