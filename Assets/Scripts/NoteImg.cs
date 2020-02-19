using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteImg : MonoBehaviour
{
    Vector3 finish;
    float delay;
    Vector3 start;
    public float currTime;
    float speed;
    public KeyCode key;
    float tolerance;
    Song song;
    bool triggered;
    public bool check;

    void Start()
    {
        start = transform.position;
    }

    public void Init(Song song, Vector3 finish, float delay, KeyCode key, float tolerance, bool check)
    {
        this.finish = finish;
        this.delay = delay;
        this.key = key;
        this.song = song;
        this.tolerance = tolerance;
        speed = song.speed;
        currTime = delay;
        this.check = check;
    }

    void Update()
    {
        transform.position += speed * Time.deltaTime * (finish - start).normalized;
        currTime -= Time.deltaTime;
        if (triggered) return;
        if (Mathf.Abs(currTime) <= tolerance && Input.GetKeyDown(key))
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            triggered = true;
        }
        else if (currTime < - tolerance && check)
        {
            Melody.Instance.clear = false;
            triggered = true;
        }
    }
}
