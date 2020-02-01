using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {        
       CharacterControler otherControl = other.gameObject.GetComponentInParent<CharacterControler>();
       if (otherControl) {
           otherControl.OnTriggerWithOther(gameObject.GetComponentInParent<CharacterControler>());
       }
    } 
}
