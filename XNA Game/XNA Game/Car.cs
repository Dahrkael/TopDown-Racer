using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CoRe;
using CoRe.Physics;
using CoRe.Audio;
using CoRe.Graphics;
using CoRe.Input;
using CoRe.Utils;

namespace RacingGame
{
    class Car
    {
        protected Sprite image;
        protected Rect rect;
        protected float acceleration = 0;
        protected float velFactor = 0.85f;
        protected float accFactor = 0.99f;
        protected Vector2 velocity = Vector2.Zero;
        protected LoopedSample driftSound;
        protected LoopedSample motorSound;

        public Car(Vector2 Position)
        {
            image = new Sprite(Position);
            Cache.Texture("carOrange");
            image.Texture = Cache.Texture("carBlue");
            image.CenterOrigin();
            image.Origin = new Vector2(image.Origin.X - 25, image.Origin.Y);
            image.Zoom = 0.4f;
            image.Rotate(270);

            rect = new Rect(image.Texture.Width * 0.4f, image.Texture.Height * 0.4f, true);
            rect.Position = Position;
            rect.Friction = 1f;
            //rect.F = () => { image.Texture = Cache.Texture("carOrange"); };

            driftSound = new LoopedSample("drift");
            driftSound.Volume = Sample.DefaultVolume / 3;

            motorSound = new LoopedSample("motor");
            motorSound.Play();
        }

        public void StopSounds()
        {
            driftSound.Stop();
            motorSound.Stop();
        }

        public void Update(GameTime gameTime)
        {
            if (InputManager.I.isKeyPressed(Keys.Up))
            {
                if (acceleration < 20)
                { acceleration += 0.2f; }
            }

            if (InputManager.I.isKeyPressed(Keys.Down))
            {
                if (acceleration > 0) { acceleration -= 0.3f; }
                else { if (acceleration > -4) { acceleration -= 0.1f; } }
            }

            float rotation = System.Math.Min(4 * acceleration / 20, 2);
            if (InputManager.I.isKeyPressed(Keys.Left))
            {
                //rect.Rotation += -Maths.ToRadians(rotation);
                image.Rotate(-rotation);
                rect.Rotate(-50000);
                if (acceleration > 15) { driftSound.Play(); }
            }
            if (InputManager.I.isKeyPressed(Keys.Right))
            {
                //rect.Rotation += Maths.ToRadians(rotation);
                rect.Rotate(50000);
                image.Rotate(rotation);
                if (acceleration > 15) { driftSound.Play(); }
            }

            if (!InputManager.I.isKeyPressed(Keys.Left) && !InputManager.I.isKeyPressed(Keys.Right))
            { driftSound.Stop(); }

            motorSound.Pitch = System.Math.Abs(1.0f * acceleration / 20);
            rect.Accelerate(new Vector2((float)Maths.OffsetX(image.AngleInRadians, PhysicsEngine.ToMeters(acceleration)),
                                        (float)Maths.OffsetY(image.AngleInRadians, PhysicsEngine.ToMeters(acceleration))));
            //rect.Speed *= accFactor;
            acceleration *= accFactor;
        }

        public void Draw(Camera2D Camera)
        {
            image.Position = rect.Position;
            image.Draw(Camera);
        }

        public void OutRoad()
        {
            velFactor = 0.98f;
            accFactor = 0.98f;
            driftSound.Stop();
        }

        public void OnRoad()
        {
            velFactor = 0.90f;
            accFactor = 0.99f;
        }

        public Vector2 Position { get { return rect.Position; } set { rect.Position = value; } }
        public Vector2 Acceleration { get { return rect.Speed; } set { } }
        public float Rotation { get { return image.AngleInDegrees; } }
    }
}
