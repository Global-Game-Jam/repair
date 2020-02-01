using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{    
    public GameObject Characters;

    private List<CharacterControler> _charactersFemale;
    private List<CharacterControler> _charactersMale;

    // Start is called before the first frame update
    void Start()
    {
        _charactersFemale = new List<CharacterControler>();
        _charactersMale = new List<CharacterControler>();

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
}
