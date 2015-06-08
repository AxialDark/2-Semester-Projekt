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
        #region fields
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
        private Texture2D statisticLinkText;
        private Texture2D introText;
        private string introString;
        public static SpriteFont introFont;
        #endregion

        public GameWorld()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //Starts the game in the preparing phase
            isPreparing = true;

            //The introduktion to the game
            introString = "Du er en gennemsnitlig IT-medarbejder, " + Environment.NewLine +
            "med 10 års erfaring og du føler at det er på tide" + Environment.NewLine +
            "med en lønforhøjelse. " + Environment.NewLine + Environment.NewLine +
            "Din nuværende løn er på 35.000kr før skat. " + Environment.NewLine + Environment.NewLine +
            "Der er stadig noget tid til at lønforhandlingen starter, " + Environment.NewLine +
            "så hvordan har du forberedt dig?";

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

            this.Window.Title = "Forhandleren";

            //Makes the gamewindow
            graphics.PreferredBackBufferWidth = (1280 / 4) * 3;
            graphics.PreferredBackBufferHeight = (960 / 4) * 3 + 20;
            graphics.ApplyChanges();
            windowWitdh = Window.ClientBounds.Width;

            //Creates 3 NegotiatingTricks
            union = new NegotiatingTrick(new Vector2(0, 150), 1, 1, new Rectangle(0, 0, 50, 25), false, true);
            colleague = new NegotiatingTrick(new Vector2(0, 200), 1, 1, new Rectangle(0, 0, 50, 25), true, false);
            noTrick = new NegotiatingTrick(new Vector2(0, 250), 1, 1, new Rectangle(0, 0, 50, 25), false, false);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //Loads the fonts for writing
            font = Content.Load<SpriteFont>(@"SpriteFont");
            smallFont = Content.Load<SpriteFont>(@"SmallFont");
            mediumFont = Content.Load<SpriteFont>(@"MediumFont");
            introFont = Content.Load<SpriteFont>(@"IntroFont");

            //Loads content for Player and Negotiator
            Player.Instance.LoadContent(Content);
            Negotiator.Instance.LoadContent(Content);

            //MOUSE IS VISIBLE!!!
            this.IsMouseVisible = true;

            //DIV texture loaded
            statisticText = Content.Load<Texture2D>(@"Statistik");
            fbIcon = Content.Load<Texture2D>(@"Facebook");
            tIcon = Content.Load<Texture2D>(@"Twitter");
            statisticLinkText = Content.Load<Texture2D>(@"prosa");
            introText = Content.Load<Texture2D>(@"Intro");
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
            //If the player clicks the logo at the end screen, open up browser to hyperlink
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !clicked && gameOver)
            {
                if(Mouse.GetState().Position.X <= windowWitdh - 20 && Mouse.GetState().Position.X >= windowWitdh - statisticLinkText.Width - 20)
                {
                    if(Mouse.GetState().Position.Y <= Window.ClientBounds.Height - 20 && Mouse.GetState().Position.Y >= Window.ClientBounds.Height - statisticLinkText.Height - 20)
                    {
                        try
                        {
                            Process.Start("https://www.prosa.dk/raadgivning/loenstatistik/");
                        }
                        catch { }
                    }
                }
                clicked = true;
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released)
                clicked = false;

            //Calls update for the stuff that only need to update during the preparing phase
            if (isPreparing)
            {
                union.Update(gameTime);
                colleague.Update(gameTime);
                noTrick.Update(gameTime);
            }

            //Calls update for the stuff that only need to update during the negotiating phase
            if (!isPreparing && !gameOver)
            {
                Player.Instance.Update(gameTime);
                Negotiator.Instance.Update(gameTime);
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

            //Draws what needs to be drawn only during the preparing phase.
            if (isPreparing)
            {
                union.Draw(spriteBatch);
                colleague.Draw(spriteBatch);
                noTrick.Draw(spriteBatch);

                spriteBatch.Draw(introText, new Vector2(20, 20), new Rectangle(0, 0, 320, 460), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.99f);
                spriteBatch.DrawString(introFont, introString, new Vector2(30, 30), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
            }

            //Only draws the negotiator when the game isn't over and has switched to the end screen.
            if(endTimer.ElapsedMilliseconds < 4000)
            {
                Negotiator.Instance.Draw(spriteBatch);
            }

            Player.Instance.Draw(spriteBatch);

            //Within this region is everything that gets drawn when the game ends.
            #region Game over

            if(gameOver)
            {
                //Makes string for writing out the salary.
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

                //Draws the end screen four seconds after the game ends.
                if (endTimer.ElapsedMilliseconds >= 4000)
                {
                    //Draws out the salarytext and graph.
                    spriteBatch.DrawString(font, endTextSalary, new Vector2(windowWitdh / 2 - font.MeasureString(endTextSalary).X / 2, statisticText.Height + 100), Color.Gold, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
                    spriteBatch.Draw(statisticText, new Vector2(windowWitdh / 2 - (statisticText.Width) / 2, 80), new Rectangle(0, 0, 689, 457), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
                    
                    //Draws the twitter and facebook sharebuttons.
                    spriteBatch.Draw(fbIcon, new Vector2(20, Window.ClientBounds.Height - (fbIcon.Height * 0.25f) - 20), new Rectangle(0, 0, 300, 258), Color.White, 0, Vector2.Zero, 0.25f, SpriteEffects.None, 1.0f);
                    spriteBatch.Draw(tIcon, new Vector2(20 + (fbIcon.Width * 0.25f) + 20 , Window.ClientBounds.Height - (tIcon.Height * 0.25f) - 20), new Rectangle(0, 0, 300, 258), Color.White, 0, Vector2.Zero, 0.25f, SpriteEffects.None, 1.0f);
                    
                    //Draws for the internet link and accompanying.
                    spriteBatch.Draw(statisticLinkText, new Vector2(windowWitdh - statisticLinkText.Width - 20, Window.ClientBounds.Height - statisticLinkText.Height - 20), new Rectangle(0, 0, 100, 25), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
                    spriteBatch.DrawString(mediumFont, "Tryk på logoet!", new Vector2(windowWitdh - mediumFont.MeasureString("Tryk på logoet!").X - 20, Window.ClientBounds.Height - statisticLinkText.Height - 20 - mediumFont.MeasureString("R").Y), Color.Gold, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
                }
            }

            #endregion
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
