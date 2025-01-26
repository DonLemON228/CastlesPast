using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField] private float kayotTime = 0.1f;
    public bool isGroundedKayot;
    public bool isGroundedReal;

    private float kayotTimer;

    private void Start()
    {
        isGroundedReal = true;
        isGroundedKayot = true;
        kayotTimer = 0f;
    }

    private void Update()
    {
        if (!isGroundedReal)
        {
            kayotTimer += Time.deltaTime;
            if (kayotTimer >= kayotTime)
            {
                isGroundedKayot = false;
            }
        }
        else
        {
            kayotTimer = 0f;
            isGroundedKayot = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(!other.CompareTag("InvisibleTrigger") && !other.CompareTag("Fance"))
            isGroundedReal = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("InvisibleTrigger") && !other.CompareTag("Fance"))
            isGroundedReal = false;
    }
}
