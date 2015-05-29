using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Forhandlingsspil
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameWorld : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private static byte roundCounter;
        public static bool isPreparing;
        public static SpriteFont font;

        public static byte RoundCounter
        {
            get { return GameWorld.roundCounter; }
            set { GameWorld.roundCounter = value; }
        }

        public static ContentManager myContent;
        private bool clicked = false;
        public static bool gameOver;
        NegotiatingTrick union;
        NegotiatingTrick collegua;
        public static int windowWitdh;
        public static SpriteFont smallFont;

        public GameWorld()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            isPreparing = true;
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
            union = new NegotiatingTrick(new Vector2(0, 50), 1, 1, new Rectangle(0, 0, 50, 20), false, true);
            collegua = new NegotiatingTrick(new Vector2(0, 75), 1, 1, new Rectangle(0, 0, 50, 20), true, false);
            base.Initialize();

            graphics.PreferredBackBufferWidth = (1280 / 4 ) * 3;
            graphics.PreferredBackBufferHeight = (960 / 4 ) * 3;
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
            Player.Instance.LoadContent(Content);
            this.IsMouseVisible = true;
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
                collegua.Update(gameTime);
            }

            if (!isPreparing && !gameOver)
            {
                Player.Instance.Update(gameTime);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R))
                isPreparing = false;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if (isPreparing)
            {
                union.Draw(spriteBatch);
                collegua.Draw(spriteBatch);
            }
            if (!isPreparing && !gameOver)
            {
                spriteBatch.DrawString(font, roundCounter.ToString(), new Vector2(250, 0), Color.White);
            }
            //if (!isPreparing)
            Negotiator.Instance.Draw(spriteBatch);

            Player.Instance.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
