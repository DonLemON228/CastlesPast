using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandPlit : MonoBehaviour
{
    [SerializeField] private Animator landButtonAnimator;
    [SerializeField] AudioSource landButtonSound;
    [SerializeField] private List<InterectiveDoorsScript> m_closeDoorsList = new List<InterectiveDoorsScript>();
    [SerializeField] private List<InterectiveDoorsScript> m_openDoorsList = new List<InterectiveDoorsScript>();
    [SerializeField] Animator m_elevatorAnimator;
    private bool m_isActivated;
    
    /*private void Start()
    {
        foreach (var openDoors in m_openDoorsList)
        {
            openDoors.SetBool("Open", true);
        }
            
        foreach (var closeDoors in m_closeDoorsList)
        {
            closeDoors.SetBool("Open", false);
        }
    }*/
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Box"))
        {
            if (!m_isActivated)
            {
                m_isActivated = true;
                landButtonSound.Play();
                landButtonAnimator.SetBool("Activate", true);
                foreach (var openDoors in m_openDoorsList)
                {
                    openDoors.InterectDoor(false);
                }
            
                foreach (var closeDoors in m_closeDoorsList)
                {
                    closeDoors.InterectDoor(true);
                }
            }
        }
        
        if(m_elevatorAnimator != null)
            m_elevatorAnimator.enabled = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Box"))
        {
            if (m_isActivated)
            {
                m_isActivated = false;
                landButtonSound.Play();
                landButtonAnimator.SetBool("Activate", false);
                foreach (var openDoors in m_openDoorsList)
                {
                    openDoors.InterectDoor(true);
                }
            
                foreach (var closeDoors in m_closeDoorsList)
                {
                    closeDoors.InterectDoor(false);
                }
            
                if(m_elevatorAnimator != null)
                    m_elevatorAnimator.enabled = false;
            }
            
        }
    }
}
