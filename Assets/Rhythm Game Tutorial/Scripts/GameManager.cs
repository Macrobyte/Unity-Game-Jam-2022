using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] AudioSource music;
    [SerializeField] bool startPlaying;
    [SerializeField] BeatScroller beatScroller;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
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
