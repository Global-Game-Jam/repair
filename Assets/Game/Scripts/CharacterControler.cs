using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterControler : MonoBehaviour
{   
    public enum SexEnum {
        Male,
        Female
    }    
    public enum AnimalTypeEnum {
        Mops,
        Ram
    }
    public AnimalTypeEnum AnimalType;
    public SexEnum Sex;
    public NavMeshAgent agent;      
    private float _stopTimer; 
    
    private float BUMP_DURATION = 1.0f; // in seconds

    // Start is called before the first frame update
    void Start()
    {
        _stopTimer = -1.0f;
    }   

    public void SetDestination(Vector3 pos)
    {
        agent.SetDestination(pos);
    }

    public void OnTriggerWithOther(CharacterControler other) {
        // code allowed only for JAM (not production one)
        gameObject.transform.parent.gameObject.GetComponentInParent<GameField>().OnTwoAnimalsWantToPlay(this, other);
    }   

    public void Bump()
    {
        agent.isStopped = true;
        _stopTimer = 0.0f;
    }

    void Update()
    {
        if (_stopTimer >= 0.0f) {
            _stopTimer += Time.deltaTime;
            if (_stopTimer >= BUMP_DURATION) {
                _stopTimer = -1.0f;
                agent.isStopped = false;
            }
        }
    }
}
