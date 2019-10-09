using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Note : MonoBehaviour
{
    AudioClip clip;
    float timer = 0f;
    public float len;

    void Start()
    {
        clip = GetComponent<AudioSource>().clip;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 8f) Destroy(gameObject);
    }
}
