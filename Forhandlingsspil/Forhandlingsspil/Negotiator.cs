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
        #region Fields
        string curText = "";
        Color textColor = Color.Black;
        private short moodValue;
        private NegotiatorMood mood = NegotiatorMood.Neutral;
        private Dictionary<string, string> responses = new Dictionary<string, string>();
        private static Negotiator instance;
        private Texture2D[] textures = new Texture2D[11];
        private DateTime idleTimer = DateTime.Now;
        private int currentResponsKey;
        private Texture2D speechBubble;
        #endregion
        #region Properties
        public int CurrentResponsKey
        {
            get { return currentResponsKey; }
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
                    instance = new Negotiator(new Vector2(0, -100), 1, 1.0f, new Rectangle(0, 0, 1280, 960));
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
        #endregion

        private Negotiator(Vector2 position, float scale, float layer, Rectangle rect)
            : base(position, scale, layer, rect)
        {
            this.layer = 0.0f;

            #region Adding text to the response Dictionary
            responses.Add("S1", "Tja det er vel rimeligt, men synes du selv at du er det værd?");
            responses.Add("N1", "Okay, men er du det værd?");
            responses.Add("D1", "Er det ikke i overkanten, er du overhovedet det værd?");

            responses.Add("S2", "Tja der er også den mulighed at få goder i stedet, hvad siger du til det?");
            responses.Add("N2", "Det vil blive temmelig dyrt for firmaet hvis du skal have det hele som løn" + Environment.NewLine + "hvad med nogle goder i stedet?");
            responses.Add("D2", "Det har vi ikke råd til, vi kan tilbyde nogle goder i stedet?");

            responses.Add("S3", "Er du nu helt sikker?");
            responses.Add("N3", "Jeg mener ikke at du er alle de penge værd, men prøv at overbevise mig");
            responses.Add("D3", "Det er bare ikke godt nok, prøv igen");

            responses.Add("S4", "Ja jeg synes at du har gjort et godt stykke arbejde men jeg synes ikke at du kvalificerer" + Environment.NewLine + "dig til en lønforhøjelse så jeg tænker samme løn?");
            responses.Add("N4", "Njaa, jeg tænker samme løn?");
            responses.Add("D4", "Jeg mener du allerede du får nok i løn?");

            responses.Add("S5", "Tja det er vel rimeligt, men synes du selv at du er det værd?");
            responses.Add("N5", "Okay, men er du det værd?");
            responses.Add("D5", "Er det ikke i overkanten, er du overhovedet det værd?");

            responses.Add("S6", "Det var en meget god samtale og du har helt klart fortjent den løn du får");
            responses.Add("N6", "Det var fint at se dig i dag, du får den løn du får");
            responses.Add("D6", "Det var det, skrid hjem med din lorte løn");
            #endregion

            this.scale = 0.75f;

            //Sets the start response
            curText = "Velkommen til, jeg har lige fået en ny stol, den er dæleme rar";
        }

        /// <summary>
        /// Switches the Negotiators mood based on the parameter
        /// </summary>
        /// <param name="value">Used in changing the Negotiators mood. Range from (-1 to 1)</param>
        public void SwitchMood(int value)
        {
            //Adds to the mood value only if possible
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

            //Changes the color for the mood text, according to the mood
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
            textures[0] = content.Load<Texture2D>(@"Forhandler\udvalgte\idle_sat");
            textures[1] = content.Load<Texture2D>(@"Forhandler\udvalgte\idle_neu");
            textures[2] = content.Load<Texture2D>(@"Forhandler\udvalgte\idle_dis");
            textures[3] = content.Load<Texture2D>(@"Forhandler\udvalgte\resp_sat");
            textures[4] = content.Load<Texture2D>(@"Forhandler\udvalgte\resp_neu");
            textures[5] = content.Load<Texture2D>(@"Forhandler\udvalgte\resp_dis");
            textures[6] = content.Load<Texture2D>(@"Forhandler\udvalgte\end_sat");
            textures[7] = content.Load<Texture2D>(@"Forhandler\udvalgte\end_neu");
            textures[8] = content.Load<Texture2D>(@"Forhandler\udvalgte\slut");
            textures[9] = content.Load<Texture2D>(@"Forhandler\udvalgte\forb");
            textures[10] = content.Load<Texture2D>(@"Forhandler\udvalgte\fyrst");

            speechBubble = content.Load<Texture2D>(@"Forhandler\Negotiator");
            
            //Sets the texture to the preparing fase
            texture = textures[9];
            base.LoadContent(content);
        }
        /// <summary>
        /// Used to Update the variables in the Negotiator class
        /// </summary>
        /// <param name="gameTime">From the monogame framework, counts the time</param>
        public override void Update(GameTime gameTime)
        {
            if (!GameWorld.isPreparing && !GameWorld.gameOver)
            {
                //Switches to idle pictures after a brief delay
                ToIdle();
            }
            if (GameWorld.gameOver)
            {
                //Repositions the texture when the game ends
                position = new Vector2(0, 0);
            }
            base.Update(gameTime);
        }
        /// <summary>
        /// Used to draw in the Negotiator class
        /// </summary>
        /// <param name="spriteBatch">From the monogame framework used to draw</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Draws the responese only after the preparingfase
            if (!GameWorld.isPreparing)
            {
                spriteBatch.Draw(speechBubble, new Vector2(GameWorld.windowWitdh / 2 - speechBubble.Width / 2, 20), new Rectangle(0, 0, 850, 75), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.99f);
                spriteBatch.DrawString(GameWorld.font, curText, new Vector2((GameWorld.windowWitdh / 2) - (GameWorld.font.MeasureString(curText).X / 2), 30 + speechBubble.Height / 2 - GameWorld.font.MeasureString(curText).Y), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
                if (!GameWorld.gameOver)
                    spriteBatch.DrawString(GameWorld.font, "Humør: " + mood.ToString(), new Vector2(GameWorld.windowWitdh - GameWorld.font.MeasureString("Humør: " + mood.ToString()).X - 150, 90), textColor, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
            }
            base.Draw(spriteBatch);
        }
        /// <summary>
        /// Switches the Negotiators response
        /// </summary>
        /// <param name="key">The last half of the key for the Dictionary</param>
        public void SwitchText(string key)
        {
            currentResponsKey = Convert.ToInt32(key);
            
            //Creates the next key based on mood and key parameter
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
                    GameWorld.endTimer.Start();
                    SwitchTexture("End", 0);
                    SwitchText("6");
                    break;
                case "S2":
                    GameWorld.gameOver = true;
                    GameWorld.endTimer.Start();
                    SwitchTexture("End", 0);
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
                    GameWorld.endTimer.Start();
                    SwitchTexture("Fired", 0);
                    break;
                case "HO4":
                    GameWorld.gameOver = true;
                    GameWorld.endTimer.Start();
                    SwitchTexture("End", 0);
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
        /// <summary>
        /// Changes the texture based on the context and the mood value from moodChanger
        /// </summary>
        /// <param name="context">The context for the texture switch (Idle, Resp, End and Fyret)</param>
        /// <param name="moodChanger">The value for the mood change</param>
        public void SwitchTexture(string context, int moodChanger)
        {
            NegotiatorMood statementMood = NegotiatorMood.Neutral;

            if (moodChanger == 1)
                statementMood = NegotiatorMood.Satisfied;
            else if (moodChanger == 0)
                statementMood = NegotiatorMood.Neutral;
            else if (moodChanger == -1)
                statementMood = NegotiatorMood.Dissatisfied;


            if (mood == NegotiatorMood.Satisfied && context == "Idle")
            {
                texture = textures[0];
            }
            else if (mood == NegotiatorMood.Neutral && context == "Idle")
            {
                texture = textures[1];
            }
            else if (mood == NegotiatorMood.Dissatisfied && context == "Idle")
            {
                texture = textures[2];
            }

            if (statementMood == NegotiatorMood.Satisfied && context == "Resp")
            {
                texture = textures[3];
                idleTimer = DateTime.Now.AddSeconds(2);
            }
            else if (statementMood == NegotiatorMood.Neutral && context == "Resp")
            {
                texture = textures[4];
                idleTimer = DateTime.Now.AddSeconds(2);
            }
            else if (statementMood == NegotiatorMood.Dissatisfied && context == "Resp")
            {
                texture = textures[5];
                idleTimer = DateTime.Now.AddSeconds(2);
            }

            if (mood == NegotiatorMood.Satisfied && context == "End")
            {
                texture = textures[6];
            }
            else if (mood == NegotiatorMood.Neutral && context == "End")
            {
                texture = textures[7];
            }
            else if (mood == NegotiatorMood.Dissatisfied && context == "End")
            {
                texture = textures[8];
            }

            if(context == "Fired")
            {
                curText = "UD! Du er fyret!!!";
                Player.Instance.Salary = 0;
                texture = textures[10];
            }
        }

        private void ToIdle()
        {
            if (idleTimer < DateTime.Now)
            {
                SwitchTexture("Idle", 0);
            }
        }
    }
}
