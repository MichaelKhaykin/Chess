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
        public abstract List<(int y, int x)> PossibleMoves { get; }
        public abstract PieceType Type { get; }

        public abstract (int y, int x) CurrentSpot { get; }
    
        private bool isEmulating = false;

        public abstract bool IsWhite { get; set; }
        public override Vector2 Position
        {
            get
            {
                return InputManager.ConvertGridIndexToPosition(CurrentSpot.x, CurrentSpot.y);
            }
        }
        public bool HasMoved { get; set; } = false;

        public Piece(Texture2D texture, Vector2 position, Color color, Vector2 scale) : base(texture, position, color, scale)
        {
        }


        public override void Update(GameTime gameTime)
        {
            if (InputManager.Mouse.LeftButton == ButtonState.Pressed && InputManager.OldMouse.LeftButton == ButtonState.Released)
            {
                if (HitBox.Contains(InputManager.Mouse.Position))
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
                else if(isEmulating)
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

                    var (y, x) = (ogPossibleMoves[index].y, ogPossibleMoves[index].x);

                    Game1.Grid[CurrentSpot.y, CurrentSpot.x] = new Empty();

                    Game1.Grid[y, x] = this;

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
