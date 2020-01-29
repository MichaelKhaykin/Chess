using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public abstract class Piece : Sprite
    {
        public abstract List<(int y, int x, bool isEmpassant)> PossibleMoves { get; }
        public abstract PieceType Type { get; }

        public (int y, int x) CurrentSpot
        {
            get
            {
                for (int i = 0; i < Game1.Grid.GetLength(0); i++)
                {
                    for (int j = 0; j < Game1.Grid.GetLength(1); j++)
                    {
                        if (Game1.Grid[i, j] == this)
                        {
                            return (i, j);
                        }
                    }
                }

                return (-1, -1);
            }
        }

        private bool isEmulating = false;
        public override Vector2 Position
        {
            get
            {
                return InputManager.ConvertGridIndexToPosition(CurrentSpot.x, CurrentSpot.y);
            }
        }
        public bool HasMoved { get; set; } = false;
        public bool ShouldBreakOutOfLoop { get; set; } = false;
        public PieceColor PieceColor { get; set; }
        public Piece(Texture2D texture, Vector2 position, Color color, Vector2 scale, PieceColor pieceColor) 
            : base(texture, position, color, scale)
        {
            PieceColor = pieceColor;
        }

        public override void Update(GameTime gameTime)
        {
            ShouldBreakOutOfLoop = false;
            if (InputManager.Mouse.LeftButton == ButtonState.Pressed && InputManager.OldMouse.LeftButton == ButtonState.Released)
            {
                if (isEmulating)
                {
                    var ogPossibleMoves = PossibleMoves;

                    Rectangle[] rectangles = new Rectangle[ogPossibleMoves.Count];
                    for (int i = 0; i < PossibleMoves.Count; i++)
                    {
                        var possibleMove = ogPossibleMoves[i];

                        var position = InputManager.ConvertGridIndexToPosition(possibleMove.x, possibleMove.y);

                        var rect = new Rectangle((int)(position.X - Game1.SquareSize / 2), (int)(position.Y - Game1.SquareSize / 2), Game1.SquareSize, Game1.SquareSize);
                        rectangles[i] = rect;
                    }

                    int index = -1;
                    for (int i = 0; i < rectangles.Length; i++)
                    {
                        if (rectangles[i].Contains(InputManager.Mouse.Position))
                        {
                            index = i;
                            break;
                        }
                    }

                    if (index == -1) return;

                    HasMoved = true;

                    isEmulating = false;

                    var (y, x, isEmpassant) = (ogPossibleMoves[index].y, ogPossibleMoves[index].x, ogPossibleMoves[index].isEmpassant);

                    Game1.Grid[CurrentSpot.y, CurrentSpot.x] = new Empty()
                    {
                        ShouldBreakOutOfLoop = true
                    };

                    Game1.Grid[y, x] = this;

                    if (isEmpassant)
                    {
                        if (Game1.Grid[y, x].PieceColor == PieceColor.White)
                        {
                            Game1.Grid[y - 1, x] = new Empty();
                        }
                        else
                        {
                            Game1.Grid[y + 1, x] = new Empty();
                        }
                    }

                    //Check for pawn case
                    if (Game1.Grid[y, x].Type == PieceType.Pawn
                        && (y == Game1.Grid.GetLength(1) - 1
                        || y == 0))
                    {
                        ((Pawn)Game1.Grid[y, x]).StartScanning = true;
                        return;
                    }

                    if(Game1.Grid[y, x] is Pawn)
                    {
                        if(((Pawn)Game1.Grid[y, x]).shouldSetFoundEnpassantToTrue)
                        {
                            ((Pawn)Game1.Grid[y, x]).HasFoundEnpassant = true;
                        }
                    }
                    Game1.PlayerTurn *= -1;
                }
                else if (HitBox.Contains(InputManager.Mouse.Position))
                {
                    //turn off all other emulated positions
                    for (int i = 0; i < Game1.Grid.GetLength(0); i++)
                    {
                        for (int j = 0; j < Game1.Grid.GetLength(1); j++)
                        {
                            if (Game1.Grid[i, j].Type == PieceType.None || Game1.Grid[i, j] == this) continue;

                            if (Game1.Grid[i, j].isEmulating)
                            {
                                Game1.Grid[i, j].isEmulating = false;
                            }
                        }
                    }

                    isEmulating = !isEmulating;

                }
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            if (isEmulating)
            {
                for (int i = 0; i < PossibleMoves.Count; i++)
                {
                    var position = InputManager.ConvertGridIndexToPosition(PossibleMoves[i].x, PossibleMoves[i].y);
                    spriteBatch.Draw(Game1.Pixel, position, new Rectangle(0, 0, Game1.SquareSize, Game1.SquareSize), Color.Green * 0.3f, 0f, new Vector2(Game1.SquareSize / 2), Vector2.One, SpriteEffects.None, 0f); ;
                }
            }

            base.Draw(spriteBatch);
        }
    }
}
