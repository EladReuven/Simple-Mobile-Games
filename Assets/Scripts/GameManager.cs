using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static int currentID = 0;

    [SerializeField] GameObject[] balls;
    [SerializeField] Transform SpawnLocation;
    GameObject currentBall;
    bool ballIsChosen = false;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (!ballIsChosen)
        {
            currentBall = balls[Random.Range(0, balls.Length)];
            ballIsChosen = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 tmpV3Pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, SpawnLocation.position.y, SpawnLocation.position.z);
            GameObject newBall = Instantiate(currentBall, tmpV3Pos, currentBall.transform.rotation);
            ActivateBall(newBall);
            ballIsChosen = false;
        }
    }

    public GameObject GetBall(int index)
    {
        return balls[index];
    }

    public void ActivateBall(GameObject ball)
    {
        ball.GetComponent<CircleCollider2D>().enabled = true;
        ball.GetComponent<Rigidbody2D>().simulated = true;
    }

    public int BallsListLength()
    {
        return balls.Length;
    }
}
