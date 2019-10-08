using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance { get; private set; }

    public GameObject A3;
    public GameObject D3;
    public GameObject E3;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    public void PlayNote(int note)
    {
        GameObject noteObject = null;
        switch (note)
        {
            case 3:
                noteObject = Instantiate(E3);
                break;
            case 2:
                noteObject = Instantiate(D3);
                break;
            case 6:
                noteObject = Instantiate(A3);
                break;
        }
        noteObject.transform.parent = transform;
    }
}
