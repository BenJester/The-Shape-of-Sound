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

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        
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
