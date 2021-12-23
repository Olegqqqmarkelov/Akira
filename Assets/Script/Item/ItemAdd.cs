using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAdd : MonoBehaviour
{
    [SerializeField] private ItemObject item;
    [SerializeField] private KeyboardInput inv;

    [SerializeField] private GameObject _char;

    bool isActive = false;

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
            inv.inventory.AddItem(item, 1);

            Destroy(gameObject);
        }
    }

    private void OnTriggerExit()
    {
        _char.active = false;
    }
}
