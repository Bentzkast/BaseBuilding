using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;
using UnityEngine.EventSystems;


namespace Basebuilding
{
	public class MouseController : MonoBehaviour
    {

        
        public GameObject MouseCursorPrefab;

		float maxScroll = 10f;
		float minScroll = 3f;

        Vector3 oldMousePos;
        Vector3 curMousePos;

		Vector3 startDrag;

		List<GameObject> selectionHighlight = new List<GameObject>();
		Tile.TileType mode = Tile.TileType.Grassland;
        

        void Start()
        {
            oldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			curMousePos.z = 0;
        }

        // Update is called once per frame
        void Update()
        {
            // get the normalized 
            curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            curMousePos.z = 0;

			//UpdateCursor();
			UpdateDragSelection();
	
        }

        void LateUpdate()
		{
			UpdateCamera();


            oldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            oldMousePos.z = 0;
		}

		Tile GetTileAtWorldCoord(Vector3 pos)
		{
			int x = Mathf.FloorToInt(pos.x);
			int y = Mathf.FloorToInt(pos.y);

			return WorldController.Instance.World.GetTileAtPos(x, y);
		}

		//void UpdateCursor(){
  //          Tile tileUnderMouse = GetTileAtWorldCoord(curMousePos);
  //          if (tileUnderMouse != null)
  //          {
  //              MouseCursor.SetActive(true);
  //              MouseCursor.transform.position = new Vector3(tileUnderMouse.X , tileUnderMouse.Y,0);
  //          }
  //          else
  //              MouseCursor.SetActive(false);
		//}


        private void UpdateDragSelection()
		{
			// if we are over a UI element bail OUT
			if(EventSystem.current.IsPointerOverGameObject())
			{
				return;
			}

			// start drag
            if (Input.GetMouseButtonDown(0))
            {
                startDrag = curMousePos;
            }

            int startDragX = Mathf.FloorToInt(startDrag.x);
            int endDragX = Mathf.FloorToInt(curMousePos.x);
            int startDragY = Mathf.FloorToInt(startDrag.y);
            int endDragY = Mathf.FloorToInt(curMousePos.y);

            // draging to a negative direction so flip the order
			if (endDragX < startDragX)
            {
				int temp;
				temp = endDragX;
				endDragX = startDragX;
				startDragX = temp;
            }
			if (endDragY < startDragY)
            {
                int temp;
                temp = endDragY;
                endDragY = startDragY;
                startDragY = temp;
            }

         
			for (int i = 0; i < selectionHighlight.Count; i++)
            {
                SimplePool.Despawn(selectionHighlight[i]);
            }
            selectionHighlight.Clear();


			if (Input.GetMouseButton(0))
			{
            
				for (int x = startDragX; x <= endDragX; x++)
                {
                    for (int y = startDragY; y <= endDragY; y++)
                    {
						Tile tile = WorldController.Instance.World.GetTileAtPos(x, y);
                        if (tile != null)
						{
							GameObject cursorGO = SimplePool.Spawn(MouseCursorPrefab, new Vector3(x, y, 0), Quaternion.identity);
                            cursorGO.transform.SetParent(this.transform, true);
                            selectionHighlight.Add(cursorGO);
						}
					}
                }
			}

            // end drag
            if (Input.GetMouseButtonUp(0))
            {
                // all tiles under the selection
                for (int x = startDragX; x <= endDragX; x++)
                {
                    for (int y = startDragY; y <= endDragY; y++)
                    {
						Tile tile = WorldController.Instance.World.GetTileAtPos(x, y);
						if(tile != null)
							tile.Type = mode;
                    }
                }
            }
		}


		private void UpdateCamera()
		{
			// click and drag Camera
            if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
            {
                Vector3 diff = oldMousePos - curMousePos;
				Camera.main.transform.Translate(diff);
            
            }

			Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * Camera.main.orthographicSize;
			Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minScroll, maxScroll);
		}

		public void SetMode_Build(){
			mode = Tile.TileType.Grassland;
		}

		public void SetMode_Remove()
        {
			mode = Tile.TileType.Sea;
        }
	}   	
}

