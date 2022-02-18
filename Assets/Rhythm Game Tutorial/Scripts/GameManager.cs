using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] bool startPlaying;
    [SerializeField] BeatScroller beatScroller;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                beatScroller.ToggleStart(true);
                music.Play();
            }
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit on Time");
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");
    }
}
