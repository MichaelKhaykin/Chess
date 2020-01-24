using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Chess
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Sprite board;

        public static Piece[,] Grid = new Piece[8, 8];

        public static Texture2D Pixel;

        public static Vector2 OffSet;

        public static int SquareSize = 91;

        public Random Random = new Random();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();

            IsMouseVisible = true;

            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    Grid[i, j] = new Empty();
                }
            }

            OffSet = new Vector2(GraphicsDevice.Viewport.Width / 2 - 363 + 45.5f, GraphicsDevice.Viewport.Height / 2 - 364 + 45.5f);

            Pixel = new Texture2D(GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });

            var center = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
         
            //645, 225

            board = new Sprite(Content.Load<Texture2D>("desk"), center, Color.White, Vector2.One);

            var blackPawnTexture = Content.Load<Texture2D>("Black_Pawn");
            var whitePawnTexture = Content.Load<Texture2D>("White_Pawn");
            for(int i = 0; i < Grid.GetLength(1); i++)
            {
                Grid[6, i] = new Pawn(blackPawnTexture, new Vector2(OffSet.X + i * SquareSize, OffSet.Y + SquareSize * 6), Color.White, Vector2.One / 5)
                {
                    IsWhite = false
                };

                Grid[1, i] = new Pawn(whitePawnTexture, new Vector2(OffSet.X + i * SquareSize, OffSet.Y + SquareSize * 1), Color.White, Vector2.One / 5)
                {
                    IsWhite = true
                };
            }

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputManager.Mouse = Mouse.GetState();
            InputManager.Keyboard = Keyboard.GetState();

            // TODO: Add your update logic here

            Window.Title = $"X:{InputManager.Mouse.X}, Y:{InputManager.Mouse.Y}";

            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if (Grid[i, j].Type == PieceType.None) continue;

                    Grid[i, j].Update(gameTime);
                }
            }

            InputManager.OldMouse = InputManager.Mouse;
            InputManager.OldKeyboardState = InputManager.Keyboard;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();


            board.Draw(spriteBatch);
            for(int i = 0; i < Grid.GetLength(0); i++)
            {
                for(int j = 0; j < Grid.GetLength(1); j++)
                {
                    if (Grid[i, j].Type == PieceType.None) continue;

                    Grid[i, j].Draw(spriteBatch);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
