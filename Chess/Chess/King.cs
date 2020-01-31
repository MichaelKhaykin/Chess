using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class King : Piece
    {
        public override List<(int y, int x, object data)> PossibleMoves
        { 
            get
            {
                List<(int y, int x, object data)> possibleMoves = new List<(int y, int x, object data)>();
                List<(int y, int x, object data)> invalidMoves = new List<(int y, int x, object data)>();

                List<Piece> pieces = new List<Piece>();
                for (int i = 0; i < Game1.Grid.GetLength(0); i++)
                {
                    for (int j = 0; j < Game1.Grid.GetLength(1); j++)
                    {
                        if (Game1.Grid[i, j].PieceColor == (PieceColor == PieceColor.White ? PieceColor.Black : PieceColor.White))
                        {
                            pieces.Add(Game1.Grid[i, j]);
                        }
                    }
                }

                int factor = (PieceColor == PieceColor.White ? 1 : -1);

                List<(int y, int x)> protectedPieces = new List<(int y, int x)>();

                for(int i = 0; i < pieces.Count; i++)
                {
                    var piece = pieces[i];

                    if (piece.Type == PieceType.King) continue;

                    invalidMoves.AddRange(piece.PossibleMoves);
                    protectedPieces.AddRange(piece.PiecesProtectedByMe);

                    if (piece.Type == PieceType.Pawn)
                    {
                        if (piece.CurrentSpot.x - 1 >= 0)
                        {
                            invalidMoves.Add((piece.CurrentSpot.y - factor, piece.CurrentSpot.x - 1, false));
                        }
                        if (piece.CurrentSpot.x + 1 < Game1.Grid.GetLength(0))
                        {
                            invalidMoves.Add((piece.CurrentSpot.y - factor, piece.CurrentSpot.x + 1, false));
                        }
                    }
                }

                AddToList(possibleMoves, invalidMoves, CurrentSpot.y + 1, CurrentSpot.x - 1, protectedPieces);
                AddToList(possibleMoves, invalidMoves, CurrentSpot.y - 1, CurrentSpot.x - 1, protectedPieces);
                AddToList(possibleMoves, invalidMoves, CurrentSpot.y - 1, CurrentSpot.x + 1, protectedPieces);
                AddToList(possibleMoves, invalidMoves, CurrentSpot.y + 1, CurrentSpot.x + 1, protectedPieces);
                AddToList(possibleMoves, invalidMoves, CurrentSpot.y, CurrentSpot.x + 1, protectedPieces);
                AddToList(possibleMoves, invalidMoves, CurrentSpot.y, CurrentSpot.x - 1, protectedPieces);
                AddToList(possibleMoves, invalidMoves, CurrentSpot.y + 1, CurrentSpot.x, protectedPieces);
                AddToList(possibleMoves, invalidMoves, CurrentSpot.y - 1, CurrentSpot.x, protectedPieces);

                //handle castling
                if(HasMoved == false)
                {
                    int yIndex = (PieceColor == PieceColor.White ? 0 : 7);

                    var rook1 = Game1.Grid[yIndex, 0];
                    var rook2 = Game1.Grid[yIndex, 7];

                    if(rook1.HasMoved == false && rook1 is Rook)
                    {
                        if(Game1.Grid[yIndex, 1] is Empty && 
                            Game1.Grid[yIndex, 2] is Empty &&
                            Game1.Grid[yIndex, 3] is Empty)
                        {
                            possibleMoves.Add((yIndex, 2, (true, false)));
                        }
                    }
                    if(rook2.HasMoved == false && rook2 is Rook)
                    {
                        if(Game1.Grid[yIndex, 6] is Empty &&
                           Game1.Grid[yIndex, 5] is Empty)
                        {
                            possibleMoves.Add((yIndex, 6, (true, true)));
                        }
                    }
                }

                return possibleMoves;
            }
        }
        public override PieceType Type { get; }

        public bool IsInCheck { get; set; }
        public King(Texture2D texture, Vector2 position, Color color, Vector2 scale, PieceColor pieceColor) : base(texture, position, color, scale, pieceColor)
        {
            Type = PieceType.King;
        }

        private bool CheckIfPieceIsHere(int x, int y)
        {
            if(Game1.Grid[x, y].PieceColor == PieceColor)
            {
                PiecesProtectedByMe.Add((y, x));
                return true;
            }

            //add code for checking if a piece is protected

            return false;
        }

        private void AddToList(List<(int y, int x, object data)> possibleMoves,
            List<(int y, int x, object data)> invalidMoves, int y, int x, List<(int y, int x)> protectedPieces)
        {
            if (x < 0 || x >= Game1.Grid.GetLength(0)
                || y < 0 || y >= Game1.Grid.GetLength(1)) return;

            if (!invalidMoves.Contains((y, x, false))
                    && CheckIfPieceIsHere(y, x) == false
                    && !protectedPieces.Contains((y, x)))
            {

                possibleMoves.Add((y, x, false));
            }
        }
    }
}
