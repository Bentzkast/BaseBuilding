using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basebuilding
{
	public class Tile
    {

        enum Type { Empty, Grassland };
        Type tileType = Type.Empty;
        
        int x;
		int y;

		World world;
        
        public Tile(World world, int x, int y)
		{
			this.world = world;
			this.x = x;
			this.y = y;
		}
    }
}


