using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    public float bpm;
    public List<int> sheet;
    public List<AudioClip> notes;

    public float currTime;
    public int index;

    public GameObject notePic;
    public GameObject start;
    public GameObject finish;
    public float displayEarlyTime;
    float interval;

    void Start()
    {
        interval = 60f / (bpm * 2);
    }

    
    void Update()
    {
        currTime += Time.deltaTime;
        if (currTime >= index * interval)
        {
            DisplayNext();
        }
    }

    void DisplayNext()
    {
        if (index >= sheet.Count) return;
        if (sheet[index] != 0)
        {
            GameObject notePicObj = Instantiate(notePic, start.transform.position, Quaternion.identity, transform);
            notePicObj.GetComponent<NoteImg>().Init(finish.transform.position, displayEarlyTime);
        }
        index += 1;
    }
}
