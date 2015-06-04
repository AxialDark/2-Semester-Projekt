using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Forhandlingsspil
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameWorld : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static bool isPreparing;
        public static SpriteFont font;

        public static ContentManager myContent;
        private bool clicked = false;
        public static bool gameOver;
        NegotiatingTrick union;
        NegotiatingTrick colleague;
        NegotiatingTrick noTrick;
        public static int windowWitdh;
        public static SpriteFont smallFont;

        public static SpriteFont mediumFont;
        private Texture2D statisticText;
        public static Stopwatch endTimer;
        private Texture2D fbIcon;
        private Texture2D tIcon;

        public GameWorld()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            isPreparing = true;

            endTimer = new Stopwatch();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            myContent = Content;
            base.Initialize();

            union = new NegotiatingTrick(new Vector2(0, 50), 1, 1, new Rectangle(0, 0, 50, 20), false, true);
            colleague = new NegotiatingTrick(new Vector2(0, 100), 1, 1, new Rectangle(0, 0, 50, 20), true, false);
            noTrick = new NegotiatingTrick(new Vector2(0, 150), 1, 1, new Rectangle(0, 0, 50, 20), false, false);

            graphics.PreferredBackBufferWidth = (1280 / 4 ) * 3;
            graphics.PreferredBackBufferHeight = (960 / 4 ) * 3 + 20;
            graphics.ApplyChanges();
            windowWitdh = Window.ClientBounds.Width;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>(@"SpriteFont");
            smallFont = Content.Load<SpriteFont>(@"SmallFont");
            mediumFont = Content.Load<SpriteFont>(@"MediumFont");
            Player.Instance.LoadContent(Content);
            Negotiator.Instance.LoadContent(Content);
            this.IsMouseVisible = true;
            statisticText = Content.Load<Texture2D>(@"Statistik");
            fbIcon = Content.Load<Texture2D>(@"Facebook");
            tIcon = Content.Load<Texture2D>(@"Twitter");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !clicked)
            {
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released)
                clicked = false;

            if (isPreparing)
            {
                union.Update(gameTime);
                colleague.Update(gameTime);
                noTrick.Update(gameTime);
            }

            if (!isPreparing && !gameOver)
            {
                Player.Instance.Update(gameTime);
                Negotiator.Instance.Update(gameTime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                isPreparing = false;
                Negotiator.Instance.SwitchTexture("Idle", 0);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            if (isPreparing)
            {
                union.Draw(spriteBatch);
                colleague.Draw(spriteBatch);
                noTrick.Draw(spriteBatch);
            }

            if(endTimer.ElapsedMilliseconds < 2000)
            {
                Negotiator.Instance.Draw(spriteBatch);
            }

            Player.Instance.Draw(spriteBatch);

            #region Game over

            if(gameOver)
            {
                string endTextSalary = "Du forhandlede dig frem til " + Player.Instance.Salary + "kr, hvilket er en forskel på " + (Player.Instance.Salary - 35000) + "kr i forhold til din forrige løn.";

                if (Player.Instance.NegotiatingTrick != null)
                {
                    if(!Player.Instance.NegotiatingTrick.IsTradeUnion && !Player.Instance.NegotiatingTrick.Used)
                    {
                        endTextSalary += Environment.NewLine + Environment.NewLine + "Men hvis du havde været medlem af en fagforening, ville du normalvis kunne få omkring 42000kr.";
                    }
                    else if(Player.Instance.NegotiatingTrick.IsTradeUnion && !Player.Instance.NegotiatingTrick.Used)
                    {
                        endTextSalary += Environment.NewLine + Environment.NewLine + "Men hvis du havde brugt din fagforening, kunne du have fået flere penge.";
                    }
                    else
                    {
                        endTextSalary += Environment.NewLine + Environment.NewLine + "Godt du havde tilmeldt dig en fagforening samt benyttet dig af den!";
                    }
                }
                if (endTimer.ElapsedMilliseconds >= 2000)
                {
                    spriteBatch.DrawString(font, endTextSalary, new Vector2(windowWitdh / 2 - font.MeasureString(endTextSalary).X / 2, statisticText.Height + 100), Color.Gold, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
                    spriteBatch.Draw(statisticText, new Vector2(windowWitdh / 2 - (statisticText.Width) / 2, 80), new Rectangle(0, 0, 689, 457), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
                    spriteBatch.Draw(fbIcon, new Vector2(20, Window.ClientBounds.Height - (fbIcon.Height * 0.25f) - 20), new Rectangle(0, 0, 300, 258), Color.White, 0, Vector2.Zero, 0.25f, SpriteEffects.None, 1.0f);
                    spriteBatch.Draw(tIcon, new Vector2(20 + (fbIcon.Width * 0.25f) + 20 , Window.ClientBounds.Height - (tIcon.Height * 0.25f) - 20), new Rectangle(0, 0, 300, 258), Color.White, 0, Vector2.Zero, 0.25f, SpriteEffects.None, 1.0f);
                }
            }

            #endregion
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
