using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallColliderScript : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] private float timeBeforeTeleport;
    [SerializeField] private Animator fadeAnimator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TeleportPlayer());
        }
    }

    IEnumerator TeleportPlayer()
    {
        audioSource.Play();
        fadeAnimator.SetTrigger("Fade");
        yield return new WaitForSeconds(timeBeforeTeleport);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
