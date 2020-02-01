using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LooseLevel : MonoBehaviour
{
    public Image Image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color color = Image.color;
        if (color.a < 0.5f)
        {
            color.a += Time.deltaTime * 0.2f;
            Image.color = color;
        }
        else
        {
            Debug.Log("Start Clicked");
            SceneManager.LoadScene("Game/Scenes/UI/Start");
        }
    }
}
