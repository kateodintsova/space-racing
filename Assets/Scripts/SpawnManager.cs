using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Variables
    public GameObject shield;
    public GameObject[] asteroids;
    public GameObject enemy;

    private float xSpawnPos = 32.0f;
    private float xSpawnEnemyPos = 20.0f;
    private float zSpawnPos = 48.0f;
    private float ySpawnPos = 0f;

    private float spawnShieldRate = 5.0f;
    private float spawnMinRangeAsteroid = 1.0f;
    private float spawnMaxRangeAsteroid = 2.0f;
    private float spawnAsteroidRate;
    private float spawnEnemyRate = 4.0f;

    private GameControllerScript gameController;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        StartCoroutine(SpawnShield());
        StartCoroutine(SpawnAsteroid());
        StartCoroutine(SpawnEnemy());
    }

    private Vector3 getRandomPosition(float xPos)
    {
        float xRandPos = Random.Range(-xPos, xPos);
        return new Vector3(xRandPos, ySpawnPos, zSpawnPos);
    }

    #region Coroutines
    private IEnumerator SpawnShield()
    {
        while (gameController.gameIsActive)
        {
            yield return new WaitForSeconds(spawnShieldRate);

            // Создаем щит
            Instantiate(shield, getRandomPosition(xSpawnPos), shield.transform.rotation);
        }
    }

    private IEnumerator SpawnAsteroid()
    {
        while (gameController.gameIsActive)
        {
            spawnAsteroidRate = Random.Range(spawnMinRangeAsteroid, spawnMaxRangeAsteroid);
            yield return new WaitForSeconds(spawnAsteroidRate);
            
            // Создаем астероид
            int indexAsteroid = Random.Range(0, asteroids.Length);
            GameObject newAsteroid = Instantiate(asteroids[indexAsteroid], getRandomPosition(xSpawnPos), asteroids[indexAsteroid].transform.rotation);

            // Зададим случайным образом размер
            float resize = Random.Range(0.7f, 1.5f);
            newAsteroid.transform.localScale *= resize;
        }
    }

    private IEnumerator SpawnEnemy()
    {
        while (gameController.gameIsActive)
        {
            yield return new WaitForSeconds(spawnEnemyRate);

            Instantiate(enemy, getRandomPosition(xSpawnEnemyPos), enemy.transform.rotation);
        }
    }
    #endregion
}
