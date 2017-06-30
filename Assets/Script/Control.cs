using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {

    public float speed=1.0f;
	// Use this for initialization
	void Start () {
		
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
            Rotate();
        }
    }
    void MoveLeft()
    {
        this.transform.Translate(Vector3.left*speed,Space.World);
    }

    void MoveRight()
    {
        this.transform.Translate(Vector3.right*speed, Space.World);
    }
    void Rotate()
    {
        this.transform.eulerAngles = this.transform.eulerAngles + new Vector3(0, 0, 90);
    }

}
