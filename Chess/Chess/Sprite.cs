using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Sprite
    {
        public Texture2D Texture { get; set; }

        public virtual Vector2 Position { get; set; }

        public Color Color { get; set; }

        public Vector2 Scale { get; set; }

        public int ScaledWidth => (int)(Texture.Width * Scale.X);

        public int ScaledHeight => (int)(Texture.Height * Scale.Y);
        public Vector2 Origin
        {
            get
            {
                return new Vector2(Texture.Width / 2, Texture.Height / 2);
            }
        }

        public Rectangle HitBox
        {
            get
            {
                return new Rectangle((int)(Position.X - ScaledWidth / 2), (int)(Position.Y - ScaledHeight / 2), ScaledWidth, ScaledHeight);
            }
        }
        public object Tag { get; set; }

        public SpriteEffects SpriteEffect { get; set; }
        public Sprite(Texture2D texture, Vector2 position, Color color, Vector2 scale)
        {
            Texture = texture;
            Position = position;
            Color = color;
            Scale = scale;

            SpriteEffect = SpriteEffects.None;
        }

        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color, 0f, Origin, Scale, SpriteEffect, 0f);
        }
    }
}
