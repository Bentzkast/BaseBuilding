using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basebuilding
{
	public class World
	{
		Tile[,] tiles;
		int width;
		int height;

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

		public Tile GetTile(int x, int y)
		{
			if(x < 0 || x > width || y < 0 || y > height)
			{
				Debug.LogError("Invalid tile coordinate (" + x + "," + y + ")");
				return null;
			}
			return tiles[x, y];
		}
    }
}


