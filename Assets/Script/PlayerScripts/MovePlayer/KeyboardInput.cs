using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyboardInput : MonoBehaviour
{
    public InventoryObject inventory;
    [SerializeField] private OpenAndCloseUI _invtUI;
    private bool _invtActive = false;

    [SerializeField] private PhysicsMovement _movement;
    [SerializeField] private GameObject _charterSprite;

    [SerializeField] private ParticleSystem particle_1;
    [SerializeField] private ParticleSystem particle_2;
    
    [SerializeField] private GameObject _trigerForward;
    [SerializeField] private GameObject _trigerBack;
    [SerializeField] private GameObject _trigerLeft;
    [SerializeField] private GameObject _trigerRight;
    public bool moveIsActive = true;
    private bool active = false;

    void Start()
    {
        StopParticls();
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.I) && _invtActive == false && moveIsActive)
        {
            _invtUI.Open();

            moveIsActive = false;
            _invtActive = true;
        }
        else if(Input.GetKeyDown(KeyCode.I) && _invtActive)
        {
            _invtUI.Close();

            moveIsActive = true;
            _invtActive = false;
        }

        if (moveIsActive){
            Moves();
        }
        if(active && moveIsActive)
        {
            StartParticls();
        }else{
            StopParticls();
        }
    }

    private void Moves()
    {
        Vector3 CharterScale = _charterSprite.transform.localScale;
        active = false;
        float _speed = 2.5f;
        float vertical;
        float horizontal;

        bool trigerValueForward = _trigerForward.GetComponent<TrigerForProhibitionToMove>()._boolForWalk;
        bool trigerValueBack = _trigerBack.GetComponent<TrigerForProhibitionToMove>()._boolForWalk;
        bool trigerValueLeft = _trigerLeft.GetComponent<TrigerForProhibitionToMove>()._boolForWalk;
        bool trigerValueRight = _trigerRight.GetComponent<TrigerForProhibitionToMove>()._boolForWalk;

        if(Input.GetKey(KeyCode.W) && trigerValueForward){
            vertical = 1;
            active = true;
        } else if(Input.GetKey(KeyCode.S) && trigerValueBack){
            vertical = -1;
            active = true;
        } else {vertical = 0;}

        if(Input.GetKey(KeyCode.A) && trigerValueLeft){
            horizontal = -1;
            CharterScale.x = -3;
            active = true;
        } else if(Input.GetKey(KeyCode.D) && trigerValueRight){
            horizontal = 1;
            CharterScale.x = 3;
            active = true;
        } else {horizontal = 0;}

        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            horizontal = 0;
            active = false;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            _speed = 3f;
        }

        CharterScale.y = 3;
        _charterSprite.transform.localScale = CharterScale;

        _movement.Move(new Vector3(horizontal, 0, vertical), _speed);
    }

    private void StartParticls()
    {
        particle_1.enableEmission = true;
        particle_2.enableEmission = true;
    }

    private void StopParticls()
    {
        particle_1.enableEmission = false;
        particle_2.enableEmission = false;
    }
}
