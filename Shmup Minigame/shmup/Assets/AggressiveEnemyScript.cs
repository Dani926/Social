using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AggressiveEnemyScript : MonoBehaviour
{
    public GameObject player;
    public GameObject e;
    public GameObject agroLaserPrefab;
    public float lastTime;
    public GameObject shieldPrefab;
    public AudioSource pew;



    // Start is called before the first frame update
    void Start()
    {
        if (e == null)
        {
            e = gameObject;
        }
        else
        {
            return;
        }

        player = PlayerScript.p;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 findTarget = player.transform.position - transform.position;
        transform.up = findTarget;
        GetComponent<Rigidbody2D>().AddForce(transform.up * 10f);
        transform.position = BoundAndMove.WrapMovement(transform.position);

        if((Time.time - lastTime) > .4f)
        {
            LaserScript.ShootLaser(agroLaserPrefab, e, 4f);
            lastTime = Time.time;
            pew.GetComponent<AudioSource>().Play();
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            PlayerScript.score += 5;
            float r = Random.Range(0, 100);
            if(r < 40)
            {
                Instantiate(shieldPrefab, transform.position, transform.rotation);
            }

        }
        

    }
}
