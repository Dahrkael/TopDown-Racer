using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using CoRe.Graphics;
using CoRe.Utils;

namespace RacingGame
{
    class Map
    {
        public int Width;
        public int Height;
        Sprite[] Images = new Sprite[6];
        int[] iTiles = new int[]
        {
            6, 6, 6, 6, 6, 6, 6, 6, 6,
            6, 2, 0, 0, 0, 0, 0, 3, 6,
            6, 1, 6, 6, 6, 6, 6, 1, 6,
            6, 1, 6, 6, 6, 6, 6, 1, 6,
            6, 1, 6, 6, 6, 6, 6, 1, 6,
            6, 1, 6, 6, 6, 6, 6, 1, 6,
            6, 5, 0, 0, 0, 0, 0, 4, 6,
            6, 6, 6, 6, 6, 6, 6, 6, 6
        };

        Sprite[] Tiles = new Sprite[72];

        public Map()
        {
            for (int i = 0; i < 72; i++)
            {
                Tiles[i] = new Sprite();
                Tiles[i].Position = new Vector2(i % 9 * 266, i / 9 * 266);
                if (iTiles[i] == 6) { Tiles[i].Texture = Cache.Tile("dirt"); }
                else { Tiles[i].Texture = Cache.Tile("road-dirt-" + iTiles[i].ToString()); }
            }
            Width = iTiles.Length / 8 * 266;
            Height = iTiles.Length / 9 * 266;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(Camera2D Camera)
        {
            foreach (Sprite Tile in Tiles)
            {
                if (Tile == null) { continue; }
                Tile.Draw(Camera);
            }
        }

        public Color GetPixelColor(Vector2 Pixel)
        {
            foreach (Sprite Tile in Tiles)
            {
                if (Pixel.X < Tile.X) { continue; }
                if (Pixel.X > (Tile.X + 266)) { continue; }
                if (Pixel.Y < Tile.Y) { continue; }
                if (Pixel.Y > (Tile.Y + 266)) { continue; }
                return Tile.GetPixel(Vector2.Subtract(Pixel, Tile.Position));
            }
            return Color.Transparent;
        }
    }
}
