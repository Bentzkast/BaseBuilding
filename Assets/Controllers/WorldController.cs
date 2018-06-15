using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basebuilding
{
	public class WorldController : MonoBehaviour
    {
		public Sprite GrassSprite;
		public Sprite SeaSprite;

		GameObject worldHolder;
		World world;
               
        void Start()
        {
			// Data
			world = new World();
            
            // create the GameObjects for visual
			worldHolder = new GameObject();
            worldHolder.name = "World_Holder";
			for (int x = 0; x < world.Width; x++)
			{
				for (int y = 0; y < world.Height; y++)
				{
					Tile tileData = world.GetTile(x, y);
					GameObject tileGO = new GameObject();
					tileGO.transform.parent = worldHolder.transform;
					tileGO.name = "Tile (" + x + "," + y + ")";
					tileGO.transform.position = new Vector3(x, y, 0);
					tileGO.AddComponent<SpriteRenderer>();
					tileData.RegisterTileTypeChangeCB((Tile tile) => { OnTileTypeChange(tile, tileGO); });
				}
			}

		}

		float timer = 2f;
		// Update is called once per frame
        void Update()
        {
			timer -= Time.deltaTime;
			if (timer > 0)
			{
				return;
			}
			timer += 2f;

			world.RandomizeTile();
        }

         

		void OnTileTypeChange(Tile tileData,GameObject tileGO)
        {
			SpriteRenderer tileSR = tileGO.GetComponent<SpriteRenderer>();

			switch(tileData.TileType)
			{
				case Tile.Type.Grassland:
					tileSR.sprite = GrassSprite;
					break;
				case Tile.Type.Sea:
					tileSR.sprite = SeaSprite;
                    break;
				case Tile.Type.Empty:
					tileSR.sprite = null;
                    break;
				default:
					Debug.LogError("OnTileTypeChange - Invalid tile Types");
					break;
			}

        }
    }

}
