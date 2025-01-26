using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterectiveDoorsScript : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private AudioSource doorSound;
    [SerializeField] private bool isOpend = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //doorSound.Play();
        if (isOpend)
            doorAnimator.SetBool("Open", true);
        else
            doorAnimator.SetBool("Open", false);
    }

    public void InterectDoor(bool isOpen)
    {
        doorSound.Play();
        if(isOpen)
            doorAnimator.SetBool("Open", true);
        else
            doorAnimator.SetBool("Open", false);
    }
}
