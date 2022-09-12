using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int state; //states from 0 - smallest, 1 - medium, 2 - large, 3 - XLarge
    public int ID;
    public bool currentBall;
    Ball otherBall;

    private void Start()
    {
        SetID(GameManager.currentID);
        GameManager.currentID++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        otherBall = collision.gameObject.GetComponent<Ball>();

        //if collision isnt a ball or a ball from a different state then return
        if (otherBall == null || otherBall.state != this.state)
        {
            return;
        }

        Debug.Log(ID);

        //return if not smaller id ball
        if(this.ID < otherBall.ID)
        {
            if (this.state + 1 < GameManager.instance.BallsListLength())
            {
                Debug.Log("Evolved");
                this.Evolve();
            }
        }

        Destroy(this.gameObject);
    }

    //create a new ball in next state in position of ball that was created first
    public void Evolve()
    {
        GameObject newBall;
        newBall = GameManager.instance.GetBall(this.state + 1);
        newBall.transform.position = this.transform.position;
        GameManager.instance.ActivateBall(newBall);
        Instantiate(newBall);
    }

    public void SetID(int ID)
    {
        this.ID = ID;
    }
}
