using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] Sprite defaultImage;
    [SerializeField] Sprite pressedImage;
    [SerializeField] KeyCode keyToPress;

    bool canBePressed;
    NoteObject currentNote;

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

            if (canBePressed)
            {
                canBePressed = false;
                currentNote.NoteHit();
                currentNote = null;
            } 
        }

        if (Input.GetKeyUp(keyToPress))
        {
            spriteRenderer.sprite = defaultImage;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<NoteObject>())
        {
            currentNote = collision.GetComponent<NoteObject>();
            canBePressed = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (currentNote == null) return;

        if (collision.GetComponent<NoteObject>())
        {
            canBePressed = false;
            if (!currentNote.Hit()) currentNote.NoteMissed();
            currentNote = null;
        }
    }
}
