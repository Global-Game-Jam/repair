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
    public SexEnum Sex;
    public NavMeshAgent agent;
    private Camera _cam;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Sex == SexEnum.Male && Input.GetMouseButton(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }

        }
    }

    public void SetDestination(Vector3 pos)
    {
        agent.SetDestination(pos);
    }
}
