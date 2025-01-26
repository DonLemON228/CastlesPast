using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterectText : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            animator.SetBool("Appear", true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            animator.SetBool("Appear", false);
    }
}
