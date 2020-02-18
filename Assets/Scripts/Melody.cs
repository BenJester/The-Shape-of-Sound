using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melody : MonoBehaviour
{
    public float bpm;
    public List<Song> songs;
    public float delay;
    public List<Track> tracks;
    public List<Instrument> instruments;
    public int index;
    public float currTime;
    public float interval;
    public int checkCycleNum;
    public bool clear = true;

    public static Melody Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
    
    void Start()
    {
        interval = 60f / (bpm * 2);
        songs[0].DisplayTrack(delay);
    }

    void Play()
    {
        foreach (var track in tracks)
        {
            if (Input.GetKeyDown(track.key))
                track.Play();
        }
    }

    void Update()
    {
        currTime += Time.deltaTime;
        if (currTime >= interval * checkCycleNum * songs[index].rawSheet.Count + delay)
        {
            if (clear)
            {
                index += 1;
            }
            else
            {
                clear = true;
                currTime = 0f;
            }
            songs[index].DisplayTrack(songs[index].rawSheet.Count * interval);
        }
    }
}
