using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour {
    //Todo: 游戏运行逻辑
    //List 2: 下落
    //List 3: 控制移动，旋转
    //List 4: 检测是否填满一层
    //List 5: 消除一层
    //List 6: 积分


    public enum Shape
    {
        A=0,
        B,
        C,
        I,
        J,
        L,
        O,
        T,
        S,
        Z 
    }
    public List<GameObject> shapePrefabs = new List<GameObject>();
    GameObject curShape;
    Control curControl;
    Placement placement;
    HeightMap heightMap;
	// Use this for initialization
	void Start () {
        CreateTetris(Shape.Z, 5, 3);
        placement = new Placement();
        heightMap = new HeightMap();
    }
    
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    CreateTetris((Shape)Random.Range(0,10), Random.Range(0,10), Random.Range(0,10));
        //}
        if (heightMap.CheckCords(curControl.cords))
        {
            curControl.Freeze();
            heightMap.AddCords(curControl.cords);
        }
	}

    void CreateTetris(Shape shape,int row, int col)
    {
        curShape = Instantiate(shapePrefabs[(int)shape]);
        curControl = curShape.GetComponent<Control>();
        curControl.row = row;
        curControl.col = col;
        curControl.height = 15;
        curControl.SetCord();
    }
}
public class HeightMap
{
    List<List<int>> heightMap = new List<List<int>>();
    public const int row = 10, col = 10, height = 20;
    public HeightMap()
    {
        for (int r = 0; r<row-1; r++)
        {
            heightMap.Add(new List<int>());
            for (int c = 0; c<col-1; c++)
            {
                heightMap[r].Add(-1);
            }
        }
    }
    public void AddCords(Control.Cord[] cords)
    {
        foreach(Control.Cord c in cords)
        {
            if (heightMap[c.row][c.col] < c.height)
            {
                heightMap[c.row][c.col] = c.height;
            }
        }
    }

    public bool CheckCords(Control.Cord[] cords)
    {
        foreach (Control.Cord c in cords)
        {
            if (heightMap[c.row][c.col]+1 == c.height)
            {
                return true;
            }
        }
        return false;
    }
}
public class Placement
{
    List<List<List<int>>> placement = new List<List<List<int>>>();
    public const int row = 10, col = 10, height=20;

    public Placement()
    {
        for(int r = 0; r<row-1; r++)
        {
            placement.Add(new List<List<int>>());
            for(int c = 0; c<col-1; c++)
            {
                placement[r].Add(new List<int>());
                for(int h = height-1; h >= 0; h--)
                {
                    placement[r][c].Add(0);
                }
            }
        }
    }
    
    public bool IsFill(int row, int col, int height)
    {
        return placement[row][col][height]==1;
    }

    public bool IsFill(int height)
    {
        for (int r = row - 1; r >= 0; r--)
        {
            for (int c = col - 1; c >= 0; c--)
            {
                if (placement[r][c][height] == 0) return false;
            }
        }
        return true;
    }

    public void AddCords(Control.Cord[] cords)
    {
        foreach (Control.Cord c in cords)
        {
            placement[c.row][c.col][c.height] = 1;
        }
    }
}
