using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is conection between store and other scripts in esthetic projects
public class ItemInfoStore3D : MonoBehaviour
{
    public bool IfPossibleToBuyAndDescontateCoins(ItemInfo3D Item) {
        print($" the cost of the item is {Item.dataItem.costItem}, and the index in the coin is: {Item.dataItem.indexCostItem}");
        print($" The actual coin is {ControlCoins.Instance.Coins}, and the index in the coin is: {ControlCoins.Instance.ActualLevelUnits}");
        return ControlCoins.Instance.BuyAThing_CompateWithUnits(Item.dataItem.costItem, Item.dataItem.indexCostItem);

    }

    public string GetStringUnitsWithIndex(int index) {
        return ControlCoins.Instance.GetStringValueUnitWithIndex(index);
    }

    public void BuyNewItem(ItemInfo3D GONewItem)
    {
        ControlCoins.Instance.MultiplyCoinGenerationPerSecond(GONewItem.dataItem.multiplicatorToGenerationPerSecond);
        print(GONewItem.name + " " + GONewItem.dataItem.multiplicatorToGenerationPerSecond);
    }

    public void OnSelectObject(ItemInfo3D GONewItem)
    {
        print(GONewItem.name + " " + GONewItem.dataItem.multiplicatorToGenerationPerSecond);
    }
}
