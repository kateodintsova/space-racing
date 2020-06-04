using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public float speed;
    Vector3 startPosition;
    private GameControllerScript gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.gameIsActive)
        {
            float move = Mathf.Repeat(Time.time * speed, 100);
            transform.position = startPosition + Vector3.back * move;
        }
    }
}
