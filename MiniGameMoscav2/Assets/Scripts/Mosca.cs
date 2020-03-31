using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosca : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform moveSpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public GameObject choque;
    private AudioSource audioSource;
    public static int gameover = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "raquete")
        {
            if (Input.GetKey("space"))
            {
                gameover = 1;
                GameObject e = Instantiate(choque) as GameObject;
                e.transform.position = transform.position;
                Destroy(gameObject);
                this.enabled = false;

            }
        }

    }


        // Start is called before the first frame update
        void Start()
    {
        audioSource = GetComponent<AudioSource>();
        waitTime = startWaitTime;
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTime = startWaitTime;
            } else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
