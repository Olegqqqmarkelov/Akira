using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAdd : MonoBehaviour
{
    [SerializeField] private ItemObject item;
    [SerializeField] private KeyboardInput inv;
    public int ammount = 1;

    [SerializeField] private GameObject _char;

    bool isActive = false;

    void Start()
    {
        inv = FindObjectOfType<KeyboardInput>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isActive = true;
            _char.active = true;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && isActive)
        {
            inv.inventory.AddItem(item, ammount);

            Destroy(gameObject);
        }
    }

    private void OnTriggerExit()
    {
        isActive = false;
        _char.active = false;
    }
}
