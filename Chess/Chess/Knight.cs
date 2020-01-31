using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Knight : Piece
    {
        public override List<(int y, int x, object data)> PossibleMoves
        {
            get
            {
                if (PossibleUnCheckMove != (-1, -1)) return new List<(int y, int x, object data)>() { (PossibleUnCheckMove.y, PossibleUnCheckMove.x, false) };

                List<(int y, int x, object data)> possibleMoves = new List<(int y, int x, object data)>();

                if (CurrentSpot.x + 2 < Game1.Grid.GetLength(0) && CurrentSpot.y + 1 < Game1.Grid.GetLength(1))
                {
                    if (Game1.Grid[CurrentSpot.y + 1, CurrentSpot.x + 2].PieceColor != PieceColor)
                    {
                        possibleMoves.Add((CurrentSpot.y + 1, CurrentSpot.x + 2, false));
                    }
                    else if (Game1.Grid[CurrentSpot.y + 1, CurrentSpot.x + 2].PieceColor != PieceColor.None)
                    {
                        PiecesProtectedByMe.Add((CurrentSpot.y + 1, CurrentSpot.x + 2));
                    }
                }

                if (CurrentSpot.x + 2 < Game1.Grid.GetLength(0) && CurrentSpot.y - 1 >= 0)
                {
                    if (Game1.Grid[CurrentSpot.y - 1, CurrentSpot.x + 2].PieceColor != PieceColor)
                    {
                        possibleMoves.Add((CurrentSpot.y - 1, CurrentSpot.x + 2, false));
                    }
                    else if (Game1.Grid[CurrentSpot.y - 1, CurrentSpot.x + 2].PieceColor != PieceColor.None)
                    {
                        PiecesProtectedByMe.Add((CurrentSpot.y - 1, CurrentSpot.x + 2));
                    }
                }

                if (CurrentSpot.x - 2 >= 0 && CurrentSpot.y - 1 >= 0)
                {
                    if (Game1.Grid[CurrentSpot.y - 1, CurrentSpot.x - 2].PieceColor != PieceColor)
                    {
                        possibleMoves.Add((CurrentSpot.y - 1, CurrentSpot.x - 2, false));
                    }
                    else if (Game1.Grid[CurrentSpot.y - 1, CurrentSpot.x - 2].PieceColor != PieceColor.None)
                    {
                        PiecesProtectedByMe.Add((CurrentSpot.y - 1, CurrentSpot.x - 2));
                    }
                }

                if (CurrentSpot.x - 2 >= 0 && CurrentSpot.y + 1 < Game1.Grid.GetLength(1))
                {
                    if (Game1.Grid[CurrentSpot.y + 1, CurrentSpot.x - 2].PieceColor != PieceColor)
                    {
                        possibleMoves.Add((CurrentSpot.y + 1, CurrentSpot.x - 2, false));
                    }
                    else
                    {
                        PiecesProtectedByMe.Add((CurrentSpot.y + 1, CurrentSpot.x - 2));
                    }
                }

                if (CurrentSpot.x - 1 >= 0 && CurrentSpot.y - 2 >= 0)
                {
                    if (Game1.Grid[CurrentSpot.y - 2, CurrentSpot.x - 1].PieceColor != PieceColor)
                    {
                        possibleMoves.Add((CurrentSpot.y - 2, CurrentSpot.x - 1, false));
                    }
                    else if (Game1.Grid[CurrentSpot.y - 2, CurrentSpot.x - 1].PieceColor != PieceColor.None)
                    {
                        PiecesProtectedByMe.Add((CurrentSpot.y - 2, CurrentSpot.x - 1));
                    }
                }

                if (CurrentSpot.x - 1 >= 0 && CurrentSpot.y + 2 < Game1.Grid.GetLength(1))
                {
                    if (Game1.Grid[CurrentSpot.y + 2, CurrentSpot.x - 1].PieceColor != PieceColor)
                    {
                        possibleMoves.Add((CurrentSpot.y + 2, CurrentSpot.x - 1, false));
                    }
                    else if (Game1.Grid[CurrentSpot.y + 2, CurrentSpot.x - 1].PieceColor != PieceColor.None)
                    {
                        PiecesProtectedByMe.Add((CurrentSpot.y + 2, CurrentSpot.x - 1));
                    }
                }

                if (CurrentSpot.x + 1 < Game1.Grid.GetLength(0) && CurrentSpot.y + 2 < Game1.Grid.GetLength(0))
                {
                    if (Game1.Grid[CurrentSpot.y + 2, CurrentSpot.x + 1].PieceColor != PieceColor)
                    {
                        possibleMoves.Add((CurrentSpot.y + 2, CurrentSpot.x + 1, false));
                    }
                    else if (Game1.Grid[CurrentSpot.y + 2, CurrentSpot.x + 1].PieceColor != PieceColor.None)
                    {
                        PiecesProtectedByMe.Add((CurrentSpot.y + 2, CurrentSpot.x + 1));
                    }
                }

                if (CurrentSpot.x + 1 < Game1.Grid.GetLength(0) && CurrentSpot.y - 2 >= 0)
                {
                    if (Game1.Grid[CurrentSpot.y - 2, CurrentSpot.x + 1].PieceColor != PieceColor)
                    {
                        possibleMoves.Add((CurrentSpot.y - 2, CurrentSpot.x + 1, false));
                    }
                    else if (Game1.Grid[CurrentSpot.y - 2, CurrentSpot.x + 1].PieceColor != PieceColor.None)
                    {
                        PiecesProtectedByMe.Add((CurrentSpot.y - 2, CurrentSpot.x + 1));
                    }
                }

                return possibleMoves;
            }
        }

        public override PieceType Type { get; }

        public Knight(Texture2D texture, Vector2 position, Color color, Vector2 scale, PieceColor pieceColor)
            : base(texture, position, color, scale, pieceColor)
        {
            Type = PieceType.Knight;
        }
    }
}
