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
    //public Track trackModeTrack;
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

    }


    IEnumerator ChangeClip(Track track, Instrument clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        track.nextInstrument = clip;
    }

    public void DisplayTrack(float delay)
    {
        // find first instrument
        for (int i = 0; i < sheet.Count; i++)
        {
            for (int j = 0; j < sheet[0].Count; j++)
            {
                if (sheet[i][j] != 0)
                {
                    StartCoroutine(ChangeClip(tracks[i], Melody.Instance.instruments[sheet[i][j]], 0f));
                    break;
                }
            }
        }

        for (int k = 0; k < Melody.Instance.cycleNum; k ++)
        {
            for (int i = 0; i < sheet.Count; i++)
            {
                for (int j = 0; j < sheet[0].Count; j++)
                {
                    float waitTime = interval * (j + k * sheet[0].Count) + delay;
                    Debug.Log((j + k * sheet[0].Count));
                    if (sheet[i][j] != 0)
                    {
                        GameObject notePicObj = Instantiate(notePic, tracks[i].finish.position + new Vector3(0f, speed * waitTime), Quaternion.identity, transform);
                        notePicObj.GetComponent<NoteImg>().Init(this, tracks[i].finish.position, waitTime, tracks[i].key, tolerance, k < Melody.Instance.checkCycleNum ? true : false);
                        
                    }
                    if (j < sheet[0].Count - 1 && sheet[i][j + 1] != 0)
                    {
                        StartCoroutine(ChangeClip(tracks[i], Melody.Instance.instruments[sheet[i][j + 1]], waitTime + tolerance));
                    }
                }
            }
        } 
    }

}
