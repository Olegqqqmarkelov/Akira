using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
	[SerializeField] private PhysicsMovement _movement;
    [SerializeField] private GameObject _charterSprite;
    
    [SerializeField] private GameObject _trigerForward;
    [SerializeField] private GameObject _trigerBack;
    [SerializeField] private GameObject _trigerLeft;
    [SerializeField] private GameObject _trigerRight;


    private void LateUpdate()
    {
        FlipControl();
        Moves();
    }

    private void FlipControl()
    {
        Vector3 CharterScale = _charterSprite.transform.localScale;
        if(Input.GetAxis("Horizontal") < 0){
            CharterScale.x = -3;
        }
        if(Input.GetAxis("Horizontal") > 0){
            CharterScale.x = 3;
        }
        CharterScale.y = 3;
        _charterSprite.transform.localScale = CharterScale;
    }

    private void Moves()
    {
        float _speed = 2.5f;
        float vertical;
        float horizontal;

        bool trigerValueForward = _trigerForward.GetComponent<TrigerForProhibitionToMove>()._boolForWalk;
        bool trigerValueBack = _trigerBack.GetComponent<TrigerForProhibitionToMove>()._boolForWalk;
        bool trigerValueLeft = _trigerLeft.GetComponent<TrigerForProhibitionToMove>()._boolForWalk;
        bool trigerValueRight = _trigerRight.GetComponent<TrigerForProhibitionToMove>()._boolForWalk;


        if(Input.GetKey(KeyCode.W) && trigerValueForward){
            vertical = 1;
        } else if(Input.GetKey(KeyCode.S) && trigerValueBack){
            vertical = -1;
        } else {vertical = 0;}

        if(Input.GetKey(KeyCode.A) && trigerValueLeft){
            horizontal = -1;
        } else if(Input.GetKey(KeyCode.D) && trigerValueRight){
            horizontal = 1;
        } else {horizontal = 0;}

        if(Input.GetKey(KeyCode.LeftShift))
        {
            _speed = 3f;
        }

        _movement.Move(new Vector3(horizontal, 0, vertical), _speed);
    }
}
