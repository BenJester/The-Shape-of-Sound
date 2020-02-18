using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Instrument : MonoBehaviour
{
    // Start is called before the first frame update
    public bool useSheet;
    public List<string> rawSheet;
    List<List<int>> sheet;
    public AudioClip clip;
    AudioSource source;

    private void Start()
    {
        ProcessSheet();

    }

    public void Play()
    {
        if (!useSheet)
            source.PlayOneShot(clip);
        else
            StartCoroutine(AutoPlay(0));
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

    IEnumerator AutoPlay(int index)
    {
        //float timer = 0f;
        int currIndex = index;
        while (currIndex < rawSheet.Count)
        {
            for (int i = 0; i < sheet.Count; i++)
            {
                int instrument = sheet[i][currIndex];
                source.PlayOneShot(FreePlayer.Instance.FindClip(instrument));
                Debug.Log(sheet[i][currIndex]);
            }
            currIndex += 1;
            yield return new WaitForSeconds(Melody.Instance.interval);
        }
    }
}
