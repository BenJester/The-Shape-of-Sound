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
    public float speed;
    public float currTime;
    public int index;
    public int playIndex;
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
    public int cycleNum;
    public int currCycle;

    void Awake()
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
            if (Input.GetKeyDown(trackModeTrack.key))
                StartCoroutine(AutoPlay(playIndex));
        }

    }

    IEnumerator AutoPlay(int index)
    {
        //float timer = 0f;
        int currIndex = index;
        while (currIndex < trackSheet.Count)
        {
            for (int i = 0; i < sheet.Count; i++)
            {
                int instrument = sheet[i][currIndex];
                source.PlayOneShot(FreePlayer.Instance.FindClip(instrument));
                Debug.Log(sheet[i][currIndex]);
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
        //Play();
        currTime += Time.deltaTime;
        if (currTime >= index * interval)
        {
            //if (!trackMode)
                //DisplayNext();
            //else
                //DisplayNextTrackMode();
        }
    }


    IEnumerator ChangeClip(Track track, AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        track.nextClip = clip;
        
    }

    public void DisplayTrack(float delay)
    {
        for (int k = 0; k < cycleNum; k ++)
        {
            for (int i = 0; i < sheet.Count; i++)
            {
                for (int j = 0; j < sheet[0].Count; j++)
                {
                    if (sheet[i][j] != 0)
                    {
                        float waitTime = interval * (j + k * sheet[0].Count) + delay;
                        GameObject notePicObj = Instantiate(notePic, tracks[i].finish.position + new Vector3(0f, speed * waitTime), Quaternion.identity, transform);
                        notePicObj.GetComponent<NoteImg>().Init(this, tracks[i].finish.position, waitTime, tracks[i].key, tolerance);
                    }
                }
            }
        } 
    }

}
