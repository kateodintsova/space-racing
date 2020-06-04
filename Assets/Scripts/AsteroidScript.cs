using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    #region Variables
    public float rotationSpeed;
    public float minSpeed, maxSpeed;

    public GameObject asteroidExplosion;
    public GameObject playerExplosion;

    private GameControllerScript gameController;
    # endregion

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        Rigidbody asteroid = GetComponent<Rigidbody>();
        asteroid.angularVelocity = Random.insideUnitSphere * rotationSpeed;
        asteroid.velocity = Vector3.back * Random.Range(minSpeed, maxSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameBoundary" || other.tag == "Asteroid" || other.tag == "Shield" || other.tag == "Player")
        {
            return;
        } else if (other.tag == "PlayerShot")
        {
            gameController.incrementScore(10);
        }

        Destroy(gameObject);
        Destroy(other.gameObject);
        Instantiate(asteroidExplosion, transform.position, Quaternion.identity);
    }
}
