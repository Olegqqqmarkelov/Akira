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
    SpriteRenderer _spriteRenderer;

    [SerializeField] private FlipCameraPlayer _flipCameraScript;

    [SerializeField] private ParticleSystem particle_1;
    [SerializeField] private ParticleSystem particle_2;
    
    [SerializeField] private TrigerForProhibitionToMove _trigerValueForward;
    [SerializeField] private TrigerForProhibitionToMove _trigerValueBack;
    [SerializeField] private TrigerForProhibitionToMove _trigerValueLeft;
    [SerializeField] private TrigerForProhibitionToMove _trigerValueRight;


    public bool moveIsActive = true;
    private bool active = false;

    void Start()
    {
        _spriteRenderer = _charterSprite.GetComponent<SpriteRenderer>();
        
        StopParticls();
    }

    private void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Q) && _flipCameraScript.permissionOnFlip)
        {
            _flipCameraScript.Flip();
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }

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
        active = false;
        float _speed = 2.5f;
        float vertical;
        float horizontal;

        bool trigerValueForward = _trigerValueForward._boolForWalk;
        bool trigerValueBack = _trigerValueBack._boolForWalk;
        bool trigerValueLeft = _trigerValueLeft._boolForWalk;
        bool trigerValueRight = _trigerValueRight._boolForWalk;

        if (_flipCameraScript.reversKey == false)
        {
            if (Input.GetKey(KeyCode.W) && trigerValueForward)
            {
                vertical = 1;
                active = true;
            }
            else if (Input.GetKey(KeyCode.S) && trigerValueBack)
            {
                vertical = -1;
                active = true;
            }
            else { vertical = 0; }

            if (Input.GetKey(KeyCode.A) && trigerValueLeft)
            {
                horizontal = -1;
                _spriteRenderer.flipX = true;
                active = true;
            }
            else if (Input.GetKey(KeyCode.D) && trigerValueRight)
            {
                horizontal = 1;
                _spriteRenderer.flipX = false;
                active = true;
            }
            else { horizontal = 0; }

            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                horizontal = 0;
                active = false;
            }
        } 
        else
        {
            if (Input.GetKey(KeyCode.S) && trigerValueBack)
            {
                vertical = 1;
                active = true;
            }
            else if (Input.GetKey(KeyCode.W) && trigerValueForward)
            {
                vertical = -1;
                active = true;
            }
            else { vertical = 0; }

            if (Input.GetKey(KeyCode.D) && trigerValueRight)
            {
                horizontal = -1;
                _spriteRenderer.flipX = false;
                active = true;
            }
            else if (Input.GetKey(KeyCode.A) && trigerValueLeft)
            {
                horizontal = 1;
                _spriteRenderer.flipX = true;
                active = true;
            }
            else { horizontal = 0; }

            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                horizontal = 0;
                active = false;
            }
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            _speed = 3f;
        }

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
