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
    private SetActiveElement _setActiveScript;
    private GameObject Content;
    private InventoryObject inventory;

    //Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    private bool _isActive = false;
    private int setIdActiveItemOnInv = 0;

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
        if (Input.GetKeyDown(KeyCode.W) && _isActive)
        {
            scrollRect.verticalNormalizedPosition += Time.deltaTime;

            SetPassiveItem();
            SetActiveItem(-1);
        }
        if (Input.GetKeyDown(KeyCode.S) && _isActive)
        {
            SetPassiveItem();
            SetActiveItem(1);
            scrollRect.verticalNormalizedPosition -= Time.deltaTime;
        }

    }
    public void Close()
    {
        anim.SetBool("SetActive", false);
        animChar.SetBool("Open", false);
        _isActive = false;
    }

    void SetActiveItem(int _number)
    {
        if(setIdActiveItemOnInv + _number != inventory.Container.Count && _number == 1)
        {
            setIdActiveItemOnInv++;
        } else if(setIdActiveItemOnInv + _number != -1 && _number == -1)
        {
            setIdActiveItemOnInv--;
        }

        foreach (Transform child in Content.transform)
        {
            if (child.gameObject.name == setIdActiveItemOnInv.ToString())
            {
                _setActiveScript = child.GetComponent<SetActiveElement>();

                if (_setActiveScript != null)
                {
                    _setActiveScript.SetActived();
                    return;
                }
            }
        }
    }

    void SetPassiveItem()
    {
        if(_setActiveScript != null)
            _setActiveScript.SetPassived();
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
            childObject.name = i.ToString();

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
