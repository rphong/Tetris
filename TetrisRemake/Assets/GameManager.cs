using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject endGameText;
    // Start is called before the first frame update
    public void EndGame()
    {
        Spawner spawner = FindObjectOfType<Spawner>();
        spawner.endMusic();
        Destroy(spawner);

        FindObjectOfType<AudioManager>().Play("GameOver");

        if (FindObjectOfType<scoreScript>().getScore() > 5000)
        {
            StartCoroutine(WinSound());
        }

        else
        {
            StartCoroutine(DefaultEnd());
        }
        GameObject endText = GameObject.Instantiate(endGameText, new Vector3(-7f, 12, 2), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform); ;
        

    }

    IEnumerator WinSound()
    {
        yield return new WaitForSeconds(2);
        FindObjectOfType<AudioManager>().Play("Winner");
        GameObject winText = GameObject.Instantiate(endGameText, new Vector3(-8.2f, 7, 2), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        winText.GetComponent<Text>().text = "Great Score!";

        yield return new WaitForSeconds(2);
        FindObjectOfType<AudioManager>().Play("WinnerBGM");
    }

    IEnumerator DefaultEnd()
    {
        yield return new WaitForSeconds(2);
        FindObjectOfType<AudioManager>().Play("GameEndBGM");

    }
}
