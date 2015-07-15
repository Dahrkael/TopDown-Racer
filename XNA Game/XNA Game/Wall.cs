using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CoRe;
using CoRe.Audio;
using CoRe.Physics;
using CoRe.Graphics;
using CoRe.Input;
using CoRe.Utils;

namespace RacingGame
{
    class Wall
    {
        protected Sprite image;
        protected Rect rect;

        public Wall(Vector2 Position) : this(Position, 16, 16)
        { }
        public Wall(Vector2 Position, int Width, int Height)
        {
            rect = new Rect(Width, Height, false);
            rect.Position = Position;
            rect.IgnoreGravity = true;
            image = new Sprite(Position);
            image.Texture = new Texture2D(XNAGame.I.GraphicsDevice, Width, Height);
            image.Fill(Color.LimeGreen);
        }

        public void Draw(Camera2D Camera)
        {
            image.Position = rect.Position;
            image.Draw(Camera);
        }

        public Vector2 Position { get { return rect.Position; } set { rect.Position = value; } }
    }
}
