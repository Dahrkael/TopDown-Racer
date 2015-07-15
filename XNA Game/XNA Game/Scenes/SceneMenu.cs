using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CoRe;
using CoRe.Input;
using CoRe.GUI;
using CoRe.GUI.WindowCommand.Command;
using CoRe.Graphics;
using CoRe.Audio;
using CoRe.Utils;

namespace RacingGame.Scenes
{
    class SceneMenu : CoRe.Scenes.Scene
    {
        Sprite Background;
        Sprite Title;
        Sprite Car;
        int Step = 0;
        CoRe.GUI.WindowCommand.TextOnly MainMenu;
        CoRe.GUI.WindowCommand.TextOnly OptionMenu;
        HelpText Help;
        Music Music;
        Sample Speedway;

        public SceneMenu() : base()
        {
            Initialize();
        }

        ~SceneMenu()
        {
        }

        protected override void Initialize()
        {
            Background = new Sprite(new Vector2(0, 0));
            Background.Texture = Cache.Picture("bgtitle");

            Title = new Sprite(new Vector2(-900, 50));
            Title.Texture = Cache.Picture("title");

            Car = new Sprite(new Vector2(-300, 100));
            Car.Texture = Cache.Texture("carBlue");

            Music = new Music("mainmenu");
            Music.Play(true);
            Music.Volume = 0.0f;

            Speedway = new Sample("speedway");
            Speedway.Pitch = 0.2f;

            Help = new HelpText(new Vector2(400, 550));
            Help.Alignment = TextAlignment.Center;
            Help.Color = Color.Red;
            Help.Outline = 1;
            Help.AddText("Start driving ASAP");
            Help.AddText("Be the number one!");
            Help.AddText("Change the game options");
            Help.AddText("Exit the game");
            Help.AddText("Sets the music volume level");
            Help.AddText("Sets the sounds volume level");
            Help.AddText("Switch between fullscreen and windowed");
            Help.AddText("Return to the main menu");
            Help.Index = 0;
            Help.Visible = false;

            MainMenu = new CoRe.GUI.WindowCommand.TextOnly(new Vector2(270, 350));
            MainMenu.AddCommand("Quick Race", ClickQuickRace);
            MainMenu.AddCommand("Championship", ClickChampionship);
            MainMenu.AddCommand("Options", ClickOptions);
            MainMenu.AddCommand("Exit", (Command c) => { XNAGame.I.Finish(); });
            MainMenu.Font = "Ethnocentric";
            MainMenu.Spacing = 40;
            MainMenu.Outline = 2;
            MainMenu.MoveSound = "broumbroum__sf3-sfx-menu-select";
            MainMenu.SelectSound = "runnerpack__menusel";

            OptionMenu = new CoRe.GUI.WindowCommand.TextOnly(new Vector2(270, 350));
            OptionMenu.AddCommand("Music Volume: "+((int)Music.DefaultVolume*100).ToString()+"%", ClickMusicVolume);
            OptionMenu.AddCommand("Sounds Volume: "+((int)Sample.DefaultVolume * 100).ToString() + "%", ClickSoundVolume);
            if (XNAGame.Instance.Graphics.IsFullScreen) { OptionMenu.AddCommand("Fullscreen: ON", ClickFullscreen); }
            else { OptionMenu.AddCommand("Fullscreen: OFF", ClickFullscreen); }
            OptionMenu.AddCommand("Back", ClickBack);
            OptionMenu.Font = "Ethnocentric";
            OptionMenu.Spacing = 40;
            OptionMenu.Outline = 2;
            OptionMenu.MoveSound = "broumbroum__sf3-sfx-menu-select";
            OptionMenu.SelectSound = "runnerpack__menusel";

            MainMenu.Off();
            OptionMenu.Off();

            MainMenu.Position = new Vector2((XNAGame.I.Width - MainMenu.Width) / 2, 350);
            OptionMenu.Position = new Vector2((XNAGame.I.Width - OptionMenu.Width) / 2, 350);
            Speedway.Play();
        }

        public override void Update(GameTime gameTime)
        {
            InputManager.I.Update();
            if (Step == 0)
            {
                if (Title.X < -60) { Title.X += 14; }
                if (Car.X < 801) { Car.X += 14; } 
                else  
                {
                    Step = 1;
                    Music.Volume = Music.DefaultVolume;
                    Car.Position = new Vector2(900, -50);
                    Car.Rotate(135);
                    MainMenu.On();
                    Help.Visible = true;
                    Help.Index = 0;
                }
            }
            else
            {
                if (Car.Position.Y < 150)
                { Car.X -= 10; Car.Y += 10; }
                if (MainMenu.Active)
                {
                    MainMenu.Update(gameTime);
                    Help.Index = MainMenu.Index;  
                }
                else
                {
                    OptionMenu.Update(gameTime);
                    Help.Index = OptionMenu.Index + 4;
                }
            }
        }

        public override void Draw()
        {
            Background.Draw();
            Car.Draw();
            Title.Draw();
            MainMenu.Draw();
            OptionMenu.Draw();
            Help.Draw();
        }

        private void ClickQuickRace(CoRe.GUI.WindowCommand.Command.Command c)
        {
            XNAGame.I.FadeIn(50);
            Music.Stop();
            XNAGame.Instance.Scene = new Scenes.SceneTest();
        }

        private void ClickChampionship(Command c)
        { }

        private void ClickOptions(Command c)
        { MainMenu.Off(); OptionMenu.On(); OptionMenu.ResetIndex(); }

        private void ClickMusicVolume(Command c)
        {
            double temp = (Music.DefaultVolume + 0.1) % 1.1;
            Music.DefaultVolume = (float)Math.Max(0, Math.Min(temp, 1));
            Music.Volume = Music.DefaultVolume;
            c.Content = "Music Volume: " + ((int)(Music.DefaultVolume*100)).ToString() + "%";
        }

        private void ClickSoundVolume(Command c)
        {
            double temp = (Sample.DefaultVolume + 0.1) % 1.1;
            Sample.DefaultVolume = (float)Math.Max(0, Math.Min(temp, 1));
            MainMenu.Volume = Sample.DefaultVolume;
            OptionMenu.Volume = Sample.DefaultVolume;
            c.Content = "Music Volume: " + ((int)(Sample.DefaultVolume * 100)).ToString() + "%";
        }

        private void ClickFullscreen(Command c)
        {
            if (XNAGame.I.Graphics.IsFullScreen)
            { XNAGame.I.Graphics.IsFullScreen = false; c.Content = "Fullscreen: OFF"; }
            else
            { XNAGame.I.Graphics.IsFullScreen = true; c.Content = "Fullscreen: ON"; }
            XNAGame.I.Graphics.ApplyChanges();
        }

        private void ClickBack(Command c)
        { OptionMenu.Off(); MainMenu.On(); }
    }
}
