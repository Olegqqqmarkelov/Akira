using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();

    public DataBaseIdItems database;
    public void AddItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++)
        {
            if(Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }

        if (!hasItem)
        {
            database.OnAfterDeserialize();
            Container.Add(new InventorySlot(database.GetId[_item], _item, _amount));
        }
    }

    public void DeleteItem(ItemObject _item, int _value, bool? _allDelete)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if(Container[i].item == _item)
            {
                if (_allDelete == true)
                {
                    Container.Remove(Container[i]);
                }
                else if (Container[i].amount <= 0)
                {
                    Container.Remove(Container[i]);
                }
                else
                {
                    if (Container[i].amount - _value == 0)
                    {
                        Container.Remove(Container[i]);
                    }
                    else
                    {
                        Container[i].MinusAmount(_value);
                    }
                }
            }
        }
    }

    [ContextMenu("ClearInventory")]
    public void ClearInventory()
    {
        Container.Clear();
    }
}

public class InventorySlot
{
    public int Id;
    public ItemObject item;
    public int amount;
    public InventorySlot(int _Id,ItemObject _item, int _amount)
    {
        Id = _Id;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
    public void MinusAmount(int value)
    {
        amount -= value;
    }
}