using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] Sprite defaultImage;
    [SerializeField] Sprite pressedImage;
    [SerializeField] KeyCode keyToPress;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            spriteRenderer.sprite = pressedImage;
        }

        if (Input.GetKeyUp(keyToPress))
        {
            spriteRenderer.sprite = defaultImage;
        }
    }
}
