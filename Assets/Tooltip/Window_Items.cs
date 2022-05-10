using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_Items : MonoBehaviour {

    private void Update()
    {
        try
        {
            ItemInfo itemInfo = transform.Find("pfUI_Item(Clone)").GetComponent<ItemInfo>();
            Tooltip_ItemStats.AddTooltip(transform.Find("pfUI_Item(Clone)"), itemInfo.sprite, itemInfo.itemName, itemInfo.itemDescription, itemInfo.DEX, itemInfo.CON, itemInfo.STR);
        }
        catch
        {
            return;
        }
    }
    

}
