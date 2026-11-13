using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Topic_5___more_classe;



enum Screen
{
    Title,
    House,
    End
}

namespace Topic_5___more_classes
{
    public class Game1 : Game
    {

        Screen screen;
        Rectangle window; // Initialize this to be at (0, 0) and 800 x 600.
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        List<Texture2D> ghostTextures;
        List<Ghost> ghosts;

        Texture2D playerTexture;
        Texture2D backgroundTexture;
        Texture2D startTexture;
        Texture2D endTexture;

        MouseState mouseState;

        Random generator;
        private string i;

        Ghost ghost1;

        KeyboardState keyboardState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            window = new Rectangle(0, 0, 800, 600);
            generator = new Random();
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;

            _graphics.ApplyChanges();

            screen = Screen.Title;

            ghostTextures = new List<Texture2D>();

            ghosts = new List<Ghost>();
            
            int size = generator.Next(40, 40);

            ghost1 = new Ghost(ghostTextures, new Rectangle(generator.Next(150), generator.Next(250), size, size));

            base.Initialize();

           
            for(int i = 0; i < 25; i++)
            {
                ghosts.Add(new Ghost(ghostTextures, new Rectangle(generator.Next(800), generator.Next(500), size, size)));
            }

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            backgroundTexture = Content.Load<Texture2D>("Images/haunted-background");
            playerTexture = Content.Load<Texture2D>("Images/mario");
            startTexture = Content.Load<Texture2D>("Images/bedbackground");
            endTexture = Content.Load<Texture2D>("Images/endScreen");
            playerTexture = Content.Load<Texture2D>("Images/mario");

            for (int i = 1; i < 9; i++)
            {
                ghostTextures.Add(Content.Load<Texture2D>("Images/boo-move-" + i));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            mouseState = Mouse.GetState();
            

            if (screen == Screen.Title)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                    screen = Screen.House;

            }
            else if (screen == Screen.House)
            {
                foreach (Ghost ghost in ghosts)
                {
                    ghost.Update(gameTime, mouseState);
                    if (ghost.Contains(mouseState.Position))
                        screen = Screen.End;
                }
                    

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTexture, window, Color.White);
            ghost1.Draw(_spriteBatch);
            if (screen == Screen.Title)
                _spriteBatch.Draw(startTexture, window, Color.White);
            else if (screen == Screen.House)
            {
                _spriteBatch.Draw(backgroundTexture, window, Color.White);
                foreach (Ghost ghost in ghosts)
                    ghost.Draw(_spriteBatch);
            }
            else
                _spriteBatch.Draw(endTexture, window, Color.White);
            //_spriteBatch.Draw(playerTexture, (mouseState.Position()), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
