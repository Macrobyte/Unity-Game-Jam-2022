using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    float speed;
    bool hit = false;
    bool controlMovement = true;   
    Rigidbody2D rb;
    Collider2D col;
    Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        ResetProperties();
    }

    private void Update()
    {
        if (controlMovement && !GameManager.Instance.IsGameOver()) Move();
    }

    public void SetUp(Vector3 startPos, float newSpeed)
    {
        gameObject.transform.position = startPos;
        speed = newSpeed;
    }

    public void Move()
    {
        transform.Translate(-transform.up * speed * Time.deltaTime);
    }

    public void NoteHit()
    {
        hit = true;

        controlMovement = false;

        if ( Mathf.Abs(transform.position.y) > 0.25)
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

        anim.SetBool("Suck", true);

        Invoke(nameof(DisableObject), 1f);
    }

    public void NoteMissed()
    {
        GameManager.Instance.NoteMissed();
        GameManager.Instance.MissEffectPool.SpawnObject(transform, Quaternion.identity, false);

        //TogglePhysics(true);
        Invoke(nameof(DisableObject), 3f);
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

    public void DisableObject()
    {
        gameObject.GetComponent<PooledObject>().ReturnToPool();
    }

    public void ResetProperties()
    {
        hit = false;
        anim.SetBool("Suck", false);
        TogglePhysics(false);
        controlMovement = true;
    }

    public bool Hit() => hit;
}
