using System;

namespace Basebuilding
{
	public class Tile
	{

		public enum Type { Empty, Grassland, Sea };
		Type tileType = Type.Empty;

		int x;
		int y;

		World world;
		InstalledObject installedObject;
		LooseObject looseObject;

		Action<Tile> TileTypeChangeCB;
		Type oldTileType;

		public int X
		{
			get
			{
				return x;
			}

			set
			{
				x = value;
			}
		}

		public int Y
		{
			get
			{
				return y;
			}

			set
			{
				y = value;
			}
		}

		public Type TileType
		{
			get
			{
				return tileType;
			}

			set
			{
				oldTileType = tileType;
				tileType = value;
				if (TileTypeChangeCB != null && tileType != oldTileType)
				{
					TileTypeChangeCB(this);
				}
			}
		}

		public Tile(World world, int x, int y)
		{
			this.world = world;
			this.X = x;
			this.Y = y;
		}

		public void RegisterTileTypeChangeCB(Action<Tile> action)
		{
			TileTypeChangeCB += action;
		}

	}
}


