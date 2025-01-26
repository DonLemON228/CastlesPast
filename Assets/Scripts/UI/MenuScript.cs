using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private int m_sceneTeleportNumber;
    [SerializeField] private float m_timeBeforeTeleport;
    [SerializeField] private Animator m_fadeAnimator;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void SceneTelport()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        m_fadeAnimator.SetTrigger("Fade");
        yield return new WaitForSeconds(m_timeBeforeTeleport);
        SceneManager.LoadScene(m_sceneTeleportNumber);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
