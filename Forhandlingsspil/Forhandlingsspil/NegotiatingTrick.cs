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
        #region Fields
        private bool isTalkWithColleague;
        private bool isTradeUnion;
        private string useText;
        private bool buttonClicked = true;
        private bool used;
        private Texture2D iconTexture;

        private Rectangle iconRect;
        private float iconScale;
        #endregion
        #region Properties
        public bool IsTradeUnion
        {
            get { return isTradeUnion; }
        }
        public bool Used
        {
            get { return used; }
        }
        #endregion

        //Constructor
        public NegotiatingTrick(Vector2 position, float scale, float layer, Rectangle rect, bool isColleague, bool isUnion)
            : base(position, scale, layer, rect)
        {

            this.isTalkWithColleague = isColleague;
            this.isTradeUnion = isUnion;
            this.layer = 0.9f;

            //The text for the negotiationtrick and the rectangle size and scale for the icon, is changed based on what type of negotiationtrick the object is.
            if (isColleague)
            {
                useText = "Snakket med kollega";
                iconRect = new Rectangle(0, 0, 100, 75);
                iconScale = 0.6f;
            }
            else if (isUnion)
            {
                useText = "Tilmeldt PROSA";
                iconRect = new Rectangle(0, 0, 100, 25);
                iconScale = 0.8f;
            }
            else
            {
                useText = "Ingen forberedelse";
                iconRect = new Rectangle(0, 0, 300, 300);
                iconScale = 0.15f;
            }

            LoadContent(GameWorld.myContent);

            this.rect.Width = (int)GameWorld.mediumFont.MeasureString(useText).X + 5;
            this.position.X = iconTexture.Width * iconScale + GameWorld.windowWitdh / 2 - 20;

        }

        /// <summary>
        /// Used to load content when the game starts
        /// </summary>
        /// <param name="content">From the monogame framework, used to load the content</param>
        public override void LoadContent(ContentManager content)
        {
            //The texture for the button for the negotiatingtrick is loaded.
            texture = content.Load<Texture2D>(@"black");
            
            //The texture for the icon for the negotiatingtrick is loaded, based on what type of negotiatingtrick the object is.
            if (isTradeUnion)
            {
                iconTexture = content.Load<Texture2D>(@"prosa");
            }
            else if (isTalkWithColleague)
            {
                iconTexture = content.Load<Texture2D>(@"SpeechBubble");
            }
            else
            {
                iconTexture = content.Load<Texture2D>(@"GO");
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
            //The button for the negotiatingtrikc gets drawn to the screen as long as it isn't used.
            if (!used && !GameWorld.gameOver)
            {
                base.Draw(spriteBatch);

                spriteBatch.Draw(iconTexture, new Vector2(position.X - iconTexture.Width * iconScale, position.Y - (iconTexture.Height * iconScale) / 2 + (this.rect.Height / 2)), iconRect, color, 0f, origin, iconScale, SpriteEffects.None, 1.0f);
                spriteBatch.DrawString(GameWorld.mediumFont, useText, position, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
            }
        }

        /// <summary>
        /// Used for the mouse control, Checking of position etc.
        /// </summary>
        private void MouseControl()
        {
            Vector2 mousePosition = Mouse.GetState().Position.ToVector2();

            //If the mouse is positioned somewhere on the negotiatingtrick button, runs the MouseClick method.
            if (mousePosition.X >= position.X && mousePosition.X <= position.X + GameWorld.mediumFont.MeasureString(useText).X)
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

                //If the game is still in the preparing fase, runs the contained code when the negotiatingtrick button is clicked.
                if (GameWorld.isPreparing)
                {
                    //Switches the game from the preparing fase to the negotiation fase.
                    SwitchFromPreparing();

                    //makes the negotiatingtrick in the player class into the chosen negotiatingtrick.
                    if (isTalkWithColleague || isTradeUnion)
                        Player.Instance.CreateNegotiatingTrick(this);
                }
                //If the game is in the negotiation fase, runs the contained code when the negotiatingtrick button is clicked.
                else if (!GameWorld.isPreparing)
                {
                    //If the button has not been used, changes variables for the player and the negotiator, based on the type of negotiatingtrick used.
                    if (!used)
                    {
                        if (isTalkWithColleague)
                        {
                            Player.Instance.Salary += 500;
                            Negotiator.Instance.SwitchMood(-1);
                        }
                        if (isTradeUnion)
                        {
                            Player.Instance.Salary += 3000;
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
            position = new Vector2(iconTexture.Width * iconScale, 550);
            Negotiator.Instance.SwitchTexture("Idle", 0);

            if (isTradeUnion)
            {
                useText = "Spørg PROSA";
            }
        }

        /// <summary>
        /// This method is used to decide with statement should be chosen when the tradeunion negotiatingtrick is used.
        /// </summary>
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
