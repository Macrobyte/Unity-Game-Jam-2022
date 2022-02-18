using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    [SerializeField] KeyCode keyToPress;
    [SerializeField] bool canBePressed;

    private void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                gameObject.SetActive(false);
            }
            else
            {

            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Activator"))
        {
            canBePressed = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Activator"))
        {
            canBePressed = false;
        }
    }
}
