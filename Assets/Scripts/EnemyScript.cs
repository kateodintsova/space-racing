using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Rigidbody enemy;
    public GameObject lazerShot;
    public GameObject playerExplosion;
    public GameObject asteroidExplosion;
    
    public float shotDelay;
    public float speed;
    float nextShot;
    int turn;
    private GameObject player;
    private Vector3 directionLazer;
    private GameControllerScript gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        enemy = GetComponent<Rigidbody>();
        turn = Random.Range(-1, 2); 
        enemy.velocity = new Vector3(turn * 0.5f, 0, -1) * speed;

        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (Time.time > nextShot)
        {
            GameObject newlazerShot = Instantiate(lazerShot, new Vector3(enemy.position.x, enemy.position.y, enemy.position.z - 5), Quaternion.identity);
            
            if (player)
                directionLazer = (player.transform.position - enemy.transform.position).normalized;
            else
                directionLazer = Vector3.back;

            newlazerShot.GetComponent<Rigidbody>().velocity = directionLazer * speed;

            nextShot = Time.time + shotDelay;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameBoundary" || other.tag == "EnemyShot" || other.tag == "Shield" || other.tag == "Player")
        {
            return;
        }
        if (other.tag == "Enemy")
        {
            Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
        } else if (other.tag == "Asteroid")
        {
            Instantiate(asteroidExplosion, other.transform.position, Quaternion.identity);
        } else if (other.tag == "PlayerShot")
        {
            gameController.incrementScore(50);
        }

        Destroy(gameObject);
        Destroy(other.gameObject);
        Instantiate(playerExplosion, transform.position, Quaternion.identity);
    }
}
