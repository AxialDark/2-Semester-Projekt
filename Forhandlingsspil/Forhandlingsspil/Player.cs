using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forhandlingsspil
{
    class Player : SpriteObject
    {
        #region Fields
        private Dictionary<string, Statement> honestDic = new Dictionary<string, Statement>();
        private Dictionary<string, Statement> humorousDic = new Dictionary<string, Statement>();
        private Dictionary<string, Statement> sneakyDic = new Dictionary<string, Statement>();

        private int salary;
        private NegotiatingTrick negotiatingTrick;
        private static Player instance;
        private Color[] color = new Color[] { Color.White, Color.White, Color.White };
        private List<string> keys = new List<string>();
        private Statement[] stateArray = new Statement[3];
        #endregion
        #region Properties
        public List<string> Keys
        {
            get { return keys; }
            set { keys = value; }
        }
        public Dictionary<string, Statement> HumorousDic
        {
            get { return humorousDic; }
        }
        public Dictionary<string, Statement> HonestDic
        {
            get { return honestDic; }
        }
        public Dictionary<string, Statement> SneakyDic
        {
            get { return sneakyDic; }
        }
        public int Salary
        {
            get { return salary; }
            set { salary = value; }
        }
        public NegotiatingTrick NegotiatingTrick
        {
            get { return negotiatingTrick; }
        }
        /// <summary>
        /// Used in the singleton design pattern
        /// </summary>
        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player(new Vector2(50, 350), 1, 1.0f, new Rectangle(0, 0, 200, 50));
                }
                return instance;
            }
        }
        #endregion

        /// <summary>
        /// The Constructor for the Player class
        /// </summary>
        /// <param name="position">The position of the Player class on the screen</param>
        /// <param name="scale">The factor used to resize the Player sprite</param>
        /// <param name="layer">The position on the layer for the Player class to be drawn on</param>
        /// <param name="rect">The section from the texture that should be drawn</param>
        private Player(Vector2 position, float scale, float layer, Rectangle rect)
            : base(position, scale, layer, rect)
        {
            //Sets the start salary
            this.salary = 35000;
            this.layer = 1.0f;
            //used for Statement placing
            int height = 620;

            #region Adding Statements to Dictionaries
            honestDic.Add("HO0", new Statement("HO0", new Vector2(10, height), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest,
                "Hej jeg tænkte på at jeg " + Environment.NewLine + "skulle have 39.000kr." + Environment.NewLine + "om måneden", 0, 4000));
            humorousDic.Add("HU0", new Statement("HU0", new Vector2(380, height), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous,
                "Dav jeg forstillede mig at" + Environment.NewLine + "skulle have 36.000kr." + Environment.NewLine + "om måneden", 0, 1000));
            sneakyDic.Add("S0", new Statement("S0", new Vector2(750, height), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky,
                "Mange tak fordi du ville " + Environment.NewLine + "mødes med mig." + Environment.NewLine + "Det er vel nok en fin stol", 1, 0));

            honestDic.Add("HO1", new Statement("HO1", new Vector2(10, height), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest,
                "Ja, fordi jeg har stået" + Environment.NewLine + "i spidsen for mange af de" + Environment.NewLine + "bedste projekter vi har haft", 1, 500));
            humorousDic.Add("HU1", new Statement("HU1", new Vector2(380, height), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous,
                "Ja da" + Environment.NewLine + "fordi jeg er stjerne god" + Environment.NewLine + "til det jeg laver", -1, -700));
            sneakyDic.Add("S1", new Statement("S1", new Vector2(750, height), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky,
                "Selvfølgelig er jeg det" + Environment.NewLine + "jeg er en af de bedste" + Environment.NewLine + "arbejdere i firmaet", 0, -200));

            honestDic.Add("HO2", new Statement("HO2", new Vector2(186, height), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest,
                "Jeg kan godt gå med til" + Environment.NewLine + "at tage nogle goder også", 1, 200));
            sneakyDic.Add("S2", new Statement("S2", new Vector2(574, height), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky,
                "Jeg vil helst have flere penge", -1, -200));

            honestDic.Add("HO3", new Statement("HO3", new Vector2(10, height), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest,
                "Ja, men jeg er den mest" + Environment.NewLine + "aktive af dem jeg arbejder" + Environment.NewLine + "i projekt med", 0, 100));
            humorousDic.Add("HU3", new Statement("HU3", new Vector2(380, height), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous,
                "Jeg er jo den der står" + Environment.NewLine + "for den gode stemning" + Environment.NewLine + "her i firmaet", 1, 200));
            sneakyDic.Add("S3", new Statement("S3", new Vector2(750, height), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky,
                "Jeg så hvad du lavede til" + Environment.NewLine + "firmafesten" + Environment.NewLine + "er din kone klar over det?", -1, 0));

            honestDic.Add("HO4", new Statement("HO4", new Vector2(10, height), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest,
                "Samme løn det vel i orden", 1, 0));
            humorousDic.Add("HU4", new Statement("HU4", new Vector2(380, height), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous,
                "Kom nu von and, du svømmer" + Environment.NewLine + "jo rundt i penge, så du kan godt" + Environment.NewLine + "lade noget falde ned til mig", -1, -500));
            sneakyDic.Add("S4", new Statement("S4", new Vector2(750, height), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky,
                "Samme løn, jeg mener da jeg er" + Environment.NewLine + "1.000kr. mere værd på grund" + Environment.NewLine + "af alle de små tjenester", 0, 500));

            honestDic.Add("HO5", new Statement("HO5", new Vector2(10, height), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest,
                "Huslejen er lige steget så hvis jeg" + Environment.NewLine + "ikke kan få mere i løn kan det være" + Environment.NewLine + "at jeg bliver nød til at sælge mit hus", 0, 700));
            humorousDic.Add("HU5", new Statement("HU5", new Vector2(380, height), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous,
                "Der er en grund til at man kalder sin" + Environment.NewLine + "kone for skat hun tager sku alle mine" + Environment.NewLine + "penge så jeg har brug for nogle flere", 1, 200));
            sneakyDic.Add("S5", new Statement("S5", new Vector2(750, height), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky,
                "Ja, for jeg er en rigtig god fedterøv", -1, -400));
            #endregion

            //Sets the initial choices to the first reponse
            stateArray[0] = honestDic["HO0"];
            stateArray[1] = humorousDic["HU0"];
            stateArray[2] = sneakyDic["S0"];
        }

        /// <summary>
        /// Used to load content when the game starts
        /// </summary>
        /// <param name="content">From the monogame framework, used to load the content</param>
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"white");
            base.LoadContent(content);
        }
        /// <summary>
        /// Used to Update the variables in the Player class
        /// </summary>
        /// <param name="gameTime">From the monogame framework, counts the time</param>
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < stateArray.Length; i++)
            {
                if (stateArray[i] != null)
                    stateArray[i].Update(gameTime);
            }

            if (!GameWorld.isPreparing && negotiatingTrick != null)
                negotiatingTrick.Update(gameTime);

            base.Update(gameTime);
        }
        /// <summary>
        /// Used to draw in the Player class
        /// </summary>
        /// <param name="spriteBatch">From the monogame framework used to draw</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Draws the Statement choices when the preperationfase is over and the game is running
            if (!GameWorld.gameOver && !GameWorld.isPreparing)
            {
                for (int i = 0; i < stateArray.Length; i++)
                {
                    if (stateArray[i] != null)
                        stateArray[i].Draw(spriteBatch);
                }
            }
            //Only draw if the player has a negotiatingtrick
            if (negotiatingTrick != null)
            {
                negotiatingTrick.Draw(spriteBatch);
            }
#if DEBUG
            spriteBatch.DrawString(GameWorld.font, "Salary: " + salary.ToString() + "kr.", new Vector2(700, 0), Color.Gold, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
#endif
            //Writes out the choices the player has made, when the negotiation is over
            //if (GameWorld.gameOver)
            //{
            //    Vector2 textPos = new Vector2(10, 30);

            //    string textString = string.Empty;

            //    foreach (string str in keys)
            //    {
            //        if (honestDic.ContainsKey(str))
            //        {
            //            textString += honestDic[str].StatementText + Environment.NewLine + Environment.NewLine;
            //        }
            //        if (humorousDic.ContainsKey(str))
            //        {
            //            textString += humorousDic[str].StatementText + Environment.NewLine + Environment.NewLine;
            //        }
            //        if (sneakyDic.ContainsKey(str))
            //        {
            //            textString += sneakyDic[str].StatementText + Environment.NewLine + Environment.NewLine;
            //        }
            //    }
            //    spriteBatch.DrawString(GameWorld.font, textString, textPos, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, layer);
            //}

            //base.Draw(spriteBatch);
        }
        /// <summary>
        /// The method used to switch the drawn statements for the Player to click on
        /// </summary>
        /// <param name="responseKey">The key last of the key for the answer Dictinaries</param>
        public void SwitchStatements(string responseKey)
        {
            //Sets the draw statements according to the responsekey
            stateArray[0] = honestDic["HO" + responseKey];
            if (humorousDic.ContainsKey("HU" + responseKey))
                stateArray[1] = humorousDic["HU" + responseKey];
            else if (!humorousDic.ContainsKey("HU" + responseKey))
                stateArray[1] = null;
            stateArray[2] = sneakyDic["S" + responseKey];
        }
        /// <summary>
        /// Used to create the Negotiating trick, that the Player can use once through the game
        /// </summary>
        /// <param name="trick">An instanse of the NegotiatingTrick class</param>
        public void CreateNegotiatingTrick(NegotiatingTrick trick)
        {
            negotiatingTrick = trick;
        }
    }
}
