using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapAnim : MonoBehaviour
{

    public GameObject frame1;
    public GameObject frame2;

    private float _swapDelay = 0.3f; // in seconds
    private float _timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (_timer >= 0.0f) {
            _timer += Time.deltaTime;
            if (_timer >= _swapDelay) {
                _timer = 0.0f;
                frame1.SetActive( ! frame1.activeSelf);
                frame2.SetActive( ! frame2.activeSelf);
                 Debug.Log("swap mesh");
            }
        }
    }
}
