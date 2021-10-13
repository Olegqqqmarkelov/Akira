using System.Collections;
using UnityEngine;

public class TrigerForProhibitionToMove : MonoBehaviour
{
    public bool _boolForWalk = true;
    
    private void OnTriggerEnter(Collider other) {
        _boolForWalk = false;
    }
    
    private void OnTriggerExit(Collider other) {
        _boolForWalk = true;
    }
}
