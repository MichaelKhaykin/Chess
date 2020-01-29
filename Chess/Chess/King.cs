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
        public override List<(int y, int x, bool isEmpassant)> PossibleMoves
        { 
            get
            {
                List<(int y, int x, bool isEmpassant)> possibleMoves = new List<(int y, int x, bool isEmpassant)>();



                return possibleMoves;
            }
        }

        public override PieceType Type { get; }

        public King(Texture2D texture, Vector2 position, Color color, Vector2 scale, PieceColor pieceColor) : base(texture, position, color, scale, pieceColor)
        {
            Type = PieceType.King;
        }
    }
}
