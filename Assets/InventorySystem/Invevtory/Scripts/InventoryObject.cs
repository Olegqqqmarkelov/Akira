using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    public ItemDataBaseObject database;
    public List<InventorySlot> Container = new List<InventorySlot>();
    public void AddItem(ItemObject _item, int _amount)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if(Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                return;
            }
        }
        database.OnAfterDeserialize();
        Container.Add(new InventorySlot(database.GetId[_item], _item, _amount));
    }


    public void OnAfterDeserialize()
    {
        /*for (int i = 0; i < Container.Count; i++)
            Container[i].item = database.GetItem[Container[i].ID];*/
    }

    public void OnBeforeSerialize()
    {
    }
}

[Serializable]
public class InventorySlot : MonoBehaviour
{
    public int ID;
    public ItemObject item;
    public int amount;
    public InventorySlot(int _id ,ItemObject _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}