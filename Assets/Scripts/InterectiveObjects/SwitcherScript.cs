using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherScript : MonoBehaviour
{
    [SerializeField] private Animator switcherAnimator;
    [SerializeField] private AudioSource switcheSound;
    [SerializeField] private List<InterectiveDoorsScript> m_closeDoorsList = new List<InterectiveDoorsScript>();
    [SerializeField] private List<InterectiveDoorsScript> m_openDoorsList = new List<InterectiveDoorsScript>();
    [SerializeField] private bool m_isInTrigger = false;
    [SerializeField] private float m_activateCooldown = 0.5f;
    [SerializeField] private bool m_isActivated = false;
    private bool m_canActivate = true;

    private void Start()
    {
        /*foreach (var openDoors in m_openDoorsList)
        {
            openDoors.SetBool("Open", true);
        }
            
        foreach (var closeDoors in m_closeDoorsList)
        {
            closeDoors.SetBool("Open", false);
        }*/
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

    void InterectWithSwitch()
    {
        m_isActivated = !m_isActivated;
        switcherAnimator.SetBool("Activate", m_isActivated);
        switcheSound.Play();
        StartCoroutine(SwitcherCooldown());
        if (m_isActivated)
        {
            foreach (var openDoors in m_openDoorsList)
            {
                openDoors.InterectDoor(false);
            }
            
            foreach (var closeDoors in m_closeDoorsList)
            {
                closeDoors.InterectDoor(true);
            }
        }
        else
        {
            foreach (var openDoors in m_openDoorsList)
            {
                openDoors.InterectDoor(true);
            }
            
            foreach (var closeDoors in m_closeDoorsList)
            {
                closeDoors.InterectDoor(false);
            }
        }
    }

    IEnumerator SwitcherCooldown()
    {
        m_canActivate = false;
        yield return new WaitForSeconds(m_activateCooldown);
        m_canActivate = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && m_isInTrigger && m_canActivate)
        {
            InterectWithSwitch();
        }
    }
}
