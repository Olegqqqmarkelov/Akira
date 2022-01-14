using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OpenAndCloseUI : MonoBehaviour
{
    #region params
    private Animator anim;
    [SerializeField] private Animator animChar;
    [SerializeField] public KeyboardInput _keyBoard;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private InventoryXScroll XScroll;
    [SerializeField] private GameObject scrollGM;
    [SerializeField] private GameObject Player;
    private SetActiveElement _setActiveScript;
    private GameObject Content;
    private InventoryObject inventory;


    public GameObject prefabDefaltItem;
    public GameObject prefabChikenItem;

    public bool _isActive = false;
    private int setIdActiveItemOnInv;
    #endregion

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
            SetPassiveItem();
            SetActiveItem(-1);

            scrollRect.verticalNormalizedPosition += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.S) && _isActive)
        {
            SetPassiveItem();
            SetActiveItem(1);

            scrollRect.verticalNormalizedPosition -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Z) && _isActive)
        {
            EjectItem();
        }
        if (Input.GetKeyDown(KeyCode.X) && _isActive)
        {
            EjectItemX();
        }

    }
    public void Close()
    {
        anim.SetBool("SetActive", false);
        animChar.SetBool("Open", false);
        _isActive = false;
    }

    private void EjectItem()
    {
        if (inventory.Container.Count != 0 && setIdActiveItemOnInv != null)
        {
            CreateItemBeforRemove(inventory.Container[setIdActiveItemOnInv].item, inventory.Container[setIdActiveItemOnInv].amount);
            inventory.DeleteItem(inventory.Container[setIdActiveItemOnInv].item, 0, true);
            UpdateItemsDisplay();
        }
    }

    private void EjectItemX()
    {
        if (inventory.Container.Count != 0 && setIdActiveItemOnInv != null)
        {
            _isActive = false;
            var _item = inventory.Container[setIdActiveItemOnInv].item;
            var _amount = inventory.Container[setIdActiveItemOnInv].amount;

            if (_amount == 1)
            {
                CreateItemBeforRemove(_item, 1);
                inventory.DeleteItem(_item, 1, true);
                _isActive = true;
                UpdateItemsDisplay();
            }
            else
            {
                scrollGM.active = true;
                XScroll.Amount = _amount;
                XScroll.IdItem = setIdActiveItemOnInv;
            }

            UpdateItemsDisplay();
        }
    }

    private void SetActiveItem(int _number)
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

    private void SetPassiveItem()
    {
        if(_setActiveScript != null)
            _setActiveScript.SetPassived();
    }

    public void UpdateItemsDisplay()
    {
        int count = 0;
        foreach (Transform child in Content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < inventory.Container.Count ; i++)
        {
            count = i;
            GameObject childObject = Instantiate(inventory.Container[i].item.prefab) as GameObject;

            childObject.transform.parent = Content.transform;
            childObject.transform.localScale = Vector3.one;
            childObject.name = i.ToString();

            GetChildWithName(childObject, "Amount").GetComponent<Text>().text = 
                inventory.Container[i].amount.ToString();
        }
        setIdActiveItemOnInv = (int)(count / 2);
    }

    public void CreateItemBeforRemove(ItemObject _item, int _ammount)
    {
        int item = inventory.database.GetId[_item];
        if (item == 0)
        {
            GameObject newGM = Instantiate(prefabChikenItem, Player.transform.position, Quaternion.identity);
            newGM.GetComponent<ItemAdd>().ammount = _ammount;
        }else if(item == 1)
        {
            GameObject newGM = Instantiate(prefabDefaltItem, Player.transform.position, Quaternion.identity);
            newGM.GetComponent<ItemAdd>().ammount = _ammount;
        }
    }

    private GameObject GetChildWithName(GameObject obj, string name)
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
