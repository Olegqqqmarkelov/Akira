using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OpenAndCloseUI : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    [SerializeField] private KeyboardInput _keyBoard;
    [SerializeField] private ScrollRect scrollRect;

    private bool _isActive = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        InventoryObject inventory = _keyBoard.inventory;

    }
    
    public void Open()
    {
        anim.SetBool("SetActive", true);
        _isActive = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) && _isActive)
        {
            scrollRect.verticalNormalizedPosition += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) && _isActive)
        {
            scrollRect.verticalNormalizedPosition -= Time.deltaTime;
        }

    }
    public void Close()
    {
        anim.SetBool("SetActive", false);
        _isActive = false;
    }
}
