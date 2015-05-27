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
        private Dictionary<string, Statement> honestDic = new Dictionary<string, Statement>();

        public Dictionary<string, Statement> HonestDic
        {
            get { return honestDic; }
            set { honestDic = value; }
        }
        private Dictionary<string, Statement> humorousDic = new Dictionary<string, Statement>();
        private Dictionary<string, Statement> sneakyDic = new Dictionary<string, Statement>();
        private int salary;
        private NegotiatingTrick negotiatingTrick;
        private static Player instance;

        private Color[] color = new Color[] { Color.White, Color.White, Color.White };
        private bool buttonClicked = false;
        private bool[] questions = new bool[] { false, false, false };
        private List<string> keys = new List<string>();


        private Statement[] stateArray = new Statement[3];
        public List<string> Keys
        {
            get { return keys; }
            set { keys = value; }
        }

        public int Salary
        {
            get { return salary; }
            set { salary = value; }
        }

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
            honestDic.Add("HO0", new Statement("HO0", new Vector2(50, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest, 
                "Hej jeg taenkte paa at jeg " + Environment.NewLine + "skulle have ??????kr." + Environment.NewLine + "mere om maaneden", 0, 0));
            humorousDic.Add("HU0", new Statement("HU0", new Vector2(300, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous,
                "Dav jeg forstillede mig at" + Environment.NewLine + "skulle have ??????kr." + Environment.NewLine + "om maaneden", 0, 0));
            sneakyDic.Add("S0", new Statement("S0", new Vector2(550, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky,
                "Mange tak fordi du ville " + Environment.NewLine + "moedes med mig." + Environment.NewLine + "Det er vel nok en fin stol", 1, 0));

            honestDic.Add("HO1", new Statement("HO1", new Vector2(50, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest,
                "Ja, fordi jeg har staaet" + Environment.NewLine + "i spidsen for mange af de" + Environment.NewLine + "bedste projekter vi har haft", 1, 0));
            humorousDic.Add("HU1", new Statement("HU1", new Vector2(300, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous,
                "Ja da"+ Environment.NewLine + "fordi jeg er stjerne god" + Environment.NewLine + "til det jeg laver", -1, 0));
            sneakyDic.Add("S1", new Statement("S1", new Vector2(550, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky,
                "Selvfoelge er jeg det" + Environment.NewLine + "jeg er en af de bedste" + Environment.NewLine + "arbejdere i firmaet", 0, 0));

            honestDic.Add("HO2", new Statement("HO2", new Vector2(50, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest,
                "Jeg kan godt gaa med til" + Environment.NewLine + "at tage nogle goder ogsaa", 1, 0));
            sneakyDic.Add("S2", new Statement("S2", new Vector2(550, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky,
                "Jeg vil helst have flere penge", -1, 0));

            honestDic.Add("HO3", new Statement("HO3", new Vector2(50, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest,
                "Ja, men jeg er den mest" + Environment.NewLine + "aktive af dem jeg arbejder" + Environment.NewLine + "i projekt med", 0, 0));
            humorousDic.Add("HU3", new Statement("HU3", new Vector2(300, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous,
                "Jeg er jo den der staar" + Environment.NewLine + "for den gode stemning" + Environment.NewLine + "her i firmaet", 1, 0));
            sneakyDic.Add("S3", new Statement("S3", new Vector2(550, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky,
                "Jeg saa hvad du lavede til" + Environment.NewLine + "firmafesten" + Environment.NewLine + "er din kone klar over det?", -1, 0));

            honestDic.Add("HO4", new Statement("HO4", new Vector2(50, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest,
                "Samme loen det vel i orden", 1, 0));
            humorousDic.Add("HU4", new Statement("HU4", new Vector2(300, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous,
                "Kom nu von and, du svoemmer" + Environment.NewLine + "jo rundt i penge, saa du kan godt" + Environment.NewLine + "lade noget falde ned til mig", -1, 0));
            sneakyDic.Add("S4", new Statement("S4", new Vector2(550, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky,
                "Samme loen, jeg mener da jeg er" + Environment.NewLine + "??????kr. mere vaerd paa grund" + Environment.NewLine + "af alle de smaa tjenester", 0, 0));

            honestDic.Add("HO5", new Statement("HO5", new Vector2(50, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest,
                "Huslejen er lige steget saa hvis jeg" + Environment.NewLine + "ikke kan faa mere i loen kan det vaere" + Environment.NewLine + "at jeg bliver noed til at saelge mit hus", 0, 0));
            humorousDic.Add("HU5", new Statement("HU5", new Vector2(300, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous,
                "Der er en grund til at man kalder sin" + Environment.NewLine + "kone for skat hun tager sku alle mine" + Environment.NewLine + "penge saa jeg har brug for nogle flere", 1, 0));
            sneakyDic.Add("S5", new Statement("S5", new Vector2(550, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky,
                "Ja, for jeg er en rigtig god fedteroev", -1, 0));




            stateArray[0] = honestDic["HO0"];
            stateArray[1] = humorousDic["HU0"];
            stateArray[2] = sneakyDic["S0"];
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
            if (!GameWorld.gameOver && !GameWorld.isPreparing)
            {
                for (int i = 0; i < stateArray.Length; i++)
                {
                    stateArray[i].Draw(spriteBatch);
                }
            }

            if (negotiatingTrick != null)
            {
                negotiatingTrick.DrawUseText(spriteBatch);
                negotiatingTrick.Draw(spriteBatch);
            }

            spriteBatch.DrawString(GameWorld.font, "Salary: " + salary.ToString(), new Vector2(700, 0), Color.Gold);

            if (GameWorld.gameOver)
            {
                Vector2 textPos = new Vector2(10, 10);

                foreach (string str in keys)
                {
                    if (honestDic.ContainsKey(str))
                    {
                        spriteBatch.DrawString(GameWorld.font, honestDic[str].StatementText, textPos, Color.Black);
                    }
                    if (humorousDic.ContainsKey(str))
                    {
                        spriteBatch.DrawString(GameWorld.font, humorousDic[str].StatementText, textPos, Color.Black);
                    }
                    if (sneakyDic.ContainsKey(str))
                    {
                        spriteBatch.DrawString(GameWorld.font, sneakyDic[str].StatementText, textPos, Color.Black);
                    }
                    textPos += new Vector2(0, 15);
                }
            }

            //base.Draw(spriteBatch);
        }
        /// <summary>
        /// The method used to switch the drawn statements for the Player to click on
        /// </summary>
        /// <param name="responseKey">The key last of the key for the answer Dictinaries</param>
        public void SwitchStatements(string responseKey)
        {
            stateArray[0] = honestDic["HO" + responseKey];
            if (humorousDic.ContainsKey("HU" + responseKey))
                stateArray[1] = humorousDic["HU" + responseKey];
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
