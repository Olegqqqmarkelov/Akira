using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DB_Id_Items", menuName = "Inventory System/dbIdItems")]
public class DataBaseIdItems : ScriptableObject
{
    public ItemObject[] Items;

    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();
    public Dictionary<ItemObject, int> GetId = new Dictionary<ItemObject, int>();

    public void OnAfterDeserialize()
    {
        GetId.Clear();
        GetItem.Clear();
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].Id = i;
            GetItem.Add(i, Items[i]);
            GetId.Add(Items[i], i);
        }
    }

}
