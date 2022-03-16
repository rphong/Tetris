using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceMovement : MonoBehaviour
{
    public Vector3 rotationPt;
    private float prevTimeY;
    private float prevTimeX;
    private float prevTimeARR;
    private float fallTime = 1f;
    public float DAS = .5f;
    public float ARR = 2f;
    public static int width = 10;
    public static int height = 21;
    public static Transform[,] grid = new Transform[width, height];
    bool gameStatus = true;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
    }

    // Update is called once per frame
    void Update()
    {
        //movements

        if(Input.GetKeyDown(KeyCode.LeftArrow)) //Inital move left
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!validMove())
                transform.position += new Vector3(1, 0, 0);
            else
            {
                prevTimeX = Time.time;
                FindObjectOfType<AudioManager>().Play("Move");
            }
        }

        else if (Input.GetKey(KeyCode.LeftArrow)) //If key is still held down keep moving left
        {
            if(Time.time - prevTimeX > DAS)
            {
                if (Time.time - prevTimeARR > ARR)
                {
                    transform.position += new Vector3(-1, 0, 0);
                    if (!validMove())
                        transform.position += new Vector3(1, 0, 0);
                    else
                    {
                        FindObjectOfType<AudioManager>().Play("Move");
                        prevTimeARR = Time.time;
                    }
                }
            }
        }

        else if (Input.GetKeyUp(KeyCode.LeftArrow)) prevTimeX = Time.time; //when key is let go reset

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!validMove())
                transform.position += new Vector3(-1, 0, 0);
            else
            {
                prevTimeX = Time.time;
                FindObjectOfType<AudioManager>().Play("Move");
            }
        }

        else if (Input.GetKey(KeyCode.RightArrow)) //If key is still held down keep moving right
        {
            if (Time.time - prevTimeX > DAS)
            {
                if (Time.time - prevTimeARR > ARR)
                {
                    transform.position += new Vector3(1, 0, 0);
                    if (!validMove())
                        transform.position += new Vector3(-1, 0, 0);
                    else
                    {
                        FindObjectOfType<AudioManager>().Play("Move");
                        prevTimeARR = Time.time;
                    }
                }
            }
        }

        else if (Input.GetKeyUp(KeyCode.RightArrow)) prevTimeX = Time.time; //when key is let go reset



        if (Time.time - prevTimeY > (Input.GetKey(KeyCode.DownArrow) ? 0 : fallTime)) //moving down
        {
            transform.position += new Vector3(0, -1, 0);
            if (!validMove())
            {
                transform.position += new Vector3(0, 1, 0);
                FindObjectOfType<AudioManager>().Play("HitBottom");
                addToGrid();
                checkForLines();
                if (grid[5, 19] != null && gameStatus)
                {
                    FindObjectOfType<GameManager>().EndGame();
                    Debug.Log("hello");
                    gameStatus = false; 
                    this.enabled = false;
                }
                else
                {
                    this.enabled = false;
                    FindObjectOfType<Spawner>().newBlock();
                }
            }
            prevTimeY = Time.time;
        }

        //rotations

        if(Input.GetKeyDown(KeyCode.X))
        {
            transform.RotateAround(transform.TransformPoint(rotationPt), new Vector3(0, 0, 1), -90);
            if(!validMove())
                transform.RotateAround(transform.TransformPoint(rotationPt), new Vector3(0, 0, 1), 90);
            else
                FindObjectOfType<AudioManager>().Play("Rotate");

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            transform.RotateAround(transform.TransformPoint(rotationPt), new Vector3(0, 0, 1), 90);
            if (!validMove())
                transform.RotateAround(transform.TransformPoint(rotationPt), new Vector3(0, 0, 1), -90);
            else
                FindObjectOfType<AudioManager>().Play("Rotate");
        }
    }
    
    void addToGrid()
    {
        foreach (Transform child in transform)
        {
            int roundedX = Mathf.RoundToInt(child.position.x);
            int roundedY = Mathf.RoundToInt(child.position.y);

            grid[roundedX, roundedY] = child;
        }
    }


    void checkForLines()
    {
        int linesCleared = 0;
        for(int row = height-1; row >= 0; row--)
        {
            if(hasLine(row))
            {
                linesCleared++;
                deleteLine(row);
                moveDown(row);
            }
        }

        switch (linesCleared)
        {
            case 1:
                scoreScript.scoreVal += 40;
                FindObjectOfType<AudioManager>().Play("LineClear");
                break;
            case 2:
                scoreScript.scoreVal += 100;
                FindObjectOfType<AudioManager>().Play("LineClear");
                break;
            case 3:
                scoreScript.scoreVal += 300;
                FindObjectOfType<AudioManager>().Play("LineClear");
                break;
            case 4:
                scoreScript.scoreVal += 1200;
                FindObjectOfType<AudioManager>().Play("Tetris");
                break;
        }
                
    }

    bool hasLine(int row)
    {
        for(int column = 0; column < width; column++)
        {
            if (grid[column, row] == null)
                return false;
        }
        return true;
    }

    void deleteLine(int row)
    {
        for (int column = 0; column < width; column++)
        {
            Destroy(grid[column, row].gameObject);
            grid[column, row] = null;
        }
    }

    void moveDown(int row)
    {
        for(int currRow = row; currRow < height; currRow++)
        {
            for(int column = 0; column < width; column++)
            {
                if(grid[column, currRow] != null)
                {
                    grid[column, currRow - 1] = grid[column, currRow];
                    grid[column, currRow] = null;
                    grid[column, currRow - 1].transform.position += new Vector3(0, -1, 0); 
                }
            }
        }
    }

    bool validMove()
    {
        foreach(Transform child in transform)
        {
            int roundedX = Mathf.RoundToInt(child.position.x);
            int roundedY = Mathf.RoundToInt(child.position.y);

            if (roundedX >= width || roundedX < 0 || roundedY >= height || roundedY < 0)
            {
                return false;
            }
            
            if(grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }
        return true;
    }
}
