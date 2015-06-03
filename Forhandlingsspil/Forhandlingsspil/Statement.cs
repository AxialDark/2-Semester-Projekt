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
    class Statement : SpriteObject
    {
        #region Fields
        private int salaryChangeValue;
        private int moodChangeValue;
        private string statementText;
        private StatementType type;
        private bool buttonClicked = true;
        #region USED IN DEBUG
        private bool question = false;
        private DateTime remove = DateTime.Now;
        #endregion
        private DateTime click;
        private bool isClickable = false;
        private string key;
        private Texture2D icon;
        private float scaly;
        private Rectangle recty;
        #endregion
        #region Properties
        public string StatementText
        {
            get { return statementText; }
        }
        public int SalaryChangeValue
        {
            get { return salaryChangeValue; }
        }
        #endregion

        /// <summary>
        /// THe Constructor for the Statement class
        /// </summary>
        /// <param name="key">The key used in the Dictionary in which this Statement is contained</param>
        /// <param name="position">The position if the Statement on the screen</param>
        /// <param name="scale">The factor used to resize the Statement sprite</param>
        /// <param name="layer">The position on the layer for the Statement to be drawn on</param>
        /// <param name="rect">The section from the texture that should be drawn</param>
        /// <param name="type">The typr of the Statement</param>
        /// <param name="text">The text that is shown on the button</param>
        /// <param name="moodValue">The variable that the Statement use to change Negotiator mood</param>
        /// <param name="salaryValue">The value for the answer</param>
        public Statement(string key, Vector2 position, float scale, float layer, Rectangle rect, StatementType type, string text, int moodValue, int salaryValue)
            : base(position, scale, layer, rect)
        {
            this.key = key;
            this.statementText = text;
            this.type = type;
            this.moodChangeValue = moodValue;
            this.salaryChangeValue = salaryValue;

            this.layer = 0.9f;

            if (type == StatementType.Honest)
            {
                this.scaly = 0.15f;
                this.recty = new Rectangle(0, 0, 640, 480);
            }
            else if (type == StatementType.Humorous)
            {
                this.scaly = 0.2f;
                this.recty = new Rectangle(0, 0, 300, 300);
            }
            else if (type == StatementType.Sneaky)
            {
                this.scaly = 0.3f;
                this.recty = new Rectangle(0, 0, 300, 300);
            }

            LoadContent(GameWorld.myContent);
        }
        /// <summary>
        /// Used to load content when the game starts
        /// </summary>
        /// <param name="content">From the monogame framework, used to load the content</param>
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"white");
	    
	        //Loads the right texture based on the type of Statement
            if (type == StatementType.Honest)
            {
                icon = content.Load<Texture2D>(@"Honest");
            }
            else if (type == StatementType.Humorous)
            {
                icon = content.Load<Texture2D>(@"Humor");
            }
            else if (type == StatementType.Sneaky)
            {
                icon = content.Load<Texture2D>(@"Cunning");
            }

            base.LoadContent(content);
        }
        /// <summary>
        /// Used to Update the variables in the Statement class
        /// </summary>
        /// <param name="gameTime">From the monogame framework, counts the time</param>
        public override void Update(GameTime gameTime)
        {
            //Makes so the player can't keep the mousebutton down
            if (!isClickable)
            {
                click = DateTime.Now.AddMilliseconds(2);
                isClickable = true;
            }
            //Used in DEBUG for removing some tekst
            RemoveText();

            MouseControl();
            base.Update(gameTime);
        }
        /// <summary>
        /// Used to draw in the Statement class
        /// </summary>
        /// <param name="spriteBatch">From the monogame framework used to draw</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(GameWorld.smallFont, statementText, position, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);

            spriteBatch.Draw(icon, new Vector2(position.X + 100 - ((icon.Width * scaly) / 2), position.Y + 50), recty, Color.White, 0f, origin, scaly, SpriteEffects.None, layer);

            //if (type == StatementType.Humorous)
            //{
            //    spriteBatch.Draw(icon, new Vector2(position.X + 100 - ((icon.Width * 0.2f) / 2), position.Y + 50), new Rectangle(0, 0, 300, 300), Color.White, 0f, origin, 0.2f, SpriteEffects.None, layer);
            //}

            #if DEBUG
            if (question)
            {
                spriteBatch.DrawString(GameWorld.font, "You clicked " + type.ToString(), new Vector2(200, 200), Color.Black);
            }
            #endif
        }
        /// <summary>
        /// Used for the mouse control, Checking of position etc.
        /// </summary>
        private void MouseControl()
        {
            Vector2 mousePosition = Mouse.GetState().Position.ToVector2();
            
            //if the mouse is positioned somewhere on the Statement button, runs the MouseClick method
            if (mousePosition.X >= position.X && mousePosition.X <= position.X + 200)
            {
                if (mousePosition.Y >= position.Y && mousePosition.Y <= position.Y + 50 && click < DateTime.Now)
                {
                    //Changes the buttons color when mouse hovers over the button
                    color = Color.Red;
                    MouseClick();
                }
                else { color = Color.White; }
            }
            else { color = Color.White; }
        }
        /// <summary>
        /// Contains the code for what happens when the mouse is clicked
        /// </summary>
        private void MouseClick()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !buttonClicked)
            {
                color = Color.Blue;
                buttonClicked = true;
                //Used in DEBUG
                if (!question)
                    remove = DateTime.Now.AddSeconds(5);
                question = true;

                Player.Instance.Keys.Add(key);
                Negotiator.Instance.SwitchMood(moodChangeValue);
                Player.Instance.Salary += salaryChangeValue;
                Negotiator.Instance.SwitchTexture("Resp", moodChangeValue);
                Negotiator.Instance.SwitchResponse(key);
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                buttonClicked = false;
            }
        }

        #region METHODS USED IN DEBUG
        private void RemoveText()
        {
            if (remove <= DateTime.Now)
                question = false;
        }
        #endregion
    }
}
