using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Song : MonoBehaviour
{
    public float bpm;
    public int trackNum;
    public List<string> rawSheet;
    public List<int> trackSheet; //单轨后谱面
    public List<AudioClip> notes;
    List<List<int>> sheet;
    public float currTime;
    public int index;
    public List<Track> tracks;
    public Track trackModeTrack;
    public GameObject notePic;
    public GameObject start;
    public GameObject finish;
    public float displayEarlyTime;
    float interval;
    public float tolerance;
    AudioSource source;
    public bool trackMode;
    public bool clear = true;

    void Start()
    {
        interval = 60f / (bpm * 2);
        source = GetComponent<AudioSource>();
        ProcessSheet();
    }

    void ProcessTrackSheet()
    {

    }

    void Play()
    {
        if (!trackMode)
        {
            foreach (var track in tracks)
            {
                if (Input.GetKeyDown(track.key))
                    source.PlayOneShot(track.nextClip);
            }
        }
        else
        {
            StartCoroutine(AutoPlay(index));
        }

    }

    IEnumerator AutoPlay(int index)
    {
        //float timer = 0f;
        int currIndex = index;
        while (currIndex < trackSheet.Count && trackSheet[currIndex] != 0)
        {
            for (int i = 0; i < sheet.Count; i++)
            {
                int instrument = sheet[i][currIndex] - 1;
                source.PlayOneShot(FreePlayer.Instance.sources[instrument]);
            }
            currIndex += 1;
            yield return new WaitForSeconds(interval);
        }
    }

    void ProcessSheet()
    {
        sheet = new List<List<int>>();
        for (int i = 0; i < rawSheet[0].Length; i++)
        {
            sheet.Add(new List<int>());
        }

        for (int i = 0; i < rawSheet.Count; i++)
        {
            char[] beat = rawSheet[i].ToCharArray();
            for (int j = 0; j < beat.Length; j++)
            {
                int noteInt = Int32.Parse(beat[j].ToString());
                sheet[j].Add(noteInt);
            }
        }
    }

    void Update()
    {
        currTime += Time.deltaTime;
        if (currTime >= index * interval)
        {
            if (!trackMode)
                DisplayNext();
            else
                DisplayNextTrackMode();
        }
    }

    void DisplayNextTrackMode()
    {
        if (index >= trackSheet.Count) return;

        if (trackSheet[index] != 0)
        {
            GameObject notePicObj = Instantiate(notePic, trackModeTrack.start.position, Quaternion.identity, transform);
            notePicObj.GetComponent<NoteImg>().Init(this, trackModeTrack.finish.position, displayEarlyTime, trackModeTrack.key, tolerance);
        }
        index += 1;
    }

    IEnumerator ChangeClip(Track track, AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        track.nextClip = clip;
    }

    IEnumerator Check()
    {
        yield return new WaitForSeconds(displayEarlyTime + Time.deltaTime);
        if (clear) trackMode = true;
        index = 0;
        currTime = 0f;
    }

    void DisplayNext()
    {
        if (index >= sheet[0].Count)
        {
            index = 0;
            currTime = 0f;
            StartCoroutine(Check());
            return;
        }

            
        for (int i = 0; i < sheet.Count; i++)
        {
            if (sheet[i][index] != 0)
            {
                GameObject notePicObj = Instantiate(notePic, tracks[i].start.position, Quaternion.identity, transform);
                notePicObj.GetComponent<NoteImg>().Init(this, tracks[i].finish.position, displayEarlyTime, tracks[i].key, tolerance);
                StartCoroutine(ChangeClip(tracks[i], FreePlayer.Instance.FindClip(sheet[i][index]), displayEarlyTime));
            }
        }

            
        index += 1;
    }
}
