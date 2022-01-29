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
        FlipCameraPlayer cameraFlip = other.GetComponent<FlipCameraPlayer>();

        if (cameraFlip != null)
        {
            _char.active = true;
            isActive = true;

            if (cameraFlip.reversKey == false)
            {
                _char.transform.eulerAngles = new Vector3(-45, 0, 0);
            }
            else
            {
                _char.transform.eulerAngles = new Vector3(-45, 180, 0);
            }

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
