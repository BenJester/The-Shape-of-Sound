using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongClip : MonoBehaviour
{
    public List<int> notes;
    public int index;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            PlayNext();
        }
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }

    void PlayNext()
    {
        if (index >= notes.Count) index = 0;
        Audio.Instance.PlayNote(notes[index]);
        index += 1;
    }
}
