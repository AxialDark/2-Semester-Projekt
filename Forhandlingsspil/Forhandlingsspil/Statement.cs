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
        private int salaryChangeValue;
        private short moodChangeValue;
        private string statementText;
        private StatementType type;

        private bool buttonClicked = false;
        private bool question = false;
        private DateTime remove = DateTime.Now;

        public Statement(Vector2 position, float scale, float layer, Rectangle rect, StatementType type, string text)
            : base(position, scale, layer, rect)
        {
            this.statementText = text;
            this.type = type;
            LoadContent(GameWorld.myContent);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"white");
            base.LoadContent(content);
        }
        public override void Update(GameTime gameTime)
        {
            RemoveText();
            MouseControl();
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(GameWorld.font, statementText, position, Color.Black);

            if (question)
            {
                spriteBatch.DrawString(GameWorld.font, "You clicked " + type.ToString(), new Vector2(200, 200), Color.Black);
            }
        }

        private void MouseControl()
        {
            Vector2 mousePosition = Mouse.GetState().Position.ToVector2();

            if (mousePosition.X >= position.X && mousePosition.X <= position.X + 200)
            {
                if (mousePosition.Y >= position.Y && mousePosition.Y <= position.Y + 50)
                {
                    color = Color.Red;
                    MouseClick();
                }
                else { color = Color.White; }
            }
            else { color = Color.White; }
        }

        private void MouseClick()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !buttonClicked)
            {
                color = Color.Blue;
                buttonClicked = true;
                if (!question)
                    remove = DateTime.Now.AddSeconds(5);
                question = true;
                Player.Instance.SwitchStatements();
                
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                buttonClicked = false;
            }
        }
        private void RemoveText()
        {
            if (remove <= DateTime.Now)
                question = false;
        }

    }
}
