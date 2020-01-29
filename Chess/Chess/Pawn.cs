using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Pawn : Piece
    {
        public bool shouldSetFoundEnpassantToTrue = false;

        public bool HasFoundEnpassant = false;
        public override List<(int y, int x, bool isEmpassant)> PossibleMoves
        {
            get
            {
                List<(int y, int x, bool isEmpassant)> possibleMoves = new List<(int y, int x, bool isEmpassant)>();

                if (PieceColor == PieceColor.Black)
                {
                    //en passant 
                    if(CurrentSpot.y == 3)
                    {
                        if (CurrentSpot.x - 1 >= 0)
                        {
                            if (Game1.Grid[CurrentSpot.y, CurrentSpot.x - 1].PieceColor == PieceColor.White
                                && Game1.Grid[CurrentSpot.y, CurrentSpot.x - 1].Type == PieceType.Pawn
                                && !HasFoundEnpassant
                                && Game1.Grid[CurrentSpot.y - 1, CurrentSpot.x - 1] is Empty)
                            {
                                possibleMoves.Add((CurrentSpot.y - 1, CurrentSpot.x - 1, true));
                                shouldSetFoundEnpassantToTrue = true;
                            }
                        }

                        if (CurrentSpot.x + 1 < Game1.Grid.GetLength(0))
                        {
                            if (Game1.Grid[CurrentSpot.y, CurrentSpot.x + 1].PieceColor == PieceColor.White
                                && Game1.Grid[CurrentSpot.y, CurrentSpot.x + 1].Type == PieceType.Pawn
                                && !HasFoundEnpassant
                                && Game1.Grid[CurrentSpot.y - 1, CurrentSpot.x + 1] is Empty)
                            {
                                possibleMoves.Add((CurrentSpot.y - 1, CurrentSpot.x + 1, true));
                                shouldSetFoundEnpassantToTrue = true;
                            }
                        }
                    }


                    var start = CurrentSpot.y - 1;
                    var end = CurrentSpot.y - (HasMoved ? 1 : 2);

                    if (start < 0) return possibleMoves;

                    for (int i = start; i >= end; i--)
                    {
                        if (Game1.Grid[i, CurrentSpot.x].Type != PieceType.None)
                        {
                            break;
                        }
                        possibleMoves.Add((i, CurrentSpot.x, false));
                    }

                    var diagonalLeft = (CurrentSpot.y - 1, CurrentSpot.x - 1);
                    if (diagonalLeft.Item2 > 0)
                    {
                        if (Game1.Grid[diagonalLeft.Item1, diagonalLeft.Item2].Type != PieceType.None
                            && Game1.Grid[diagonalLeft.Item1, diagonalLeft.Item2].PieceColor != PieceColor)
                        {
                            possibleMoves.Add((diagonalLeft.Item1, diagonalLeft.Item2, false));
                        }
                    }

                    var diagonalRight = (CurrentSpot.y - 1, CurrentSpot.x + 1);
                    if (diagonalRight.Item2 < Game1.Grid.GetLength(0)
                        && Game1.Grid[diagonalRight.Item1, diagonalRight.Item2].PieceColor != PieceColor)
                    {
                        if (Game1.Grid[diagonalRight.Item1, diagonalRight.Item2].Type != PieceType.None)
                        {
                            possibleMoves.Add((diagonalRight.Item1, diagonalRight.Item2, false));
                        }
                    }
                }
                else
                {
                    //handle enpassant
                    if (CurrentSpot.y == 4)
                    {
                        if (CurrentSpot.x - 1 >= 0)
                        {
                            if (Game1.Grid[CurrentSpot.y, CurrentSpot.x - 1].PieceColor == PieceColor.Black
                                && Game1.Grid[CurrentSpot.y, CurrentSpot.x - 1].Type == PieceType.Pawn
                                && !HasFoundEnpassant
                                && Game1.Grid[CurrentSpot.y + 1, CurrentSpot.x - 1] is Empty)
                            {
                                possibleMoves.Add((CurrentSpot.y + 1, CurrentSpot.x - 1, true));
                                shouldSetFoundEnpassantToTrue = true;
                            }
                        }

                        if (CurrentSpot.x + 1 < Game1.Grid.GetLength(0))
                        {
                            if (Game1.Grid[CurrentSpot.y, CurrentSpot.x + 1].PieceColor == PieceColor.Black
                                && Game1.Grid[CurrentSpot.y, CurrentSpot.x + 1].Type == PieceType.Pawn
                                && !HasFoundEnpassant
                                && Game1.Grid[CurrentSpot.y + 1, CurrentSpot.x + 1] is Empty)
                            {
                                possibleMoves.Add((CurrentSpot.y + 1, CurrentSpot.x + 1, true));
                                shouldSetFoundEnpassantToTrue = true;
                            }
                        }
                    }

                    var start = CurrentSpot.y + 1;
                    var end = CurrentSpot.y + (HasMoved ? 1 : 2);

                    if (start >= Game1.Grid.GetLength(1)) return possibleMoves;

                    for (int i = start; i <= end; i++)
                    {
                        if (Game1.Grid[i, CurrentSpot.x].Type != PieceType.None)
                        {
                            break;
                        }
                        possibleMoves.Add((i, CurrentSpot.x, false));
                    }

                    var diagonalLeft = (CurrentSpot.y + 1, CurrentSpot.x - 1);
                    if (diagonalLeft.Item2 > 0)
                    {
                        if (Game1.Grid[diagonalLeft.Item1, diagonalLeft.Item2].Type != PieceType.None
                            && Game1.Grid[diagonalLeft.Item1, diagonalLeft.Item2].PieceColor != PieceColor)
                        {
                            possibleMoves.Add((diagonalLeft.Item1, diagonalLeft.Item2, false));
                        }
                    }

                    var diagonalRight = (CurrentSpot.y + 1, CurrentSpot.x + 1);
                    if (diagonalRight.Item2 < Game1.Grid.GetLength(0))
                    {
                        if (Game1.Grid[diagonalRight.Item1, diagonalRight.Item2].Type != PieceType.None
                            && Game1.Grid[diagonalRight.Item1, diagonalRight.Item2].PieceColor != PieceColor)
                        {
                            possibleMoves.Add((diagonalRight.Item1, diagonalRight.Item2, false));
                        }
                    }
                }
               
                return possibleMoves;
            }
        }
        public override PieceType Type { get; }

        public bool StartScanning = false;
        public Pawn(Texture2D texture, Vector2 position, Color color, Vector2 scale, PieceColor pieceColor)
            : base(texture, position, color, scale, pieceColor)
        {
            Type = PieceType.Pawn;
        }

        public override void Update(GameTime gameTime)
        {
            if(StartScanning)
            {
                //Temporary choosing of new piece to become 
                if(InputManager.Keyboard.IsKeyDown(Keys.Q))
                {
                    var textureToUse = PieceColor == PieceColor.White ? StaticInfo.WhiteQueenTexture : StaticInfo.BlackQueenTexture;
                    Game1.Grid[CurrentSpot.y, CurrentSpot.x] = new Queen(textureToUse, Vector2.Zero, Color.White, Vector2.One, PieceColor);

                    Game1.PlayerTurn *= -1;
                    StartScanning = false;
                }
                else if(InputManager.Keyboard.IsKeyDown(Keys.R))
                {
                    var textureToUse = PieceColor == PieceColor.White ? StaticInfo.WhiteRookTexture : StaticInfo.BlackRookTexture;
                    Game1.Grid[CurrentSpot.y, CurrentSpot.x] = new Rook(textureToUse, Vector2.Zero, Color.White, Vector2.One, PieceColor);

                    Game1.PlayerTurn *= -1;
                    StartScanning = false;
                }
                else if(InputManager.Keyboard.IsKeyDown(Keys.B))
                {
                    var textureToUse = PieceColor == PieceColor.White ? StaticInfo.WhiteBishopTexture : StaticInfo.BlackBishopTexture;
                    Game1.Grid[CurrentSpot.y, CurrentSpot.x] = new Bishop(textureToUse, Vector2.Zero, Color.White, Vector2.One, PieceColor);

                    Game1.PlayerTurn *= -1;
                    StartScanning = false;
                }
                else if(InputManager.Keyboard.IsKeyDown(Keys.K))
                {
                    var textureToUse = PieceColor == PieceColor.White ? StaticInfo.WhiteKnightTexture : StaticInfo.BlackKnightTexture;
                    Game1.Grid[CurrentSpot.y, CurrentSpot.x] = new Knight(textureToUse, Vector2.Zero, Color.White, Vector2.One, PieceColor);

                    Game1.PlayerTurn *= -1;
                    StartScanning = false;
                }
            }

            base.Update(gameTime);
        }
    }
}
