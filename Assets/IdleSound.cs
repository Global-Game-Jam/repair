using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSound : MonoBehaviour
{

    public float minDelay = 1.0f; // in seconds
    public float random = 5.0f; // in seconds
    private float _timer = 0.0f;

    private float _delay = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        _delay = minDelay + Random.Range(0.0f, random);
    }

    // Update is called once per frame
    void Update()
    {
         if (_timer >= 0.0f) {
            _timer += Time.deltaTime;
            if (_timer >= _delay) {
                GetComponent<AudioSource>().Play();
                
                float clipLen = GetComponent<AudioSource>().clip.length;
                _delay =  clipLen + minDelay + Random.Range(0.0f, random);
            }
         }
    }

}
