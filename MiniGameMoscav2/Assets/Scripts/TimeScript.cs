using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeScript : MonoBehaviour
{

    AudioSource audioData;
    public AudioClip winClip;
    public AudioClip Haha;
    public static int i = 1;
    public GameObject gameManager;



    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        if (i == 2)
        {
              audioData.pitch += 0.2f;
        }
    }  

    // Update is called once per frame
    void Update()
    {
        gameManager.GetComponent<GameManager>().pass = false;
        i = 2;
        if (Mosca.gameover == 1)
        {
            gameManager.GetComponent<GameManager>().pass = true;
            audioData.Stop();
            audioData.PlayOneShot(Haha);
            this.enabled = false;
            if (Time.timeScale > 1.1f)
            {
                audioData.PlayOneShot(winClip);
                Time.timeScale = 0;
                this.enabled = false;

            }
            else
            {
                Mosca.gameover = 0;
            }

        }

    }


}
