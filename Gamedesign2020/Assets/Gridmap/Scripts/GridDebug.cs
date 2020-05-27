using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEditor;

public class GridDebug : MonoBehaviour
{
    private GridTest grid;
    public GameObject[] moveables;
    public GameObject[] lights;

    // Start is called before the first frame update
    void Start()
    {
        grid = new GridTest(50, 50,1f,new Vector3(-5,-5));
        //if (moveables == null)
        //{
        //    moveables = grid.CollectTaggedObject("MOVEABLE");
        //}
        //if (lights == null)
        //{
        //    lights = grid.CollectTaggedObject("LIGHTSOURCE");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
        //if (Input.GetMouseButtonDown(0))
        //{
        //    grid.GenLight(UtilsClass.GetMouseWorldPosition(), 5,500);
        //}
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
        for (int i = 0; i < moveables.Length; i++)
        {
            BoxCollider2D collider = moveables[i].GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                Vector2 size = collider.bounds.size;

                Vector3 originBoundingBox = collider.bounds.center- (new Vector3(size.x, size.y, 0) *0.5f);
                int x, y;
                grid.GetGridCoord(originBoundingBox, out x, out y);
                int x2, y2;
                grid.GetGridCoord(originBoundingBox + new Vector3(size.x,size.y,0), out x2, out y2);
                
                for (int _x=x; _x <= x2; _x++) { 

                    for(int _y=y; _y <= y2;_y++)
                    {
                        if (grid.GetValue(_x, _y) != 0)
                        {
                            grid.SetValue(_x, _y, -1);
                        }
                    }
                }

            }
        }
        //alte Lichtwerte im Grid decayen lassen
        grid.Decay(2);
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

}
