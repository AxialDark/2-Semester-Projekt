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
    class NegotiatingTrick : SpriteObject
    {
        private bool isTalkWithColleague;
        private bool isTradeUnion;
        private int salaryChangeValue;
        private short moodChangeValue;
        private string useText;



        private bool buttonClicked = true;
        public string UseText
        {
            get { return useText; }
        }
        private bool used;

        public NegotiatingTrick(Vector2 position, float scale, float layer, Rectangle rect, bool isColleague, bool isUnion)
            : base(position, scale, layer, rect)
        {
            LoadContent(GameWorld.myContent);
            this.isTalkWithColleague = isColleague;
            this.isTradeUnion = isUnion;
            //Temp
            if (isColleague)
                useText = "Snak med kollega";
            else if (isUnion)
                useText = "Brug forening";
        }

        /// <summary>
        /// Used to load content when the game starts
        /// </summary>
        /// <param name="content">From the monogame framework, used to load the content</param>
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"black");
            //base.LoadContent(content);
        }
        /// <summary>
        /// Used to Update the variables in the NegotiatingTrick class
        /// </summary>
        /// <param name="gameTime">From the monogame framework, counts the time</param>
        public override void Update(GameTime gameTime)
        {
            MouseControl();
            base.Update(gameTime);
        }
        /// <summary>
        /// Used to draw in the NegotiatingTrick class
        /// </summary>
        /// <param name="spriteBatch">From the monogame framework used to draw</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!used)
            {
                base.Draw(spriteBatch);
                spriteBatch.DrawString(GameWorld.font, useText, position, Color.White);
            }
        }
        /// <summary>
        /// Used for the mouse control, Checking of position etc.
        /// </summary>
        private void MouseControl()
        {
            Vector2 mousePosition = Mouse.GetState().Position.ToVector2();

            if (mousePosition.X >= position.X && mousePosition.X <= position.X + 50)
            {
                if (mousePosition.Y >= position.Y && mousePosition.Y <= position.Y + 10)
                {
                    MouseClick();
                }
            }
        }
        /// <summary>
        /// Contains the code for what happens when the mouse is clicked
        /// </summary>
        private void MouseClick()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !buttonClicked)
            {
                buttonClicked = true;

                if (GameWorld.isPreparing)
                {
                    SwitchFromPreparing();
                    Player.Instance.CreateNegotiatingTrick(this);
                }
                else if (!GameWorld.isPreparing)
                {
                    if (!used)
                    {
                        Player.Instance.Salary += 200;
                        used = true;
                    }
                }
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                buttonClicked = false;
            }
        }
        /// <summary>
        /// Used when switching from preparing phase to the negotiating phase
        /// </summary>
        private void SwitchFromPreparing()
        {
            GameWorld.isPreparing = false;
            position = new Vector2(0, 330);
        }
        /// <summary>
        /// Sub method for the draw
        /// </summary>
        /// <param name="spriteBatch">From the monogame framework used to draw</param>
        public void DrawUseText(SpriteBatch spriteBatch)
        {
            if (used)
                spriteBatch.DrawString(GameWorld.font, useText, new Vector2(500, 0), Color.Black);
        }
    }
}
