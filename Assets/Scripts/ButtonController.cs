using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] KeyCode keyToPress;

    bool canBePressed;
    NoteObject currentNote;

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            door.SetActive(false);

            if (canBePressed)
            {
                canBePressed = false;
                currentNote.NoteHit();
                currentNote = null;
            }
            else
                GameManager.Instance.MissClick();
        }

        if (Input.GetKeyUp(keyToPress))
        {
            door.SetActive(true);
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
