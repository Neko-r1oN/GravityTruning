using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HomeGoal : MonoBehaviour
{
    public bool HitFlag;

    public float posX;
    public float posY;

    private float[] pos1;
    private float[] pos2;
    private float[] pos3;
    private float[] pos4;

    int randLastNum = 0;

    // Use this for initialization
    void Start()
    {
        pos1 = new float[2] { 1.77f, 2.52f};
        pos2 = new float[2] { 1.04f, 0.30f };
        pos3 = new float[2] { -1.09f, 0.57f };
        pos4 = new float[2] { -1.55f, 2.80f };

        randLastNum = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if(HitFlag == true)
        {
            
            HitFlag = false;
        }
        

    }
    private void Randum()
    {
        System.Random rand = new System.Random();
        int randNum = rand.Next(1, 5);
        

        

        while(randNum != randLastNum)
        {
            rand = new System.Random();
            randLastNum = randNum;
        }

        switch (randNum)
        {
            case 1:
                posX = pos1[0];
                posY = pos1[1];
                //Debug.Log("X1");
                break;
            case 2:
                posX = pos2[0];
                posY = pos2[1];
                //Debug.Log("X2");
                break;
            case 3:
                posX = pos3[0];
                posY = pos3[1];
                //Debug.Log("X3");
                break;
            case 4:
                posX = pos4[0];
                posY = pos4[1];
                //Debug.Log("X4");
                break;
        }
        Debug.Log("x:"+ posX);
        Debug.Log("y:" + posY);
        return;
    }

   
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))//さっきつけたTagutukeruというタグがあるオブジェクト限定で～という条件の下
        {
            Randum();

            collision.gameObject.transform.position = new Vector3(posX, posY, 0.0f);
        }
    }

}
