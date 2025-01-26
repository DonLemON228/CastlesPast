using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherDisable : MonoBehaviour
{
    [SerializeField] List<GameObject> disableObjects = new List<GameObject>();
    [SerializeField] BoxCollider2D interectCollider;
    [SerializeField] Animator interectText;
    // Start is called before the first frame update
    void Start()
    {
        SwitcherOff();
    }

    public void SwitcherOff()
    {
        if (disableObjects != null)
        {
            foreach (GameObject obj in disableObjects)
            {
                obj.SetActive(false);
            }
        }
        interectCollider.enabled = false;
        interectText.SetBool("Appear", false);
    }
    
    public void SwitcherOn()
    {
        if (disableObjects != null)
        {
            foreach (GameObject obj in disableObjects)
            {
                obj.SetActive(true);
            }
        }
        interectCollider.enabled = true;
        //interectText.SetBool("Appear", true);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
