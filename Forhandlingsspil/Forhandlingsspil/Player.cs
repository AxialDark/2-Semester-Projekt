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
        private string[] keys = new string[10];


        private Statement[] stateArray = new Statement[3];
        public string[] Keys
        {
            get { return keys; }
            set { keys = value; }
        }

        public int Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        private Player(Vector2 position, float scale, float layer, Rectangle rect)
            : base(position, scale, layer, rect)
        {
            honestDic.Add(0.ToString() + "HO", new Statement("0HO", new Vector2(50, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest, "The honest answer", 1, 100));
            humorousDic.Add(0.ToString() + "HU", new Statement("0HU", new Vector2(300, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous, "The humorous answer", 0, 0));
            sneakyDic.Add(0.ToString() + "S", new Statement("0S", new Vector2(550, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky, "The sneaky answer", -1, -100));

            honestDic.Add(1.ToString() + "HO", new Statement("1HO", new Vector2(50, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest, "The honest answer2", 1, 100));
            humorousDic.Add(1.ToString() + "HU", new Statement("1HU", new Vector2(300, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous, "The humorous answer2", 0, 0));
            sneakyDic.Add(1.ToString() + "S", new Statement("1S", new Vector2(550, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky, "The sneaky answer2", -1, -100));

            honestDic.Add(2.ToString() + "HO", new Statement("2HO", new Vector2(50, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest, "The honest answer3", 1, 100));
            humorousDic.Add(2.ToString() + "HU", new Statement("2HU", new Vector2(300, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous, "The humorous answer3", 0, 0));
            sneakyDic.Add(2.ToString() + "S", new Statement("2S", new Vector2(550, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky, "The sneaky answer3", -1, -100));

            honestDic.Add(3.ToString() + "HO", new Statement("3HO", new Vector2(50, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest, "The honest answer4", 1, 100));
            humorousDic.Add(3.ToString() + "HU", new Statement("3HU", new Vector2(300, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous, "The humorous answer4", 0, 0));
            sneakyDic.Add(3.ToString() + "S", new Statement("3S", new Vector2(550, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky, "The sneaky answer4", -1, -100));

            honestDic.Add(4.ToString() + "HO", new Statement("4HO", new Vector2(50, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest, "The honest answer5", 1, 100));
            humorousDic.Add(4.ToString() + "HU", new Statement("4HU", new Vector2(300, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous, "The humorous answer5", 0, 0));
            sneakyDic.Add(4.ToString() + "S", new Statement("4S", new Vector2(550, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky, "The sneaky answer5", -1, -100));

            honestDic.Add(5.ToString() + "HO", new Statement("5HO", new Vector2(50, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest, "The honest answer6", 1, 100));
            humorousDic.Add(5.ToString() + "HU", new Statement("5HU", new Vector2(300, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous, "The humorous answer6", 0, 0));
            sneakyDic.Add(5.ToString() + "S", new Statement("5S", new Vector2(550, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky, "The sneaky answer6", -1, -100));

            honestDic.Add(6.ToString() + "HO", new Statement("6HO", new Vector2(50, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest, "The honest answer7", 1, 100));
            humorousDic.Add(6.ToString() + "HU", new Statement("6HU", new Vector2(300, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous, "The humorous answer7", 0, 0));
            sneakyDic.Add(6.ToString() + "S", new Statement("6S", new Vector2(550, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky, "The sneaky answer7", -1, -100));

            honestDic.Add(7.ToString() + "HO", new Statement("7HO", new Vector2(50, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest, "The honest answer8", 1, 100));
            humorousDic.Add(7.ToString() + "HU", new Statement("7HU", new Vector2(300, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous, "The humorous answer8", 0, 0));
            sneakyDic.Add(7.ToString() + "S", new Statement("7S", new Vector2(550, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky, "The sneaky answer8", -1, -100));

            honestDic.Add(8.ToString() + "HO", new Statement("8HO", new Vector2(50, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest, "The honest answer9", 1, 100));
            humorousDic.Add(8.ToString() + "HU", new Statement("8HU", new Vector2(300, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous, "The humorous answer9", 0, 0));
            sneakyDic.Add(8.ToString() + "S", new Statement("8S", new Vector2(550, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky, "The sneaky answer9", -1, -100));

            honestDic.Add(9.ToString() + "HO", new Statement("9HO", new Vector2(50, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Honest, "The honest answer10", 1, 100));
            humorousDic.Add(9.ToString() + "HU", new Statement("9HU", new Vector2(300, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Humorous, "The humorous answer10", 0, 0));
            sneakyDic.Add(9.ToString() + "S", new Statement("9S", new Vector2(550, 350), 1, 1, new Rectangle(0, 0, 200, 50), StatementType.Sneaky, "The sneaky answer10", -1, -100));

            stateArray[0] = honestDic[0.ToString() + "HO"];
            stateArray[1] = humorousDic[0.ToString() + "HU"];
            stateArray[2] = sneakyDic[0.ToString() + "S"];
        }

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
        public override void LoadContent(ContentManager content)
        {
            //honestDic[0.ToString()].LoadContent(content);
            //humorousDic[0.ToString()].LoadContent(content);
            //sneakyDic[0.ToString()].LoadContent(content);


            //for (int i = 0; i < stateArray.Length; i++)
            //{
            //    stateArray[i].LoadContent(content);
            //}

            texture = content.Load<Texture2D>(@"white");
            base.LoadContent(content);
        }
        public override void Update(GameTime gameTime)
        {
            //honestDic[0.ToString()].Update(gameTime);
            //humorousDic[0.ToString()].Update(gameTime);
            //sneakyDic[0.ToString()].Update(gameTime);

            for (int i = 0; i < stateArray.Length; i++)
            {
                stateArray[i].Update(gameTime);
            }

            if (!GameWorld.isPreparing && negotiatingTrick != null)
                negotiatingTrick.Update(gameTime);

            //MouseControl();
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, position, rect, color[0], 0f, origin, scale, SpriteEffects.None, layer);
            //spriteBatch.Draw(texture, position + new Vector2(250, 0), rect, color[1], 0f, origin, scale, SpriteEffects.None, layer);
            //spriteBatch.Draw(texture, position + new Vector2(500, 0), rect, color[2], 0f, origin, scale, SpriteEffects.None, layer);

            //honestDic[GameWorld.RoundCounter.ToString()].Draw(spriteBatch);
            //humorousDic[GameWorld.RoundCounter.ToString()].Draw(spriteBatch);
            //sneakyDic[GameWorld.RoundCounter.ToString()].Draw(spriteBatch);
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

            spriteBatch.DrawString(GameWorld.font, salary.ToString(), new Vector2(100, 100), Color.Gold);

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
            //if (questions[0])
            //{
            //    spriteBatch.DrawString(GameWorld.font, "You clicked button 1", new Vector2(200, 200), Color.White);
            //}
            //if (questions[1])
            //{
            //    spriteBatch.DrawString(GameWorld.font, "You clicked button 2", new Vector2(200, 220), Color.White);
            //}
            //if (questions[2])
            //{
            //    spriteBatch.DrawString(GameWorld.font, "You clicked button 3", new Vector2(200, 240), Color.White);
            //}

            //spriteBatch.DrawString(GameWorld.font, text[0], position, Color.Black);
            //spriteBatch.DrawString(GameWorld.font, text[1], position + new Vector2(250, 0), Color.Black);
            //spriteBatch.DrawString(GameWorld.font, text[2], position + new Vector2(500, 0), Color.Black);

            //base.Draw(spriteBatch);
        }
        #region Crap
        //private void MouseControl()
        //{
        //    Vector2 mousePosition = Mouse.GetState().Position.ToVector2();

        //    #region MouseOver
        //    //if (mousePosition.X >= position.X && mousePosition.X <= position.X + 200)
        //    //{
        //    //    if (mousePosition.Y >= position.Y && mousePosition.Y <= position.Y + 50)
        //    //    {
        //    //        color[0] = Color.Red;
        //    //        MouseClick(mousePosition, 0);
        //    //    }
        //    //    else { color[0] = Color.White; }
        //    //}
        //    //else { color[0] = Color.White; }

        //    //if (mousePosition.X >= position.X + 250 && mousePosition.X <= position.X + 450)
        //    //{
        //    //    if (mousePosition.Y >= position.Y && mousePosition.Y <= position.Y + 50)
        //    //    {
        //    //        color[1] = Color.Red;
        //    //        MouseClick(mousePosition, 1);
        //    //    }
        //    //    else { color[1] = Color.White; }
        //    //}
        //    //else { color[1] = Color.White; }

        //    //if (mousePosition.X >= position.X + 500 && mousePosition.X <= position.X + 700)
        //    //{
        //    //    if (mousePosition.Y >= position.Y && mousePosition.Y <= position.Y + 50)
        //    //    {
        //    //        color[2] = Color.Red;
        //    //        MouseClick(mousePosition, 2);
        //    //    }
        //    //    else { color[2] = Color.White; }
        //    //}
        //    //else { color[2] = Color.White; }

        //    #endregion

        //}

        //private void MouseClick(Vector2 mousePosition, int buttonNumber)
        //{
        //    if (Mouse.GetState().LeftButton == ButtonState.Pressed && buttonNumber == 0 && !buttonClicked)
        //    {
        //        color[buttonNumber] = Color.Blue;
        //        questions[buttonNumber] = true;
        //        buttonClicked = true;

        //        //GameWorld.RoundCounter++;
        //    }
        //    if (Mouse.GetState().LeftButton == ButtonState.Pressed && buttonNumber == 1 && !buttonClicked)
        //    {
        //        color[buttonNumber] = Color.Blue;
        //        questions[buttonNumber] = true;
        //        buttonClicked = true;
        //        //GameWorld.RoundCounter++;
        //    }
        //    if (Mouse.GetState().LeftButton == ButtonState.Pressed && buttonNumber == 2 && !buttonClicked)
        //    {
        //        color[buttonNumber] = Color.Blue;
        //        questions[buttonNumber] = true;
        //        buttonClicked = true;
        //        //GameWorld.RoundCounter++;
        //    }
        //    if (Mouse.GetState().LeftButton == ButtonState.Released)
        //    {
        //        buttonClicked = false;
        //    }
        //}
        #endregion
        public void SwitchStatements()
        {
            if (honestDic.Count > GameWorld.RoundCounter)
            {
                stateArray[0] = honestDic[GameWorld.RoundCounter.ToString() + "HO"];
                stateArray[1] = humorousDic[GameWorld.RoundCounter.ToString() + "HU"];
                stateArray[2] = sneakyDic[GameWorld.RoundCounter.ToString() + "S"];
            }
        }
        public void CreateNegotiatingTrick(NegotiatingTrick trick)
        {
            negotiatingTrick = trick;
        }
    }
}
