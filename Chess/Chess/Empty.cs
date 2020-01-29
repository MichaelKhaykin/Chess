using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Empty : Piece
    {
        
        public override List<(int y, int x, bool isEmpassant)> PossibleMoves => throw new NotImplementedException();

        public override PieceType Type { get; }
        public Empty() 
            : base(null, Vector2.Zero, Color.White, Vector2.Zero, PieceColor.None)
        {
            Type = PieceType.None;
        }
    }
}
