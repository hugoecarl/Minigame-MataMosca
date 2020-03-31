using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raquete : MonoBehaviour
{
    public float moveSpeed;
    public AudioClip choqueClip;
    public AudioClip choqueMoClip;
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime, 0f);
        int i = 0;

        if (audioSource.isPlaying)
        i = 1;

        if (Input.GetKey("space") && i == 0)
        audioSource.PlayOneShot(choqueClip);

        if (Mosca.gameover == 1)
        {
            audioSource.PlayOneShot(choqueMoClip);
            this.enabled = false;
        } else if (Time.timeScale == 0){
            this.enabled = false;
        }
    }
}