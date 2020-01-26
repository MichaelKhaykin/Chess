﻿using Microsoft.Xna.Framework;
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
        public override List<(int y, int x)> PossibleMoves
        {
            get
            {
                List<(int y, int x)> possibleMoves = new List<(int y, int x)>();

                if (PieceColor == PieceColor.Black)
                {
                    var start = CurrentSpot.y - 1;
                    var end = CurrentSpot.y - (HasMoved ? 1 : 2);

                    for (int i = start; i >= end; i--)
                    {
                        if (Game1.Grid[i, CurrentSpot.x].Type != PieceType.None)
                        {
                            break;
                        }
                        possibleMoves.Add((i, CurrentSpot.x));
                    }

                    var diagonalLeft = (CurrentSpot.y - 1, CurrentSpot.x - 1);
                    if (diagonalLeft.Item2 > 0)
                    {
                        if (Game1.Grid[diagonalLeft.Item1, diagonalLeft.Item2].Type != PieceType.None)
                        {
                            possibleMoves.Add(diagonalLeft);
                        }
                    }

                    var diagonalRight = (CurrentSpot.y - 1, CurrentSpot.x + 1);
                    if (diagonalRight.Item2 < Game1.Grid.GetLength(0))
                    {
                        if (Game1.Grid[diagonalRight.Item1, diagonalRight.Item2].Type != PieceType.None)
                        {
                            possibleMoves.Add(diagonalRight);
                        }
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

                    var diagonalLeft = (CurrentSpot.y + 1, CurrentSpot.x - 1);
                    if (diagonalLeft.Item2 > 0)
                    {
                        if (Game1.Grid[diagonalLeft.Item1, diagonalLeft.Item2].Type != PieceType.None)
                        {
                            possibleMoves.Add(diagonalLeft);
                        }
                    }

                    var diagonalRight = (CurrentSpot.y + 1, CurrentSpot.x + 1);
                    if (diagonalRight.Item2 < Game1.Grid.GetLength(0))
                    {
                        if (Game1.Grid[diagonalRight.Item1, diagonalRight.Item2].Type != PieceType.None)
                        {
                            possibleMoves.Add(diagonalRight);
                        }
                    }
                }
               
                return possibleMoves;
            }
        }
        public override PieceType Type { get; }

        public bool StartScanning = false;
        public Pawn(Texture2D texture, Vector2 position, Color color, Vector2 scale)
            : base(texture, position, color, scale)
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

                }
                else if(InputManager.Keyboard.IsKeyDown(Keys.R))
                {

                }
                else if(InputManager.Keyboard.IsKeyDown(Keys.B))
                {

                }
                else if(InputManager.Keyboard.IsKeyDown(Keys.K))
                {

                }
            }

            base.Update(gameTime);
        }
    }
}
