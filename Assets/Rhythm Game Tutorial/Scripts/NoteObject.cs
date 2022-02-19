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
            if(canBePressed) NoteHit();
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
            if(gameObject.activeInHierarchy) NoteMissed();
        }
    }

    public void NoteHit()
    {

        if( Mathf.Abs(transform.position.y) > 0.25)
        {
            Debug.Log("Hit");
            GameManager.Instance.NoteHit(GameManager.Hit.Normal);
            GameManager.Instance.hitEffectPooler.SpawnObject(transform, Quaternion.identity, false);
        }
        else if (Mathf.Abs(transform.position.y) > 0.05)
        {
            Debug.Log("Good Hit");
            GameManager.Instance.NoteHit(GameManager.Hit.Good);
            GameManager.Instance.GoodEffectBool.SpawnObject(transform, Quaternion.identity, false);
        }
        else
        {
            Debug.Log("Perfect Hit");
            GameManager.Instance.NoteHit(GameManager.Hit.Perfect);
            GameManager.Instance.PefectEffectPool.SpawnObject(transform, Quaternion.identity, false);
        }
        gameObject.SetActive(false);
    }

    public void NoteMissed()
    {
        GameManager.Instance.NoteMissed();
        GameManager.Instance.MissEffectPool.SpawnObject(transform, Quaternion.identity, false);
    }
}
