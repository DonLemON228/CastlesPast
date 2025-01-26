using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportCollider : MonoBehaviour
{
    [SerializeField] private int m_nexSceneNumber;
    [SerializeField] private Animator m_fadeAnimator;
    [SerializeField] private float m_timeBeforeTP;
    private PlayerMove m_playerMove;
    
    void Start()
    {
        m_playerMove = FindObjectOfType<PlayerMove>();
    }
    
    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(m_timeBeforeTP);
        SceneManager.LoadScene(m_nexSceneNumber);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_playerMove.enabled = false;
            m_fadeAnimator.SetTrigger("Fade");
            StartCoroutine(FadeIn());
        }
    }
}
