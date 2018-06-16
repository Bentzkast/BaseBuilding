using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basebuilding
{
	public class WorldController : MonoBehaviour
	{

		public Sprite GrassSprite;
		public Sprite SeaSprite;

		// The only World controller instance
		public static WorldController Instance { get; protected set; }

		GameObject worldHolder;
		public World World{ get; protected set; }

		private void Awake()
		{
			if(Instance != null)
			{
				Debug.LogError("The should be only one world controller");
                Destroy(this);
				return;

			}
			Instance = this;
		}


		void Start()
        {
			// Data
			World = new World();

            // create the GameObjects for visual
			worldHolder = new GameObject();
			worldHolder.name = "World_Holder";
			for (int x = 0; x < World.Width; x++)
			{
				for (int y = 0; y < World.Height; y++)
				{
					Tile tileData = World.GetTileAtPos(x, y);
					GameObject tileGO = new GameObject();
					tileGO.transform.SetParent(worldHolder.transform,true);
					tileGO.name = "Tile (" + x + "," + y + ")";
					tileGO.transform.position = new Vector3(x, y, 0);
					// TODO : set sorting layer?
					tileGO.AddComponent<SpriteRenderer>();

                    // initial sprite setup
					OnTileTypeChange(tileData, tileGO);

                    // register callback
					tileData.RegisterTileTypeChangeCB((Tile tile) => { OnTileTypeChange(tile, tileGO); });
				}
			}
			World.RandomizeTile();
		}
              
        // set the tile sprite based on its type
		void OnTileTypeChange(Tile tileData,GameObject tileGO)
        {
			SpriteRenderer tileSR = tileGO.GetComponent<SpriteRenderer>();
            
			switch(tileData.Type)
			{
				case Tile.TileType.Grassland:
					tileSR.sprite = GrassSprite;
					break;
				case Tile.TileType.Sea:
					tileSR.sprite = SeaSprite;
                    break;
				default:
					Debug.LogError("OnTileTypeChange - Invalid tile Types");
					break;
			}

        }
    }

}
