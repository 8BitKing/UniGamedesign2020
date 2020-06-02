﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System.Dynamic;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class GridTest
{
    [NonSerialized]
    public int width;
    [NonSerialized]
    public int height;
    private float cellSize;
    private int[,] gridArray;
    private Vector3 originPos;
    private Vector3 movefrom;
    private bool movefromUsed = false;
    
    //debug kram
    private TextMesh[,] debugTextArray;

    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }


    //Grid Constructor mit Debug Informationen
  public GridTest(int width, int height, Tilemap tilemap)
    {
        this.width = width;
        this.height = height;
        this.cellSize = tilemap.cellSize.x * tilemap.transform.localScale.x;
        this.originPos = (Vector3)tilemap.origin*cellSize;
        gridArray = new int[width, height];
        //debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++) {
                //standart 1
                gridArray[x, y] = 1;

                //debugTextArray[x,y]= UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPos(x, y)+ new Vector3(cellSize,cellSize)*0.5f, 10,Color.white,TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPos(x, y), GetWorldPos(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPos(x, y), GetWorldPos(x + 1, y), Color.white, 100f);

            }
            Debug.DrawLine(GetWorldPos(0, height), GetWorldPos(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPos(width, 0), GetWorldPos(width, height), Color.white, 100f);

            //OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) => {
            //    debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y].ToString();
            //};
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
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
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
            for (int i = x-(radius+1); i < Mathf.Min(x + (radius+1), width); i++)
            {
                for (int j = y - (radius+1); j < Mathf.Min(y + (radius+1), height); j++)
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

    public void GetPathingGoal(Vector3 currPos,int visionRange)
    {
        int x, y;
        GetGridCoord(currPos, out x, out y);
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            GetPathingGoal(x, y, visionRange);
        }
    }
    public void GetPathingGoal(int x, int y, int visionRange)
    {
        
        Vector3 center = GetWorldPos(x, y);
        Vector3 maxLight = new Vector3(x,y,1);
        
        center.x += 0.5f * cellSize;
        center.y += 0.5f * cellSize;
        Vector2 centerGrid = new Vector2(x, y);
        for (int i = x - (visionRange + 1); i < Mathf.Min(x + (visionRange + 1), width); i++)
        {
            for (int j = y - (visionRange + 1); j < Mathf.Min(y + (visionRange + 1), height); j++)
            {
                Vector3 curr = GetWorldPos(i, j);
                curr.x += 0.5f * cellSize;
                curr.y += 0.5f * cellSize;
                Vector3 dist = curr - center;
                Vector2 currGrid = new Vector2(i, j);
                Vector2 distGrid = currGrid - centerGrid;
                if (distGrid.magnitude <= visionRange&& GetValue(i, j)>maxLight.z)
                {
                    
                    RaycastHit2D hit=Physics2D.Raycast(new Vector2(center.x,center.y), new Vector2 (dist.x,dist.y));


                    Debug.Log(hit.distance+"vs"+dist.magnitude);

                    if (hit.distance > dist.magnitude||hit.distance==0)
                    {

                        maxLight = new Vector3(i, j, GetValue(i, j));
                    }
                            
                    
                    
                    
                }

            }
        }
        Vector3 dest= GetWorldPos((int)maxLight.x,(int)maxLight.y);
        dest.x += 0.5f * cellSize;
        dest.y += 0.5f * cellSize;
        Debug.DrawRay(center,dest-center , Color.green, 1f, false);
        //Debug.Log("Pathing Goal liegt bei " + maxLight);
    }
}
