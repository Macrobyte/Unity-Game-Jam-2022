using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] float fallSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D col;
    [SerializeField] bool controlMovement = true;

    float timer = 0;

    private void Start()
    {
        timer = 2;
    }

    void Update()
    {
        if(controlMovement) transform.Translate(-transform.up * fallSpeed * Time.deltaTime);

        if (timer <= 0)
        {
            TogglePhysics(true);
        }
        else timer -= Time.deltaTime;
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
