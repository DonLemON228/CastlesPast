using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeShiftUnlock : MonoBehaviour
{
    [SerializeField] GameObject clockSprite;
    [SerializeField] TimeShiftScript timeShiftScript;
    [SerializeField] BoxCollider2D m_triggerCollider;
    [SerializeField] Animator m_interectAnimator;
    [SerializeField] Animator m_tutorialAnimator;
    private bool m_isInTrigger = false;
    private bool m_isActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_isInTrigger = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_isInTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && m_isInTrigger && !m_isActivated)
        {
            m_isActivated = true;
            m_triggerCollider.enabled = false;
            m_interectAnimator.SetBool("Appear", false);
            m_tutorialAnimator.SetTrigger("Appear");
            clockSprite.SetActive(false);
            timeShiftScript.m_camUseTimeShift = true;
        }
    }
}
