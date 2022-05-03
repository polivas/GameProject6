using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameProject6
{
    class Tilemap
    {
        /// <summary>
        /// The dim of the ties and the map
        /// </summary>
        int _tileWidth, _tileHeight, _mapWidth, _mapHeight;

        /// <summary>
        /// Tileset texture
        /// </summary>
        Texture2D _tilesetTexture;

        /// <summary>
        /// The tile info in the tileset
        /// </summary>
        Rectangle[] _tiles;

        /// <summary>
        /// The tile map data
        /// </summary>
        int[] _map;

        /// <summary>
        /// The filename 
        /// </summary>
        string _filename;

        public Tilemap(string filename)
        {
            _filename = filename;
        }

        public void LoadContent(ContentManager content)
        {
            string data = File.ReadAllText(Path.Join(content.RootDirectory, _filename));
            var lines = data.Split('\n');

            //First line is the tilesetfilename
            var tilesetFilename = lines[0].Trim();
            _tilesetTexture = content.Load<Texture2D>(tilesetFilename);

            //Seconf line in the tile size //16 for game 3
            var secondLine = lines[1].Split(',');
            _tileWidth = int.Parse(secondLine[0]);
            _tileHeight = int.Parse(secondLine[1]);


            int index = 0;

            int tilesetColumns = _tilesetTexture.Height / _tileHeight; 
            int tilesetRows = _tilesetTexture.Width / _tileWidth;
            _tiles = new Rectangle[(tilesetColumns * tilesetRows) + 1];

            for (int y = 0; y < tilesetColumns; y++)
            {
                for (int x = 0; x < tilesetRows; x++)
                {

                    _tiles[index] = new Rectangle(
                        x * _tileWidth,
                        y * _tileHeight,
                        _tileWidth,
                        _tileHeight
                        );
                    index++;
                }

            }

            var thirdLine = lines[2].Split(',');
            _mapWidth = int.Parse(thirdLine[0]);
            _mapHeight = int.Parse(thirdLine[1]);

            var fourthLine = lines[3].Split(',');
            _map = new int[_mapWidth * _mapHeight];

            for (int i = 0; i < _mapWidth * _mapHeight; i++)
            {
                _map[i] = int.Parse(fourthLine[i]);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {


            for (int y = 0; y < _mapHeight; y++)
            {
                for (int x = 0; x < _mapWidth; x++)
                {
                    int index = _map[y * _mapWidth + x];

                    if (index == -1) continue;

                    spriteBatch.Draw(
                        _tilesetTexture,
                        new Vector2(
                            x * _tileWidth,
                            y * _tileHeight
                            ),
                        _tiles[index],
                        Color.White);


                }
            }
        }

    }
}
