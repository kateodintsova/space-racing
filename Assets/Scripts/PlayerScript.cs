using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    #region Variables
    public float xMin, xMax, zMin, zMax;
    public float speed;
    public float tilt;
    public int maxCountShields;

    public Transform lazerGun;
    public Transform lazerGunLeft;
    public Transform lazerGunRight;
    public GameObject lazerShot;
    public GameObject playerShield;
    public GameObject playerExplosion;
    public GameObject asteroidExplosion;
    public float shotDelay;

    private float nextShot;
    private float nextShotLittle;
    private int cntShields;
    private Rigidbody playerShip;
    private GameControllerScript gameController;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        playerShip = GetComponent<Rigidbody>();
        playerShield.SetActive(false);
        cntShields = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        playerShip.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;

        playerShip.rotation = Quaternion.Euler(tilt * playerShip.velocity.z, 0, -tilt * playerShip.velocity.x);

        float newXposition = Mathf.Clamp(playerShip.position.x, xMin, xMax);
        float newZposition = Mathf.Clamp(playerShip.position.z, zMin, zMax);

        playerShip.position = new Vector3(newXposition, 0, newZposition);

        if (Time.time > nextShot && Input.GetButton("Fire1"))
        {
            Instantiate(lazerShot, lazerGun.position, Quaternion.identity);
            nextShot = Time.time + shotDelay;
        }
        if (Time.time > nextShotLittle && Input.GetButton("Fire2"))
        {
            GameObject newLazerShot = Instantiate(lazerShot, lazerGunLeft.position, Quaternion.Euler(0,-45,0));
            newLazerShot.transform.localScale *= 0.6f;
            newLazerShot = Instantiate(lazerShot, lazerGunRight.position, Quaternion.Euler(0, 45, 0));
            newLazerShot.transform.localScale *= 0.6f;
            nextShotLittle = Time.time + shotDelay / 2;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameController.PauseGame();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameBoundary" || other.tag == "PlayerShot")
        {
            return;
        } 
        else if (other.tag == "Shield")
        {
            if (cntShields < maxCountShields)
            {
                cntShields++;
                gameController.countShields(cntShields);
                playerShield.SetActive(true);
            }

            Destroy(other.gameObject);
            return;
        }
        else if (other.tag == "Enemy")
        {
            gameController.incrementScore(50);
            Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
        }
        else if (other.tag == "Asteroid")
        {
            gameController.incrementScore(10);
            Instantiate(asteroidExplosion, other.transform.position, Quaternion.identity);
        }

        Destroy(other.gameObject);

        if (cntShields > 0)
        {
            cntShields--;
            gameController.countShields(cntShields);
            if (cntShields == 0)
            {
                playerShield.SetActive(false);
            }
        } else
        {
            Destroy(gameObject);
            Instantiate(playerExplosion, transform.position, Quaternion.identity);
            gameController.GameOver();
        }
    }
}
