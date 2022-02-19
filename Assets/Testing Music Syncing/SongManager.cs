using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    [Header("Song Information")]
    [SerializeField] float bpm;
    [SerializeField] float[] notes;

    [Header("Position Tracking")]
    [SerializeField] float songPosition;
    [SerializeField] float songPosInBeats;
    [SerializeField] float secPerBeat;
    [SerializeField] float dsptimesong;

    [SerializeField] int nextIndex = 0;

    void Start()
    {
        secPerBeat = 60f / bpm;

        dsptimesong = (float)AudioSettings.dspTime;

        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dsptimesong);

        songPosInBeats = songPosition / secPerBeat;

        if(nextIndex < notes.Length && notes[nextIndex] < songPosInBeats)
        {
            Debug.Log("Spawn Note");

            nextIndex++;
        }
    }
}
