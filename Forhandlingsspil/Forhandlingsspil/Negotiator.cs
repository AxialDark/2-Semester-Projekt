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
            //responses.Add("0", "kald det kaerlighed");
            //responses.Add("1", "kald det lige hvad du vil");
            //responses.Add("2", "ouuaaaaaah der findes ingen ord");
            //responses.Add("3", "ingen ord der helt slaar til");
            //responses.Add("4", "saa kald det lige hvad du vil");

            responses.Add("S1", "Sat1");
            responses.Add("N1", "Neu1");
            responses.Add("D1", "Dis1");

            responses.Add("S2", "Sat2");
            responses.Add("N2", "Neu2");
            responses.Add("D2", "Dis2");

            responses.Add("S3", "Sat3");
            responses.Add("N3", "Neu3");
            responses.Add("D3", "Dis3");

            responses.Add("S4", "Sat4");
            responses.Add("N4", "Neu4");
            responses.Add("D4", "Dis4");

            responses.Add("S5", "Sat5");
            responses.Add("N5", "Neu5");
            responses.Add("D5", "Dis5");

            responses.Add("S6", "Sat6");
            responses.Add("N6", "Neu6");
            responses.Add("D6", "Dis6");

            curText = "Hello!";
        }

        /// <summary>
        /// Used for the singleton design pattern
        /// </summary>
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
        /// <summary>
        /// Switches the Negotiators mood based on the parameter
        /// </summary>
        /// <param name="value">Used in changing the Negotiators mood. Range from (-1 to 1)</param>
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
        /// <summary>
        /// Used to load content when the game starts
        /// </summary>
        /// <param name="content">From the monogame framework, used to load the content</param>
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }
        /// <summary>
        /// Used to Update the variables in the Negotiator class
        /// </summary>
        /// <param name="gameTime">From the monogame framework, counts the time</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        /// <summary>
        /// Used to draw in the Negotiator class
        /// </summary>
        /// <param name="spriteBatch">From the monogame framework used to draw</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameWorld.font, curText, new Vector2(300, 12), Color.Black);
            if (!GameWorld.gameOver)
                spriteBatch.DrawString(GameWorld.font, mood.ToString(), new Vector2(0, 20), textColor);
            //base.Draw(spriteBatch);
        }
        /// <summary>
        /// Switches the Negotiators response
        /// </summary>
        /// <param name="key">The last half of the key for the Dictionary</param>
        public void SwitchText(string key)
        {
            string newText = string.Empty;

            if (mood == NegotiatorMood.Satisfied)
                newText = "S";
            else if (mood == NegotiatorMood.Neutral)
                newText = "N";
            else if (mood == NegotiatorMood.Dissatisfied)
                newText = "D";

            newText += key;

            curText = responses[newText];

            if (!GameWorld.gameOver)
                Player.Instance.SwitchStatements(key);
        }
        /// <summary>
        /// Gets a key from the chosen Statement and uses it to change the Negotiators response accordingly
        /// </summary>
        /// <param name="key">The Key from the chosen Statement</param>
        public void SwitchResponse(string key)
        {
            switch (key)
            {
                case "HO0":
                    SwitchText("1");
                    break;
                case "HU0":
                    SwitchText("1");
                    break;
                case "S0":
                    SwitchText("4");
                    break;
                case "HO1":
                    SwitchText("2");
                    break;
                case "HU1":
                    SwitchText("2");
                    break;
                case "S1":
                    SwitchText("3");
                    break;
                case "HO2":
                    GameWorld.gameOver = true;
                    SwitchText("6");
                    break;
                case "S2":
                    GameWorld.gameOver = true;
                    SwitchText("6");
                    break;
                case "HO3":
                    SwitchText("2");
                    break;
                case "HU3":
                    SwitchText("2");
                    break;
                case "S3":
                    GameWorld.gameOver = true;
                    break;
                case "HO4":
                    GameWorld.gameOver = true;
                    SwitchText("6");
                    break;
                case "HU4":
                    SwitchText("5");
                    break;
                case "S4":
                    SwitchText("3");
                    break;
                case "HO5":
                    SwitchText("2");
                    break;
                case "HU5":
                    SwitchText("2");
                    break;
                case "S5":
                    SwitchText("3");
                    break;
                default:
                    break;
            }
        }
    }
}
