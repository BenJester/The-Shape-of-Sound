using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreePlayer : MonoBehaviour
{
    public AudioSource source;
    public AudioClip highHat;
    public AudioClip kick;
    public AudioClip tom;
    public AudioClip highTone;
    public AudioClip lowTone;
    public AudioClip swish;
    public List<AudioClip> sources;
    public static FreePlayer Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        source = GetComponent<AudioSource>();
    }

    public AudioClip FindClip(int index)
    {
        return sources[index];
    }

    private void Update()
    {
        return;
        if (Input.GetKeyDown(KeyCode.A))
            source.PlayOneShot(kick);

        if (Input.GetKeyDown(KeyCode.S))
            source.PlayOneShot(tom);

        if (Input.GetKeyDown(KeyCode.D))
            source.PlayOneShot(highHat);

        if (Input.GetKeyDown(KeyCode.J))
            source.PlayOneShot(highTone);

        if (Input.GetKeyDown(KeyCode.K))
            source.PlayOneShot(lowTone);

        if (Input.GetKeyDown(KeyCode.L))
            source.PlayOneShot(swish);
    }
}
