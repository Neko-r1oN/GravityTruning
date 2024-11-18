using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SAGoal : MonoBehaviour
{
    public ScoreManager scoreManager;
    public bool hitFlag;

    public float posX;
    public float posY;

    private float posZ = 0;

    private float posX1 = 1.0f;
    private float posX2 = 0.5f;
    private float posX3 = 0.0f;
    private float posX4 = -0.5f;
    private float posX5 = -1.0f;

    private float posY1 = 1.21f;
    private float posY2 = 0.59f;
    private float posY3 = 0.0f;
    private float posY4 = -0.57f;
    private float posY5 = -1.22f;

    private int randX;
    private int randY;

    // Use this for initialization
    void Start()
    {
        randX = 0;
        randY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(hitFlag == true)
        {
            scoreManager.AddScore();
            hitFlag = false;
        }
    }

    private void RandumX()
    {
        System.Random randX = new System.Random();
        int randNum = randX.Next(1, 6);

        switch (randNum)
        {
            case 1:
                posX = posX1;
                //Debug.Log("X1");
                break;
            case 2:
                posX = posX2;
                //Debug.Log("X2");
                break;
            case 3:
                posX = posX3;
                //Debug.Log("X3");
                break;
            case 4:
                posX = posX4;
                //Debug.Log("X4");
                break;
            case 5:
                posX = posX5;
                //Debug.Log("X5");
                break;
        }
        Debug.Log("x:"+randNum);
        return;
    }

    private void RandumY()
    {
        System.Random randX = new System.Random();
        int randNum = randX.Next(1, 6);

        switch (randNum)
        {
            case 1:
                posY = posY1;
                //Debug.Log("Y1");
                break;
            case 2:
                posY = posY2;
                //Debug.Log("Y2");
                break;
            case 3:
                posY = posY3;
                //Debug.Log("Y3");
                break;
            case 4:
                posY = posY4;
                //Debug.Log("Y4");
                break;
            case 5:
                posY = posY5;
                //Debug.Log("Y5");
                break;
        }
        Debug.Log("y:" + randNum);
        return;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))//さっきつけたTagutukeruというタグがあるオブジェクト限定で～という条件の下
        {
            RandumX();
            RandumY();

            collision.gameObject.transform.position = new Vector3(posX, posY, posZ);
            //落ちないように重力を0にする
           
            scoreManager.AddScore();
        }
    }

}
