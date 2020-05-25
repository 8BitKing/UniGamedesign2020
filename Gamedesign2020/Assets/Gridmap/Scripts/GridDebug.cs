using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridDebug : MonoBehaviour
{
    private GridTest grid;
    
    // Start is called before the first frame update
    void Start()
    {
         grid = new GridTest(50, 50,3f,new Vector3(-25,-25));
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            grid.GenLight(UtilsClass.GetMouseWorldPosition(), 5,500);
        }
        if (Input.GetMouseButtonDown(1))
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(),1);
            


        }
        if (Input.GetKeyDown("space"))
        {
            grid.MoveObstacleFrom(UtilsClass.GetMouseWorldPosition());
        }
        if (Input.GetKeyUp("space"))
        {
            grid.MoveObstacleTo(UtilsClass.GetMouseWorldPosition());
        }
        
        
        
    }
    private void FixedUpdate()
    {
       grid.Decay(2); 
    }

}
