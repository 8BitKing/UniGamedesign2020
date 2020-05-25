using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System.Dynamic;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

public class GridTest
{

    private int width;
    private int height;
    private float cellSize;
    private int[,] gridArray;
    private Vector3 originPos;
    private Vector3 movefrom;
    private bool movefromUsed = false;
    
    //debug kram
    private TextMesh[,] debugTextArray;

    //Grid Constructor mit Debug Informationen
  public GridTest(int width, int height, float cellSize, Vector3 originPos)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPos = originPos;
        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++) {

                debugTextArray[x,y]= UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPos(x, y)+ new Vector3(cellSize,cellSize)*0.5f, 10,Color.white,TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPos(x, y), GetWorldPos(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPos(x, y), GetWorldPos(x + 1, y), Color.white, 100f);

            }
            Debug.DrawLine(GetWorldPos(0, height), GetWorldPos(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPos(width, 0), GetWorldPos(width, height), Color.white, 100f);

        }
           
        
    }
    public GameObject[] CollectTaggedObject(string Tag)
    {
        GameObject[] objects;
        objects= GameObject.FindGameObjectsWithTag(Tag);
        return objects;
    }

    //erhält grid koordinate und verschiebt sie in Weltkoordinaten
    private Vector3 GetWorldPos(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPos;
    }

    //erhält Weltkoordinate und rechnet um in Gridkoordinate
    public void GetGridCoord(Vector3 worldPos,out int x,out int y)
    {
        x = Mathf.FloorToInt((worldPos - originPos).x / cellSize);
        y = Mathf.FloorToInt((worldPos - originPos).y / cellSize);

    }

    //Funktionen um festen Wert an gegebener Stelle im Grid zu setzen
    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }
    }
    public void SetValue(Vector3 worldPos, int value)
    {
        int x, y;
        GetGridCoord(worldPos, out x, out y);
        SetValue(x, y, value);
    }

    //Methoden um als Rückgabewert den Integer-Wert an gegebener Stelle im Grid zu erhalten
    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];

        }
        else { return -1; 
        }
    }
    public int GetValue(Vector3 worldPos)
    {
        int x, y;
        GetGridCoord(worldPos, out x, out y);
        return GetValue(x, y);
    }

    //Methode um an Position (x,y) in Grid Koords Lichtwerte im radius "radius" und in max-höhe vom Wert "brightness" im zentrum zu setzen
   public void GenLight(int x, int y,int radius,int brightness)
    {
        float lightFactor;
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            Vector2 center = new Vector2(x, y);
            for (int i = 0; i < gridArray.GetLength(0); i++)
            {
                for (int j = 0; j < gridArray.GetLength(1); j++)
                {
                    Vector2 curr = new Vector2(i, j);
                    Vector2 dist = curr - center;
                    lightFactor = (brightness *(1/(dist.magnitude+1)));
                    if (dist.magnitude <= radius)
                    {
                        if (GetValue(i,j) < Mathf.FloorToInt(lightFactor)&&GetValue(i,j)>0)
                        {
                            SetValue(i, j, Mathf.FloorToInt(lightFactor));
                        }
                    }

                }
            }
        }
    }

    //umrechnung von welt in grid koord zur platzierung der lichtwerte
    public void GenLight(Vector3 worldPos, int radius, int brightness)
    {
        int x, y;
        GetGridCoord(worldPos, out x, out y);
        GenLight(x, y, radius, brightness);
    }
    public void Decay(float rate)
    {
        for(int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                float curr = GetValue(x,y) - ((GetValue(x,y) * (1 / rate)) * Time.deltaTime);
               
               

                 
                    if (curr >= 1 )
                    {
                        SetValue(x,y,Mathf.FloorToInt(curr));

                    }
                    
                
            }
        }
    }
    public void MoveObstacleFrom(Vector3 currpos)
    {
        if (GetValue(currpos) == 0)
        {
            movefrom = currpos;
            movefromUsed = true;
            SetValue(currpos, 1);

        }
        else movefromUsed = false;
    }
    public void MoveObstacleTo(Vector3 targetpos) {
       
        if (movefromUsed&&GetValue(targetpos)>0) {
            int x, y;
            GetGridCoord(targetpos, out x, out y);
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                SetValue(x,y,0);
            }


        }else if (GetValue(targetpos) == 0)
        {
            SetValue(movefrom, 0);
        }
        
    
    }
    public void ResetMoveables()
    {
        for(int x = 0; x < gridArray.GetLength(0); x++)
        {
            for(int y = 0; y < gridArray.GetLength(1); y++)
            {
                if (GetValue(x, y) == -1)
                {
                    SetValue(x, y, 1);
                }
            }
        }
    }
}
