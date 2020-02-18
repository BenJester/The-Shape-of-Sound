using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public Transform start;
    public Transform finish;
    public KeyCode key;
    public AudioClip nextClip;
    public Instrument nextInstrument;

    public void Play()
    {
        nextInstrument.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
