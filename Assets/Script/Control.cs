using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {

    public float speed=1.0f;
    public int row { get; set; }
    public int col { get; set; }
    public int height { get; set; }
    bool freeze = false;
    public struct Cord
    {
        public int row, col, height;
        public Cord(int r,int h, int c)
        {
            row = r;
            height = h;
            col = c;
        }
        public override string ToString()
        {
            return "[" + row + "," + col + "," + height + "]";
        }
    }
    public Cord[] cords=new Cord[4];
	// Use this for initialization
	void Start () {
        this.transform.position = new Vector3(row - 0.5f, height+1, -1 * col + 1.5f);
        UpdateCord();
    }

    // Update is called once per frame
    void Update () { 
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveForward();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveBackward();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            RotateZ();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RotateX();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            RotateY();
        }
        if(!freeze)
            Falling();
    }

    public void Freeze()
    {
        Debug.Log("Freeze");
        freeze = true;
        height = (int)(this.transform.position.y - 1);
        this.transform.position = new Vector3(row - 0.5f, height+1, -1 * col + 1.5f);
    }
    void UpdateCord()
    {
        Vector3 pos = this.transform.GetChild(0).position;
        cords[0] = new Cord((int)(pos.x - 0.5f), (int)(pos.y - 1), (int)(pos.z + 0.5f) * -1);

        pos = this.transform.GetChild(1).position;
        cords[1] = new Cord((int)(pos.x - 0.5f), (int)(pos.y - 1), (int)(pos.z + 0.5f) * -1);

        pos = this.transform.GetChild(2).position;
        cords[2] = new Cord((int)(pos.x - 0.5f), (int)(pos.y - 1), (int)(pos.z + 0.5f) * -1);

        pos = this.transform.GetChild(3).position;
        cords[3] = new Cord((int)(pos.x - 0.5f), (int)(pos.y - 1), (int)(pos.z + 0.5f) * -1);
        Debug.Log("1-" + cords[0] + "\t2-" + cords[1] + "\t3-" + cords[2] + "\t4-" + cords[3]);
        CheckBounds();
    }

    void CheckBounds()
    {
        foreach(Cord c in cords)
        {
            if (c.row < 0) MoveRight();//move right
            if (c.row > Placement.row) MoveLeft();//move left
            if (c.col < 0) MoveBackward();//move back
            if (c.col > Placement.col) MoveForward();//move forward
        }
    }

    public void SetCord()
    {
        this.transform.position = new Vector3(row - 0.5f, this.transform.position.y,- 1 * col + 1.5f);
        UpdateCord();
    }
    void MoveLeft()
    {
        row--;
        if (row <= 0) row = 0;
        SetCord();
    }
    void MoveRight()
    {
        row++;
        if (row >= Placement.row) row = Placement.row;
        SetCord();
    }
    void MoveForward()
    {
        col--;
        if (col <= 0) col = 0;
        SetCord();
    }
    void MoveBackward()
    {
        col++;
        if (col >= Placement.col) col = Placement.col;
        SetCord();
    }
    void RotateZ()
    {
        //this.transform.eulerAngles = this.transform.eulerAngles + new Vector3(0, 0, 90);
        this.transform.Rotate(Vector3.forward, 90, Space.World);
        UpdateCord();
    }
    void RotateX()
    {
        //this.transform.eulerAngles = this.transform.eulerAngles + new Vector3(0, 0, 90);
        this.transform.Rotate(Vector3.left, 90, Space.World);
        UpdateCord();
    }
    void RotateY()
    {
        //this.transform.eulerAngles = this.transform.eulerAngles + new Vector3(0, 0, 90);
        this.transform.Rotate(Vector3.up, 90, Space.World);
        UpdateCord();
    }
    void Falling()
    {
        this.transform.Translate(Vector3.down * speed*Time.deltaTime, Space.World);
        int curHeight= (int)(this.transform.position.y - 1);
        if (curHeight != height)
        {
            height = curHeight;
            UpdateCord();
        }
    }
}
