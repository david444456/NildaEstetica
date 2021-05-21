using Est.CycleGoal;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script control activated and desactive store, and call method to activated objects
[RequireComponent(typeof(ObjectStore3D))]
[RequireComponent(typeof(MovementStore3D))]
public class ControlStore3D : MonoBehaviour
{
    public Action<TypeGoal> NewPurchasedItem3D = delegate { };

    [SerializeField] GameObject AllObjectStore = null;
    [SerializeField] bool isObjectSelect = true;

    [Header("UI")]
    [SerializeField] Text textSelectOrPurchasedNewItem = null;
    [SerializeField] GameObject gameObjectSelectPurchasedNewItem = null;

    private ObjectStore3D ctrolObjectStore;
    private MovementStore3D movementStore3D;
    private ItemInfoStore3D buyItemInfo3D;

    private int m_indexActualPositionInStore  = 0;
    private int m_objectSelectable = 0;

    private void Start()
    {
        ctrolObjectStore = GetComponent<ObjectStore3D>();
        movementStore3D = GetComponent<MovementStore3D>();
        buyItemInfo3D = GetComponent<ItemInfoStore3D>();

        //for set initial values
        newMovement_VerifyIfObjectIsPurchased();

        //cycle goals
        ControlCycleGoals.Instance.SubscriptionToEvent(ref NewPurchasedItem3D);
    }

    public void activeOrDesactiveStore(bool changeValueBool) {
        AllObjectStore.SetActive(changeValueBool);
        ctrolObjectStore.changeActiveOrDesactiveAllObjects(changeValueBool);
    }

    public void TryBuyActualObject() {
        m_indexActualPositionInStore = movementStore3D.GetActualIndexPosition();

        if (buyItemInfo3D.IfPossibleToBuyAndDescontateCoins(ctrolObjectStore.GetObjectActualPosition(m_indexActualPositionInStore)))
        {
            ctrolObjectStore.BuyIndexItem(movementStore3D.GetActualIndexPosition());
            desactiveButtonBuyItemInStore();
            NewPurchasedItem3D.Invoke(TypeGoal.Store);
        }
    }

    public void newMovementInStore() {

        m_indexActualPositionInStore = movementStore3D.GetActualIndexPosition();
        if (!gameObjectSelectPurchasedNewItem.activeSelf) gameObjectSelectPurchasedNewItem.SetActive(true);
        newMovement_VerifyIfObjectIsPurchased();
    }

    public bool GetIfItemStoreIsPurchased(int index)
    {
        print("I store " + ctrolObjectStore.GetArraySaveIfPurchased(index) + " " + index);
        return ctrolObjectStore.GetArraySaveIfPurchased(index);
    }

    private bool IsActualSelectable()
    {
        if (ctrolObjectStore.GetArraySaveIfPurchased(m_indexActualPositionInStore))
        {
            m_objectSelectable = m_indexActualPositionInStore;
            buyItemInfo3D.OnSelectObject(ctrolObjectStore.GetObjectActualPosition(m_indexActualPositionInStore));
            gameObjectSelectPurchasedNewItem.SetActive(false);

            //save
            PlayerPrefs.SetInt("Select", m_objectSelectable);
            return true;
        }
        return false;
    }

    private void newMovement_VerifyIfObjectIsPurchased() {

        if (ctrolObjectStore.GetArraySaveIfPurchased(m_indexActualPositionInStore))
        {
            desactiveButtonBuyItemInStore();
            if(isObjectSelect) IsActualSelectable();
        }
        else {
            textSelectOrPurchasedNewItem.text = "BUY " + 
                ctrolObjectStore.GetObjectActualPosition(m_indexActualPositionInStore).dataItem.costItem  + " " +
                buyItemInfo3D.GetStringUnitsWithIndex(ctrolObjectStore.GetObjectActualPosition(m_indexActualPositionInStore).dataItem.indexCostItem); //coins
        }
    }

    private void desactiveButtonBuyItemInStore() { if (!isObjectSelect) gameObjectSelectPurchasedNewItem.SetActive(false); }
}
