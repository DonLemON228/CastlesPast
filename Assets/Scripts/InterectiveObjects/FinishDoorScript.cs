using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishDoorScript : MonoBehaviour
{
    [SerializeField] private int m_nexSceneNumber;
    [SerializeField] private Animator m_fadeAnimator;
    [SerializeField] private float m_timeBeforeTP;
    private PlayerMove m_playerMove;
    private bool m_isInTrigger = false;
    private bool m_isActivated = false;

    void Start()
    {
        m_playerMove = FindObjectOfType<PlayerMove>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            m_isInTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            m_isInTrigger = false;
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(m_timeBeforeTP);
        SceneManager.LoadScene(m_nexSceneNumber);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && m_isInTrigger && !m_isActivated)
        {
            m_isActivated = true;
            m_playerMove.speed = 0;
            m_fadeAnimator.SetTrigger("Fade");
            StartCoroutine(FadeIn());
        }
    }
}
