using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

        public static int PlayerTurn = 1;
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

            InitTextures(Content);

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
            for (int i = 0; i < Grid.GetLength(1); i++)
            {
                Grid[6, i] = new Pawn(blackPawnTexture, Vector2.Zero, Color.White, Vector2.One, PieceColor.Black);

                Grid[1, i] = new Pawn(whitePawnTexture, Vector2.Zero, Color.White, Vector2.One, PieceColor.White);
            }

            #region Knights
            //Knight (White)
            Grid[0, 1] = new Knight(StaticInfo.WhiteKnightTexture, Vector2.Zero, Color.White, Vector2.One, PieceColor.White)
            {
                SpriteEffect = SpriteEffects.FlipHorizontally
            };

            //Second knight (White)
            Grid[0, 6] = new Knight(StaticInfo.WhiteKnightTexture, Vector2.Zero, Color.White, Vector2.One, PieceColor.White);

            //Black knight 
            Grid[7, 1] = new Knight(StaticInfo.BlackKnightTexture, Vector2.Zero, Color.White, Vector2.One, PieceColor.Black)
            {
                SpriteEffect = SpriteEffects.FlipHorizontally
            };
            //Second black knight
            Grid[7, 6] = new Knight(StaticInfo.BlackKnightTexture, Vector2.Zero, Color.White, Vector2.One, PieceColor.Black);
            #endregion

            #region Bishops
            Grid[0, 2] = new Bishop(StaticInfo.WhiteBishopTexture, Vector2.Zero, Color.White, Vector2.One, PieceColor.White);
            Grid[0, 5] = new Bishop(StaticInfo.WhiteBishopTexture, Vector2.Zero, Color.White, Vector2.One, PieceColor.White);

            Grid[7, 2] = new Bishop(StaticInfo.BlackBishopTexture, Vector2.Zero, Color.White, Vector2.One, PieceColor.Black);
            Grid[7, 5] = new Bishop(StaticInfo.BlackBishopTexture, Vector2.Zero, Color.White, Vector2.One, PieceColor.Black);
            #endregion

            #region Rooks
            Grid[0, 0] = new Rook(StaticInfo.WhiteRookTexture, Vector2.Zero, Color.White, Vector2.One, PieceColor.White);
            Grid[0, 7] = new Rook(StaticInfo.WhiteRookTexture, Vector2.Zero, Color.White, Vector2.One, PieceColor.White);

            Grid[7, 0] = new Rook(StaticInfo.BlackRookTexture, Vector2.Zero, Color.White, Vector2.One, PieceColor.Black);
            Grid[7, 7] = new Rook(StaticInfo.BlackRookTexture, Vector2.Zero, Color.White, Vector2.One, PieceColor.Black);
            #endregion

            // TODO: use this.Content to load your game content here
        }

        private void InitTextures(ContentManager content)
        {
            StaticInfo.WhiteKnightTexture = content.Load<Texture2D>("White_Knight");
            StaticInfo.BlackKnightTexture = content.Load<Texture2D>("Black_Knight");

            StaticInfo.WhiteBishopTexture = content.Load<Texture2D>("White_Bishop");
            StaticInfo.BlackBishopTexture = content.Load<Texture2D>("Black_Bishop");

            StaticInfo.WhiteRookTexture = content.Load<Texture2D>("White_Rook");
            StaticInfo.BlackRookTexture = content.Load<Texture2D>("Black_Rook");

            StaticInfo.WhiteQueenTexture = content.Load<Texture2D>("White_Queen");
            StaticInfo.BlackQueenTexture = content.Load<Texture2D>("Black_Queen");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputManager.Mouse = Mouse.GetState();
            InputManager.Keyboard = Keyboard.GetState();

            // TODO: Add your update logic here

            Window.Title = $"X:{InputManager.Mouse.X}, Y:{InputManager.Mouse.Y}";

            Piece piece = new Empty();
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if(Grid[i, j].Type == PieceType.Pawn 
                        && ((Pawn)Grid[i, j]).StartScanning)
                    {
                        piece = Grid[i, j];
                    }
                }
            }

            if (piece.Type != PieceType.None)
            {
                piece.Update(gameTime);
            }
            else
            {
                for (int i = 0; i < Grid.GetLength(0); i++)
                {
                    for (int j = 0; j < Grid.GetLength(1); j++)
                    {
                        if (Grid[i, j].Type == PieceType.None
                            || (Grid[i, j].PieceColor == PieceColor.Black && PlayerTurn == 1)
                            || (Grid[i, j].PieceColor == PieceColor.White && PlayerTurn == -1)) continue;

                        Grid[i, j].Update(gameTime);

                        if (Grid[i, j].ShouldBreakOutOfLoop)
                        {
                            i = Grid.GetLength(0);
                            break;
                        }
                    }
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
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
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
