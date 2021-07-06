using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Est.Mobile.Save;
using UnityEngine.Events;

public class ObjectStore3D : MonoBehaviour, ISaveable 
{
    [SerializeField] EBuyNewItem buyNewItem;

    [Header("GO in store")]
    [SerializeField] ItemInfo3D[] gameObjectInStore = null;
    [SerializeField] private bool[] arrayThatSaveWhoIsPurchased;

    string nameDataSave;

    [System.Serializable]
    public class EBuyNewItem : UnityEvent<ItemInfo3D>
    {
    }

    private void Awake()
    {
        if (buyNewItem == null)
            buyNewItem = new EBuyNewItem();

    }

    public ItemInfo3D[] GetObjectsItems() => gameObjectInStore;
    
    public ItemInfo3D GetObjectActualPosition(int index) => gameObjectInStore[index];
    
    public int GetMaxCountOfGameObjectsInStore() => gameObjectInStore.Length;

    public bool GetArraySaveIfPurchased(int index) => arrayThatSaveWhoIsPurchased[index];

    public void changeActiveOrDesactiveAllObjects(bool activeOrDesactive)
    {
        for (int i =0; i< gameObjectInStore.Length; i++)
        {
            gameObjectInStore[i].gameObject.SetActive(activeOrDesactive);
        }
    }

    public void BuyIndexItem(int index) {
        if (index >= GetMaxCountOfGameObjectsInStore()) return;
        if (arrayThatSaveWhoIsPurchased[index]) return; //change select

        //buy
        arrayThatSaveWhoIsPurchased[index] = true;
        buyNewItem.Invoke(gameObjectInStore[index]);
    }

    public object CaptureState()
    {
        return arrayThatSaveWhoIsPurchased;
    }

    public void RestoreState(object state)
    {
        arrayThatSaveWhoIsPurchased = (bool[])state;
    }
}
