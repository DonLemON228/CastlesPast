using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrailScript : MonoBehaviour
{
    public GameObject trailPref;
    public float delay = 1.0f;
    private float delta = 0;
    
    public GameObject obj;
    public Image imageComponent;
    public Image imageComponentObj;
    public float destroyTime = 0.1f;
    public Color color;
    public Material material = null;
    public float fadeSpeed = 0.5f; // Adjust this to control the speed of fading

    void Update()
    {
        if (delta > 0)
        {
            delta -= Time.deltaTime;
        }
        else
        {
            delta = delay;
            CreateTrail();
        }
    }

    void CreateTrail()
    {
        // Instantiate the trail object
        GameObject trailObj = Instantiate(trailPref, transform.position, transform.rotation);
        trailObj.transform.parent = transform;
        trailObj.transform.localScale = obj.transform.localScale;
        Destroy(trailObj, destroyTime);

        // Get the Image component and copy properties
        imageComponent = trailObj.GetComponent<Image>();
        imageComponent.sprite = imageComponentObj.sprite;
        imageComponent.color = color;

        if (material != null)
        {
            imageComponent.material = material;
        }

        // Start fading the trail
        StartCoroutine(FadeTrail(trailObj));
    }

    IEnumerator FadeTrail(GameObject trail)
    {
        Image trailImageComponent = trail.GetComponent<Image>();
        Color trailColor = trailImageComponent.color;

        float elapsedTime = 0f;
        while (elapsedTime < destroyTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            trailColor.a = Mathf.Lerp(1f, 0f, elapsedTime / destroyTime);
            if (trailImageComponent != null)
            {
                trailImageComponent.color = trailColor;
            }
        }

        // Ensure the trail is fully faded
        trailColor.a = 0f;
        if (trailImageComponent != null)
        {
            trailImageComponent.color = trailColor;
        }
    }
}
