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

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"black");
            //base.LoadContent(content);
        }
        public override void Update(GameTime gameTime)
        {
            MouseControl();
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(GameWorld.font, useText, position, Color.White);
        }
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

        private void SwitchFromPreparing()
        {
            GameWorld.isPreparing = false;
            position = new Vector2(0, 330);
        }

        public void DrawUseText(SpriteBatch spriteBatch)
        {
            if (used)
                spriteBatch.DrawString(GameWorld.font, useText, new Vector2(500, 0), Color.Black);
        }
    }
}
