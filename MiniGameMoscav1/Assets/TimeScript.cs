using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeScript : MonoBehaviour
{

    Image Timer;
    public float maxTime = 5f;
    float timeLeft;
    public GameObject GameOver;
    public GameObject Win;
    public GameObject Faster;
    AudioSource audioData;
    public AudioClip overClip;
    public AudioClip winClip;
    public AudioClip Haha;
    private static int i = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        GameOver.SetActive(false);
        Win.SetActive(false);
        Faster.SetActive(false);
        Timer = GetComponent<Image>();
        timeLeft = maxTime;
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        if (i == 2)
        {
            Time.timeScale += 0.2f;
            audioData.pitch += 0.2f;
        }
    }  

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(run());

    }
    IEnumerator run()
    {
        if (timeLeft > 0 && Mosca.gameover == 0)
        {
            timeLeft -= Time.deltaTime;
            Timer.fillAmount = timeLeft / maxTime;
        }
        else if (Mosca.gameover == 1)
        {
            audioData.Stop();
            audioData.PlayOneShot(Haha);
            this.enabled = false;
            yield return new WaitForSeconds(2.6f);

            if (Time.timeScale > 1.3f)
            {
                audioData.PlayOneShot(winClip);
                Win.SetActive(true);
                Time.timeScale = 0;
                this.enabled = false;

            } else
            {
                Faster.SetActive(true);
                yield return new WaitForSeconds(1);
                Faster.SetActive(false);
                i = 2;
                SceneManager.LoadScene("SampleScene");
                Mosca.gameover = 0;
            }

        }
        else
        {
            audioData.Stop();
            audioData.PlayOneShot(overClip);
            GameOver.SetActive(true);
            Time.timeScale = 0;
            this.enabled = false;
        }



        
        
    }

}
