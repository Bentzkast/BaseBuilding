using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basebuilding
{
	public class World
	{
		Tile[,] tiles;
		readonly int width;
		readonly int height;

		public int Width
		{
			get{ return width;}
		}      
		public int Height
		{
			get{return height;}
		}

		public World(int width = 100, int height = 100)
		{
			this.width = width;
			this.height = height;
			tiles = new Tile[width, height];
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					tiles[x, y] = new Tile(this, x, y);
				}
			}
			Debug.Log("World created of size " + width + " x " + height);
		}

		public Tile GetTileAtPos(int x, int y)
		{
			if(x < 0 || x > width || y < 0 || y > height)
			{
				Debug.LogWarning("Invalid tile coordinate (" + x + "," + y + ")");
				return null;
			}
			return tiles[x, y];
		}     

        public void RandomizeTile()
		{
			Debug.Log("RandomizeTile");
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					tiles[x, y].Type = (Tile.TileType)Random.Range(0, 2);
				}
			}
		}
    }
}


