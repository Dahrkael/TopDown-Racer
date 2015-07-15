using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CoRe;
using CoRe.Scenes;
using CoRe.Utils;
using CoRe.Audio;
using CoRe.Graphics;
using CoRe.Input;

namespace RacingGame
{
    public class GameMain : CoRe.XNAGame
    {
        public static void CreateInstance()
        { instance = new GameMain(); }

        private GameMain() : base(800, 600, false)
        {
            // Components.Add(new GamerServicesComponent(this)); 
        }

        protected override void Initialize()
        {
            scene = new CoRe.Scenes.SceneSplash("XNAPowered", () => { XNAGame.I.Scene = new Scenes.SceneMenu(); });
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            scene.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            scene.Draw();
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
