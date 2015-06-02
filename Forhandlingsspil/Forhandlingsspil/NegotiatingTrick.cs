﻿using Microsoft.Xna.Framework;
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
        private string useText;
        private bool buttonClicked = true;
        public string UseText
        {
            get { return useText; }
        }
        private bool used;
        private Texture2D iconTexture;

        public NegotiatingTrick(Vector2 position, float scale, float layer, Rectangle rect, bool isColleague, bool isUnion)
            : base(position, scale, layer, rect)
        {

            this.isTalkWithColleague = isColleague;
            this.isTradeUnion = isUnion;
            this.layer = 0.9f;
            //Temp
            if (isColleague)
                useText = "Snak med kollega";
            else if (isUnion)
                useText = "Brug forening";
            else
                useText = "Start nu";
            LoadContent(GameWorld.myContent);
            this.rect.Width = (int)GameWorld.smallFont.MeasureString(useText).X + 5;
            this.position.X = iconTexture.Width * 0.2f;
        }

        /// <summary>
        /// Used to load content when the game starts
        /// </summary>
        /// <param name="content">From the monogame framework, used to load the content</param>
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"black");
            if (isTradeUnion)
            {
                iconTexture = content.Load<Texture2D>(@"white");
            }
            else if (isTalkWithColleague)
            {
                iconTexture = content.Load<Texture2D>(@"SpeechBubble");
            }
            else
            {
                iconTexture = content.Load<Texture2D>(@"white");
            }
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

                spriteBatch.Draw(iconTexture, new Vector2(position.X - iconTexture.Width * 0.2f, position.Y), new Rectangle(0, 0, 100, 75), color, 0f, origin, 0.2f, SpriteEffects.None, 1.0f);
                spriteBatch.DrawString(GameWorld.smallFont, useText, position, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
            }
        }
        /// <summary>
        /// Used for the mouse control, Checking of position etc.
        /// </summary>
        private void MouseControl()
        {
            Vector2 mousePosition = Mouse.GetState().Position.ToVector2();

            if (mousePosition.X >= position.X && mousePosition.X <= position.X + GameWorld.smallFont.MeasureString(useText).X)
            {
                if (mousePosition.Y >= position.Y && mousePosition.Y <= position.Y + 20)
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
                    if (isTalkWithColleague || isTradeUnion)
                        Player.Instance.CreateNegotiatingTrick(this);
                }
                else if (!GameWorld.isPreparing)
                {
                    if (!used)
                    {
                        if (isTalkWithColleague)
                        {
                            Player.Instance.Salary += 500;
                            Negotiator.Instance.SwitchMood(-1);
                        }
                        if (isTradeUnion)
                        {
                            Player.Instance.Salary += 500;
                            Negotiator.Instance.SwitchMood(1);
                            TradeUnionStatementChoice();
                        }

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
            Negotiator.Instance.SwitchTexture("Idle", 0);
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

        private void TradeUnionStatementChoice()
        {
            switch (Negotiator.Instance.CurrentResponsKey)
            {
                case 0:
                    Negotiator.Instance.SwitchText("1");
                    Player.Instance.Salary += Player.Instance.HonestDic["HO0"].SalaryChangeValue;
                    break;

                case 1:
                    Negotiator.Instance.SwitchText("2");
                    Player.Instance.Salary += Player.Instance.HonestDic["HO1"].SalaryChangeValue;
                    break;

                case 2:
                    Negotiator.Instance.SwitchText("6");
                    Player.Instance.Salary += Player.Instance.HonestDic["HO2"].SalaryChangeValue;
                    break;

                case 3:
                    Negotiator.Instance.SwitchText("2");
                    Player.Instance.Salary += Player.Instance.HumorousDic["HU3"].SalaryChangeValue;
                    break;

                case 4:
                    Negotiator.Instance.SwitchText("5");
                    Player.Instance.Salary += Player.Instance.HonestDic["HO4"].SalaryChangeValue;
                    break;

                case 5:
                    Negotiator.Instance.SwitchText("2");
                    Player.Instance.Salary += Player.Instance.HumorousDic["HU5"].SalaryChangeValue;
                    break;

                default:
                    break;
            }
        }
    }
}
