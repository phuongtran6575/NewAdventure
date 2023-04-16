using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCheckPoint : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("start", true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("start", false);
        }
    }
}
