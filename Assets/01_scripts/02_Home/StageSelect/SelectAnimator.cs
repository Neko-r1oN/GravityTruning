using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAnimator : MonoBehaviour
{
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartMAnim()
    {     
        animator.SetBool("isSelect", true);
    }
    public void CloseAnim()
    {
        animator.SetBool("isSelect", false);
    }
}
