using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public int lives;
    public static int score;
    public int highScore;
    public static GameObject p;
    GameObject agroEnemy;
    public GameObject agroEnemyPrefab;
    public GameObject pasvEnemyPrefab;
    public GameObject laserPrefab;
    public bool enabledRen = true;
    public float invokeTime;
    public static float numPassive = 4;
    public static bool shieldsOn;
    public static float shieldTime;
    public GameObject pew;

    // Start is called before the first frame update
    void Start()
    {
        lives = 15;

        highScore = PlayerPrefs.GetInt("HS", 100);

        if(p == null)
        {
            p = gameObject;
        } else
        {
            return;
        }

        SpawnAgro();

        for(int i = 0; i < 4; i++)
        {
            Instantiate(pasvEnemyPrefab);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("lives").GetComponent<Text>().text = "Lives: " + lives;
        GameObject.Find("highScore").GetComponent<Text>().text = "HighScore: " + highScore;
        print("lives:" + lives);
        if (lives == 0)
        {
            GameOver();
        }

        if(score > highScore)
        {
            highScore = score;
            GameObject.Find("highScore").GetComponent<Text>().text = "HighScore: " + highScore;
        }

        GameObject.Find("score").GetComponent<Text>().text = "Score: " + score;

        transform.position = BoundAndMove.WrapMovement(transform.position);

        if(((Time.time - invokeTime) >= 2f) || shieldsOn)
        {
            CancelInvoke("TookDamage");
            if (!gameObject.GetComponent<SpriteRenderer>().enabled)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        if(shieldsOn == true && (Time.time - shieldTime) >= 8f)
        {
            shieldsOn = false;
            shieldTime = 0f;
            p.GetComponent<Renderer>().material.color = Color.white;

        }

        SpawnPasv();

        float translation = Input.GetAxis("Vertical") /5f;
        float rotation = Input.GetAxis("Horizontal") * -5f;

        transform.Translate(0, translation, 0);
        transform.Rotate(0, 0, rotation);
        

        if (Input.GetKeyDown(KeyCode.Z))
        {

            LaserScript.ShootLaser(laserPrefab, p, 10f);
            pew.GetComponent<AudioSource>().Play();
        }
    }

    public static void shield()
    {
        p.GetComponent<Renderer>().material.color = Color.green;
    }

    public void SpawnAgro()
    {
        Vector2 enempyPos = new Vector2(Random.Range(-7, 7), Random.Range(-5, 5));
        while(enempyPos.x == transform.position.x && enempyPos.y == transform.position.y)
        {
            enempyPos.x = Random.Range(-7, 7);
            enempyPos.y = Random.Range(-5, 5);
        }

        Instantiate(agroEnemyPrefab, enempyPos, Quaternion.identity);

        Invoke("SpawnAgro", 10f);
    }

    public void SpawnPasv()
    {
        if (numPassive == 0)
        {
            float r = Random.Range(2, 5);
            for (int i = 0; i < r; i++)
            {
                Vector2 enempyPos = new Vector2(Random.Range(-7, 7), Random.Range(-5, 5));
                while (enempyPos.x == transform.position.x && enempyPos.y == transform.position.y)
                {
                    enempyPos.x = Random.Range(-7, 7);
                    enempyPos.y = Random.Range(-5, 5);
                }

                Instantiate(pasvEnemyPrefab, enempyPos, Quaternion.identity);
            }
            numPassive = r;
        }
    }

    void TookDamage()
    {

        if (enabledRen)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            enabledRen = false;
            print("false");

        } else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            enabledRen = true;
            print("true");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);

            if (!shieldsOn)
            {
                InvokeRepeating("TookDamage", 0f, .5f);
                invokeTime = Time.time;

                if (lives != 0)
                {
                    lives -= 1;
                }
            }
        }
  
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("HS", highScore);
        SceneManager.LoadScene("GameOver");
    }
}
