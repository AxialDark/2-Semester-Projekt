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
        string curText = "";
        Color textColor = Color.Black;

        private short moodValue;
        private NegotiatorMood mood = NegotiatorMood.Neutral;
        private Dictionary<string, string> responses = new Dictionary<string, string>();
        private static Negotiator instance;

        private Texture2D[] textures = new Texture2D[11];

        private Negotiator(Vector2 position, float scale, float layer, Rectangle rect)
            : base(position, scale, layer, rect)
        {
            //responses.Add("0", "kald det kaerlighed");
            //responses.Add("1", "kald det lige hvad du vil");
            //responses.Add("2", "ouuaaaaaah der findes ingen ord");
            //responses.Add("3", "ingen ord der helt slaar til");
            //responses.Add("4", "saa kald det lige hvad du vil");

            responses.Add("S1", "Tja det er vel rimeligt, men synes du selv at du er det hver?");
            responses.Add("N1", "Okay, men er du det hver?");
            responses.Add("D1", "Er det ikke i overkanten, er du overhovedet det hver?");

            responses.Add("S2", "Tja der er ogsaa den mulighed at faa goder i stedet, hvad siger du til det?");
            responses.Add("N2", "Det vil blive temmelig dyrt for firmaet hvis du skal have det hele som loen" + Environment.NewLine + "hvad med nogle goder i stedet?");
            responses.Add("D2", "Det har vi ikke raad til, vi kan tilbyde nogle goder i stedet?");

            responses.Add("S3", "Er du nu helt sikker?");
            responses.Add("N3", "Jeg mener ikke at du er alle de penge hver, men proev at overbevise mig");
            responses.Add("D3", "Det er bare ikke godt nok, proev igen");

            responses.Add("S4", "Ja jeg synes at du har gjort et godt stykke arbejde" + Environment.NewLine + "men jeg synes ikke at du kvalificerer dig til en loenforhoejelse" + Environment.NewLine + "saa jeg taenker samme loen?");
            responses.Add("N4", "Njaa, jeg taenker samme loen?");
            responses.Add("D4", "Jeg mener du allerede du faar nok i loen?");

            responses.Add("S5", "Tja det er vel rimeligt, men synes du selv at du er det hver?");
            responses.Add("N5", "Okay, men er du det hver?");
            responses.Add("D5", "Er det ikke i overkanten, er du overhovedet det hver?");

            responses.Add("S6", "Det var en meget god samtale og du har helt klart fortjent den loen du faar");
            responses.Add("N6", "Det var fint at se dig i dag, du faar den loen du faar");
            responses.Add("D6", "Det var det, skrid hjem med din lorte loen");

            LoadContent(GameWorld.myContent);
            
            texture = textures[9];

            this.scale = 0.75f;

            curText = "Velkommen til, jeg har lige faaet en ny stol, den er daelme rar";
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
                    instance = new Negotiator(new Vector2(0, 0), 1, 1.0f, new Rectangle(0, 0, 1280, 960));
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
            if (!GameWorld.isPreparing)
            {
                spriteBatch.DrawString(GameWorld.font, curText, new Vector2((GameWorld.windowWitdh / 2) - (GameWorld.font.MeasureString(curText).X / 2), 12), Color.Black);
                if (!GameWorld.gameOver)
                    spriteBatch.DrawString(GameWorld.font, mood.ToString(), new Vector2(0, 20), textColor);
            }
            base.Draw(spriteBatch);
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

        public void SwitchTexture()
        {
            if (mood == NegotiatorMood.Satisfied)
            {
                texture = textures[0];
            }
            else if (mood == NegotiatorMood.Neutral)
            {
                texture = textures[1];
            }
            else if (mood == NegotiatorMood.Dissatisfied)
            {
                texture = textures[3];
            }
        }
    }
}
