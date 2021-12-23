using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] blocks;

    AudioSource music;
    void Start()
    {
        newBlock();
        music = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void newBlock()
    {
        Instantiate(blocks[Random.Range(0, blocks.Length)], transform.position, Quaternion.identity);
    }

    public void endMusic()
    {
        music.Stop();
    }
}
