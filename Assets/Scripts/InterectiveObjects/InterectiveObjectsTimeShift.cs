using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterectiveObjectsTimeShift : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite oldSprite;
    [SerializeField] Sprite newSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TimeShift(bool _isOld)
    {
        if (!_isOld)
            spriteRenderer.sprite = oldSprite;
        else
            spriteRenderer.sprite = newSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
