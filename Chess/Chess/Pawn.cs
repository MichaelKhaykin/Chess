using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Pawn : Piece
    {
        public override List<(int y, int x)> PossibleMoves
        {
            get
            {
                List<(int y, int x)> possibleMoves = new List<(int y, int x)>();

                if(!IsWhite)
                {
                    var start = CurrentSpot.y - 1;
                    var end = CurrentSpot.y - (HasMoved ? 1 : 2);

                    for(int i = start; i >= end; i--)
                    {
                        if (Game1.Grid[i, CurrentSpot.x].Type != PieceType.None)
                        {
                            break;
                        }
                        possibleMoves.Add((i, CurrentSpot.x));
                    }
                }
                else
                {
                    var start = CurrentSpot.y + 1;
                    var end = CurrentSpot.y + (HasMoved ? 1 : 2);

                    for (int i = start; i <= end; i++)
                    {
                        if (Game1.Grid[i, CurrentSpot.x].Type != PieceType.None)
                        {
                            break;
                        }
                        possibleMoves.Add((i, CurrentSpot.x));
                    }
                }
               

                return possibleMoves;
            }
        }
        public override PieceType Type { get; }
        public override (int y, int x) CurrentSpot
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
        public override bool IsWhite { get; set; }

        public Pawn(Texture2D texture, Vector2 position, Color color, Vector2 scale)
            : base(texture, position, color, scale)
        {
            Type = PieceType.Pawn;
        }
    }
}
