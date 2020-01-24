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
        
        public override List<(int y, int x)> PossibleMoves => throw new NotImplementedException();

        public override PieceType Type { get; }

        public override (int y, int x) CurrentSpot { get => throw new NotImplementedException(); }
        public override bool IsWhite { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Empty(Texture2D texture, Vector2 position, Color color, Vector2 scale) : base(texture, position, color, scale)
        {
            Type = PieceType.None;
        }

        public Empty()
            : this(null, Vector2.Zero, Color.White, Vector2.Zero)
        {

        }
    }
}
