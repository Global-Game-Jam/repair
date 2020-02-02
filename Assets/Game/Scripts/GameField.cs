using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameField : MonoBehaviour
{    
    public GameObject Characters;
    public uint FailBeforeLoose = 1;
    public uint Couple2Pair = 1;

    public AudioSource ambient;

    public float pauseBeforeRestartLevel = 2.0f;



    private List<CharacterControler> _charactersFemale;
    private List<CharacterControler> _charactersMale;
    private Camera _cam;

    private GameObject _kissEffect;

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        _charactersFemale = new List<CharacterControler>();
        _charactersMale = new List<CharacterControler>();
        _kissEffect = Instantiate (Resources.Load<GameObject>("Prefabs/Effects/HeartParticlesInGameObject"));
        _kissEffect.SetActive(false);
        _kissEffect.transform.rotation = Quaternion.AngleAxis(45, Vector3.up);
        

        foreach (Transform t in Characters.transform)
        {
            CharacterControler characterCont = t.gameObject.GetComponent<CharacterControler>();
            if (characterCont) {
                if (characterCont.Sex == CharacterControler.SexEnum.Female) {
                    _charactersFemale.Add(characterCont);
                } else if (characterCont.Sex == CharacterControler.SexEnum.Male) {
                    _charactersMale.Add(characterCont);
                }
            }
        }

        Debug.Log("Found " + _charactersMale.Count + " male(s) and " + _charactersFemale.Count + " famale(s).");
        CalculateMaleTargets();
    }

    private void CalculateMaleTargets()
    {
        // find one of the closest famale and set it as target for male
        foreach(CharacterControler male in _charactersMale) {
            float minDist = float.MaxValue;
            CharacterControler closestFemale = null;
            foreach(CharacterControler female in _charactersFemale) {
                float dist = Vector3.Distance(male.transform.position, female.transform.position);
                if (dist < minDist) {
                    closestFemale = female;
                }
            }
            if (closestFemale != null) {
                male.SetDestination(closestFemale.gameObject.transform.position);
            }
        }
    }   

    public void OnTwoAnimalsWantToPlay(CharacterControler firstAnimal, CharacterControler secondAnimal)
    {
        if (Couple2Pair == 0 || FailBeforeLoose == 0)
            return;
        if (firstAnimal.AnimalType == secondAnimal.AnimalType &&
            firstAnimal.Sex != secondAnimal.Sex)
        {
            SpawnKiss(Vector3.Lerp(firstAnimal.transform.position, secondAnimal.transform.position, 0.5f));
            --Couple2Pair;
            if (Couple2Pair == 0)
            {
                Debug.Log("Win");
                SceneManager.LoadScene("Game/Scenes/UI/WinLevel", LoadSceneMode.Additive);
                // Stop all mobs here
            }
        }
        else
        {
            
            --FailBeforeLoose;
            if (FailBeforeLoose == 0)
            {
                Debug.Log("Loose");
                SceneManager.LoadScene("Game/Scenes/UI/LooseLevel", LoadSceneMode.Additive);
                // Stop all mobs here
            }
        }
        //Destroy(firstAnimal.gameObject);
        //Destroy(secondAnimal.gameObject);
    }

    private void SpawnKiss(Vector3 position)
    {
        GetComponent<AudioSource>().Play();
        ambient.Pause();
        _kissEffect.SetActive(true);
        _kissEffect.transform.position = position + new Vector3(0.0f, 0.0f, 1.0f);
        _kissEffect.GetComponent<ParticleSystem>().Play();

        StartCoroutine(RestartLevel());
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {                
                //Destroy(hit.transform.gameObject);
                GameObject obj = hit.transform.gameObject;
                if (obj) {
                    CharacterControler animal = obj.GetComponentInParent<CharacterControler>();
                    // bumb allowed only for males
                    if (animal && animal.Sex == CharacterControler.SexEnum.Male) {
                        animal.Bump();                       
                    }
                }
            }
        }
    }

    IEnumerator RestartLevel()
    {
         yield return new WaitForSeconds(pauseBeforeRestartLevel);
         SceneManager.LoadScene("Game/Scenes/main");
    }
}
