using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovement : MonoBehaviour
{
    private float prevTimeY;
    private float prevTimeX;
    public float fallTime = 1f;
    public float DAS = .5f;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 12;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)) //Inital move left
        {
            transform.position += new Vector3(-1, 0, 0);
            prevTimeX = Time.time;
        }

        if(Input.GetKey(KeyCode.LeftArrow)) //If key is still held down keep moving left
        {
            if(Time.time - prevTimeX > DAS)
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow)) prevTimeX = Time.time; //when key is let go reset

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            prevTimeX = Time.time;
        }

        if (Input.GetKey(KeyCode.RightArrow)) //If key is still held down keep moving right
        {
            if (Time.time - prevTimeX > DAS)
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }

        if (Input.GetKeyUp(KeyCode.RightArrow)) prevTimeX = Time.time; //when key is let go reset

        if (Time.time - prevTimeY > (Input.GetKey(KeyCode.DownArrow) ? 0 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            prevTimeY = Time.time;
        }
    }
}
