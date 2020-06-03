using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEditor;
using UnityEngine.Tilemaps;

public class GridDebug : MonoBehaviour
{
    private GridTest grid;
    public GameObject[] moveables;
    public GameObject[] lights;
    public float cellSize = .32f;
    public Tilemap tilemap;
    public Tile CollisionTile;
    public GameObject Collidier;
    public GameObject Trigger;
    public GameObject TriggerGrid;
    


    
    private void Grid_OnGridValueChanged(object sender, GridTest.OnGridValueChangedEventArgs e)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        grid = new GridTest(50, 50, tilemap);
        
        PlaceTilemap();
        PlaceColliders();
        //if (moveables == null)
        //{
        //    moveables = grid.CollectTaggedObject("MOVEABLE");
        //}
        //if (lights == null)
        //{
        //    lights = grid.CollectTaggedObject("LIGHTSOURCE");
        //}
        grid.OnGridValueChanged += Grid_OnGridValueChanged;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            grid.GetPathingGoal(UtilsClass.GetMouseWorldPosition(), 5);
        }
        if (Input.GetMouseButtonDown(1))
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(),1);
            


        }
        //if (Input.GetKeyDown("space"))
        //{
        //    grid.MoveObstacleFrom(UtilsClass.GetMouseWorldPosition());
        //}
        //if (Input.GetKeyUp("space"))
        //{
        //    grid.MoveObstacleTo(UtilsClass.GetMouseWorldPosition());
        //}
        
        
        
    }
    private void FixedUpdate()
    {
        //zur Laufzeit GameObjects mit gesetztem Tag sammeln
        moveables = grid.CollectTaggedObject("MOVEABLE");
        lights = grid.CollectTaggedObject("LIGHTSOURCE");
        //alte GridWerte der Obstacles resetten
        grid.ResetMoveables();
        //für alle gefundenen Objekte collider abfragen und im grid entsprechend Werte setzen
        PlaceMovables();
        //alte Lichtwerte im Grid decayen lassen
        grid.Decay(2);

        PlaceLights();


    }

    public void PlaceColliders()
    {
        for (int x = 0; x <= grid.width; x++)
        {
            for (int y = 0; y <= grid.height; y++)
            {
                if (grid.GetValue(x,y) == 0)
                {
                    var _collider = Instantiate(Collidier,grid.GetOriginPosition() + new Vector3(x*grid.GetCellSize() + grid.GetCellSize()/2, y * grid.GetCellSize() + grid.GetCellSize() / 2, 0) , Quaternion.identity);
                    _collider.transform.parent = grid.GetTilemapParent();

                    var _trigger = Instantiate(Trigger, grid.GetOriginPosition() + new Vector3(x * grid.GetCellSize() + grid.GetCellSize() / 2, y * grid.GetCellSize() + grid.GetCellSize() / 2, 0), Quaternion.identity);
                    _trigger.transform.parent = TriggerGrid.transform;
                }
            }
        }
    }

    public void PlaceTilemap()
    {
        
        for (int x = 0; x <= grid.width; x++)
        {
            if (x > tilemap.cellBounds.size.x) {
                continue;
            }
            for (int y = 0; y <= grid.height; y++)
            {
                if (y > tilemap.cellBounds.size.y)
                {
                    continue;
                }
                

                if (tilemap.GetTile(new Vector3Int(x + (tilemap.origin.x), y + (tilemap.origin.y), 0)) == CollisionTile)
                {
                    grid.SetValue(x, y, 0);
                }
            }
        }
    }

    public void PlaceMovables()
    {
        for (int i = 0; i < moveables.Length; i++)
        {
            BoxCollider2D collider = moveables[i].GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                Vector2 size = collider.bounds.size;

                Vector3 originBoundingBox = collider.bounds.center - (new Vector3(size.x, size.y, 0) * 0.5f);
                int x, y;
                grid.GetGridCoord(originBoundingBox, out x, out y);
                int x2, y2;
                grid.GetGridCoord(originBoundingBox + new Vector3(size.x, size.y, 0), out x2, out y2);

                for (int _x = x; _x <= x2; _x++)
                {

                    for (int _y = y; _y <= y2; _y++)
                    {
                        if (grid.GetValue(_x, _y) != 0)
                        {
                            grid.SetValue(_x, _y, -1);
                        }
                    }
                }

            }
        }
    }

    public void PlaceLights()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            BoxCollider2D collider = lights[i].GetComponent<BoxCollider2D>();
            if (collider != null)
            {


                Vector3 centerBoundingBox = collider.bounds.center;
                grid.GenLight(centerBoundingBox, 3, 50);

            }
        }
    }
    public Vector3 GetGoal(Vector3 pos,int range)
    {
        return grid.GetPathingGoal(pos, range);
    }
}
