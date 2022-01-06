using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryXScroll : MonoBehaviour
{
    [SerializeField] private OpenAndCloseUI InvUI;
    [SerializeField] private GameObject gmObject;
    [SerializeField] private Scrollbar scrollBar;
    [SerializeField] private Text textUi;
    public int IdItem;
    public int Amount;
    public int number;

    private void FixedUpdate()
    {
        number = (int)(Amount * scrollBar.value);
        textUi.text = $"Викінути {number}";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseXScroll();
        }
    }

    public void CloseXScroll()
    {
        InvUI._keyBoard.inventory.DeleteItem(InvUI._keyBoard.inventory.Container[IdItem].item, number, false);

        Close();
    }

    public void Close()
    {
        InvUI.UpdateItemsDisplay();
        InvUI._isActive = true;
        gmObject.active = false;
    }
}
