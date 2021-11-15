using UnityEngine;
using System.Collections;

public class TrigerForProhibitionToMove : MonoBehaviour
{
    public bool _boolForWalk = true;
    
    private void OnTriggerEnter(Collider other) {
        if(other.isTrigger != true)
            _boolForWalk = false;
    }
    
    private void OnTriggerExit(Collider other) {
        _boolForWalk = true;
    }
}
