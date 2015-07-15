using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CoRe.Graphics;
using CoRe;
using CoRe.GUI;
using CoRe.Audio;
using CoRe.Input;

namespace RacingGame.Scenes
{
    public class SceneTest : CoRe.Scenes.Scene
    {
        Camera2D Camera;
        Text info;
        Wall Wall;
        Car Car;
        Map Map;
        Music Music;

        public SceneTest() : base()
        {
            Initialize();
        }

        ~SceneTest()
        {
        }

        protected override void Initialize()
        {
            
            Map = new Map();
            Camera = new Camera2D(Vector2.Zero, Map.Width - 800, Map.Height - 600);
            Car = new Car(new Vector2(XNAGame.I.Width / 2 + 50, XNAGame.I.Height / 2 + 50));
            Wall = new Wall(new Vector2(XNAGame.I.Width / 2 + 50, XNAGame.I.Height / 2 + 500), 100, 16);
            info = new Text("", "Speed: 0 - Angle: 0", new Vector2(0, 20));
            Music = new Music("track");
            Music.Play(true);
        }

        public override void Update(GameTime gameTime)
        {
            CoRe.Physics.PhysicsEngine.Update(gameTime);
            InputManager.I.Update();
            if (InputManager.I.isKeyTriggered(Keys.Escape))
            {
                XNAGame.Instance.FadeIn(50);
                Car.StopSounds();
                Music.Stop();
                XNAGame.Instance.Scene = new Scenes.SceneMenu();
            }

            Camera.Update(new Vector2(Car.Position.X - 400, Car.Position.Y - 300));
            Car.Update(gameTime);
            Map.Update(gameTime);

            Color color = Map.GetPixelColor(Car.Position);
            if ((color != new Color(0x4D, 0x4D, 0x4D, 255)) && (color != new Color(0xE6, 0xE60, 0xE6, 255)))
            { Car.OutRoad(); }
            else { Car.OnRoad(); }
        }

        public override void Draw()
        {
            Map.Draw(Camera);
            Car.Draw(Camera);
            Wall.Draw(Camera);
            info.Content = "Speed: " + Car.Acceleration.ToString() + " - Position: " + (Car.Position).ToString();
            info.Draw();
        }
    }
}