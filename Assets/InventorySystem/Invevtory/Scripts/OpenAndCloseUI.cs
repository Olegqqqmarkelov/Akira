using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OpenAndCloseUI : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    [SerializeField] private Animator animChar;
    [SerializeField] private KeyboardInput _keyBoard;
    [SerializeField] private ScrollRect scrollRect;
    private GameObject Content;
    private InventoryObject inventory;

    //Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    private bool _isActive = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        inventory = _keyBoard.inventory;
        Content = GameObject.Find("Content");

    }
    
    public void Open()
    {
        anim.SetBool("SetActive", true);
        animChar.SetBool("Open", true);
        UpdateItemsDisplay();
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
        animChar.SetBool("Open", false);
        _isActive = false;
    }

    void UpdateItemsDisplay()
    {
        foreach (Transform child in Content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < inventory.Container.Count ; i++)
        {
            GameObject childObject = Instantiate(inventory.Container[i].item.prefab) as GameObject;

            childObject.transform.parent = Content.transform;
            childObject.transform.localScale = Vector3.one;

            GetChildWithName(childObject, "Amount").GetComponent<Text>().text = 
                inventory.Container[i].amount.ToString();
        }
    }

    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }
}
