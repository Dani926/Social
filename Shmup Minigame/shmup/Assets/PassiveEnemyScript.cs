using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveEnemyScript : MonoBehaviour
{
    public GameObject a;
    public GameObject player;

    

    // Start is called before the first frame update
    void Start()
    {
        if (a == null)
        {
            a = gameObject;
        }
        else
        {
            return;
        }

        player = PlayerScript.p;

        transform.Rotate(0, 0, Random.value * 360f);

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = BoundAndMove.WrapMovement(transform.position);
        GetComponent<Rigidbody2D>().AddForce(transform.up * Random.value * 10f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            PlayerScript.numPassive -= 1;
            PlayerScript.score += 2;

        }
    }

}
