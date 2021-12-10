using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Equiment,
    Default
}

public class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
}
