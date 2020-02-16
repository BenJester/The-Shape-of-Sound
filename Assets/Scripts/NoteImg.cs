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

    void Start()
    {
        start = transform.position;
    }

    public void Init(Vector3 finish, float delay)
    {
        this.finish = finish;
        this.delay = delay;
        speed = (finish - transform.position).magnitude / delay; 
    }

    void Update()
    {
        transform.position += speed * Time.deltaTime * (finish - start).normalized;
    }
}
