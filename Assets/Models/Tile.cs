using System;

namespace Basebuilding
{
	public class Tile
	{
        // terrain type
		public enum TileType { Sea, Grassland };
		TileType _tileType = TileType.Sea;
  
       
		World world;
        // building, mountain, wall etc
		InstalledObject installedObject;
        // units, resouces, weapon
		LooseObject looseObject;

        // callbacks
		Action<Tile> TileTypeChangeCB;
		TileType _oldTileType;

		public int X { get; protected set; }
		public int Y { get; protected set; }

		public TileType Type
		{
			get
			{
				return _tileType;
			}

			set
			{
				_oldTileType = _tileType;
				_tileType = value;
				if (TileTypeChangeCB != null && _tileType != _oldTileType)
				{
					TileTypeChangeCB(this);
				}
			}
		}

        // constructor 
		public Tile(World world, int x, int y)
		{
			this.world = world;
			this.X = x;
			this.Y = y;
		}

        // callback
		public void RegisterTileTypeChangeCB(Action<Tile> action)
		{
			TileTypeChangeCB += action;
		}

	}
}


