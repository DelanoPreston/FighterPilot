using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameLibrary;

namespace FighterPilot
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    //public class Globals
    //{
    //    public SpriteBatch mySpriteBatch;
    //    public static ContentManager myContentManager;
    //    public GraphicsDeviceManager graphics;
    //}
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputHandlerKeyboard inputHandler;

        NPC enemy1;
        NPC enemy2;
        Player player;

        SpriteFont font;
        
        Texture2D background;
        Camera camera;

        Texture2D testTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            inputHandler = new InputHandlerKeyboard(this);

            inputHandler.Initialize();



            base.Initialize();
        }

        

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //SpriteTexture = Content.Load<Texture2D>("Fighter");
            Viewport viewport = graphics.GraphicsDevice.Viewport;
            //origin.X = SpriteTexture.Width / 2;
            //origin.Y = SpriteTexture.Height / 2;
            //screenpos.X = viewport.Width / 2;
            //screenpos.Y = viewport.Height / 2;
            graphics.PreferredBackBufferWidth = 800;// 1366;
            graphics.PreferredBackBufferHeight = 600;// 768;
            graphics.ApplyChanges();
            //graphics.ToggleFullScreen();

            IsMouseVisible = true;

            camera = new Camera();

            background = Content.Load<Texture2D>("Galaxy background");
            font = Content.Load<SpriteFont>("Font");

            Random random = new Random();

            player = new Player(random);
            player.LoadContent(GraphicsDevice, this.Content);




            enemy1 = new NPC(random);
            enemy1.LoadContent(GraphicsDevice, this.Content);//, new Vector2(950, 950));
            enemy2 = new NPC(random);
            enemy2.LoadContent(GraphicsDevice, this.Content);//, new Vector2(900, 950));


            //testTexture = new Texture2D(graphics.GraphicsDevice, 32, 32);
            //Color[] red = new Color[32 * 32];
            //for (int i = 0; i < 32 * 32; i++)
            //{
            //    if (i < 64 || i > 32 * 30 - 1 || i % 32 < 2 || i % 32 > 29)
            //        red[i] = new Color(64, 64, 64, 64);
            //    else
            //        red[i] = new Color(128, 128, 128, 64);
            //}
            //testTexture.SetData<Color>(red);

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            camera.Update(graphics.GraphicsDevice, player.SPosition);

            inputHandler.Update(gameTime);

            player.PlayerInput(gameTime);
            enemy1.Update(gameTime, player);
            enemy2.Update(gameTime, enemy1);

            //if (InputHandler.KeyDown(Keys.E))
            //    enemy1.enemyFighterState = enumFighterState.Flee;
            //else
            //    enemy1.enemyFighterState = enumFighterState.Follow;

            base.Update(gameTime);
        }

        string displayText;
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, camera.TransformMatrix(graphics.GraphicsDevice));
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);//sketchy
            //spriteBatch.Draw(testTexture, new Vector2(800, 800), Color.White);//sketchy



            player.Draw(spriteBatch);
            enemy1.Draw(spriteBatch);
            enemy2.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();
            displayText = "Curr Rotation " + player.thisShip.modules[0].position.X;
            spriteBatch.DrawString(font, displayText, new Vector2(0, 0), Color.White);
            displayText = "Desi Rotation " + player.thisShip.modules[0].position.Y;
            spriteBatch.DrawString(font, displayText, new Vector2(0, 20), Color.White);
            displayText = "my Position (" + string.Format("{0:0.0}", player.SPosition.X) + ", " + string.Format("{0:0.0}", player.SPosition.Y) + ")";
            spriteBatch.DrawString(font, displayText, new Vector2(0, 40), Color.White);
            //displayText = "enemy Position (" + string.Format("{0:0.0}", enemy1.ePosition.X) + ", " + string.Format("{0:0.0}", enemy1.ePosition.Y) + ")";
            //spriteBatch.DrawString(font, displayText, new Vector2(0, 60), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
