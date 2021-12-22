using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] blocks;
    // Start is called before the first frame update
    void Start()
    {
        newBlock();
    }

    // Update is called once per frame
    public void newBlock()
    {
        Instantiate(blocks[Random.Range(0, blocks.Length)], transform.position, Quaternion.identity);
    }
}
