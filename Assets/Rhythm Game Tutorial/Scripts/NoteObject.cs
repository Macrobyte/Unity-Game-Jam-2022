using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    bool controlMovement = true;   
    Rigidbody2D rb;
    Collider2D col;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (controlMovement) Move();
    }

    public void Move()
    {
        transform.Translate(-transform.up * GameManager.Instance.FallSpeed() * Time.deltaTime);
    }

    public void NoteHit()
    {
        if( Mathf.Abs(transform.position.y) > 0.25)
        {
            GameManager.Instance.NoteHit(GameManager.Hit.Normal);
            GameManager.Instance.hitEffectPooler.SpawnObject(transform, Quaternion.identity, false);
        }
        else if (Mathf.Abs(transform.position.y) > 0.05)
        {
            GameManager.Instance.NoteHit(GameManager.Hit.Good);
            GameManager.Instance.GoodEffectBool.SpawnObject(transform, Quaternion.identity, false);
        }
        else
        {
            GameManager.Instance.NoteHit(GameManager.Hit.Perfect);
            GameManager.Instance.PefectEffectPool.SpawnObject(transform, Quaternion.identity, false);
        }
        gameObject.SetActive(false);
    }

    public void NoteMissed()
    {
        GameManager.Instance.NoteMissed();
        GameManager.Instance.MissEffectPool.SpawnObject(transform, Quaternion.identity, false);

        TogglePhysics(true);
    }

    public void TogglePhysics(bool state)
    {
        if (state == true)
        {
            controlMovement = false;
            col.isTrigger = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            col.isTrigger = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}
