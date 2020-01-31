using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Queen : Piece
    {
        public override List<(int y, int x, object data)> PossibleMoves
        { 
            get
            {
                var possibleMoves = new List<(int y, int x, object data)>();

                possibleMoves.AddRange(Helper(1, 0));
                possibleMoves.AddRange(Helper(-1, 0));
                possibleMoves.AddRange(Helper(0, 1));
                possibleMoves.AddRange(Helper(0, -1));
                possibleMoves.AddRange(Helper(1, 1));
                possibleMoves.AddRange(Helper(1, -1));
                possibleMoves.AddRange(Helper(-1, 1));
                possibleMoves.AddRange(Helper(-1, -1));

                return possibleMoves;
            }
        }

        public override PieceType Type { get; }
        public Queen(Texture2D texture, Vector2 position, Color color, Vector2 scale, PieceColor pieceColor) : base(texture, position, color, scale, pieceColor)
        {
            Type = PieceType.Queen;
        }

        private List<(int y, int x, object data)> Helper(int movingX, int movingY)
        {
            var moves = new List<(int y, int x, object data)>();

            bool hasBlackBeenFound = false;

            bool isInBottomBounds = CurrentSpot.y + movingY < Game1.Grid.GetLength(1);
            bool isInRightBounds = CurrentSpot.x + movingX < Game1.Grid.GetLength(0);
            bool isInLeftBounds = CurrentSpot.x + movingX >= 0;
            bool isInTopBounds = CurrentSpot.y + movingY >= 0;

            while (isInBottomBounds && isInRightBounds && isInLeftBounds && isInTopBounds &&
                Game1.Grid[CurrentSpot.y + movingY, CurrentSpot.x + movingX].PieceColor != PieceColor
                && hasBlackBeenFound == false)
            {
                hasBlackBeenFound = Game1.Grid[CurrentSpot.y + movingY, CurrentSpot.x + movingX].PieceColor == (this.PieceColor == PieceColor.Black ? PieceColor.White : PieceColor.Black);

                moves.Add((CurrentSpot.y + movingY, CurrentSpot.x + movingX, false));

                movingX += 1 * (movingX == 0 ? 0 : movingX > 0 ? 1 : -1);
                movingY += 1 * (movingY == 0 ? 0 : movingY > 0 ? 1 : -1);

                isInBottomBounds = CurrentSpot.y + movingY < Game1.Grid.GetLength(1);
                isInRightBounds = CurrentSpot.x + movingX < Game1.Grid.GetLength(0);
                isInLeftBounds = CurrentSpot.x + movingX >= 0;
                isInTopBounds = CurrentSpot.y + movingY >= 0;
            }

            if(!hasBlackBeenFound && isInBottomBounds && isInRightBounds && isInLeftBounds && isInTopBounds)
            {
                PiecesProtectedByMe.Add((CurrentSpot.y + movingY, CurrentSpot.x + movingX));
            }
            return moves;
        }

    }
}
