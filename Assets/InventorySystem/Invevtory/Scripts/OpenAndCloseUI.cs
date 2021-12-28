using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndCloseUI : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    [SerializeField] private KeyboardInput _keyBoard;

    void Start()
    {
        anim = GetComponent<Animator>();
        InventoryObject inventory = _keyBoard.inventory;

    }

    public void Open()
    {
        anim.SetBool("SetActive", true);


    }

    public void Close()
    {
        anim.SetBool("SetActive", false);
    }
}
