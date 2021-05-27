using Est.Interact;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Est.CycleGoal
{
    public abstract class DataGoal : ScriptableObject
    {
        [Header("Principal data")]
        [SerializeField] private float m_coinReward = 0;
        [SerializeField] private int m_levelUnitCoinReward = 0;
        [SerializeField] private TypeGoal m_typeGoalData;
        [TextArea][SerializeField] private string m_textInformationGoal = " ";
        [SerializeField] private Sprite m_spriteInformationGoal = null;

        public virtual int IndexItemStore3D { get => -1; }
        public virtual float GetValueToGoalCoin { get => 100;  }
        public virtual TypeSlotMainBusiness TypeSlotMain { get => TypeSlotMainBusiness.Esthetic; }
        public virtual float GetLevelTypeSlotToCheck { get => -1; }

        public TypeGoal GetTypeGoal() => m_typeGoalData;

        public string GetTextInfo() => m_textInformationGoal;

        public Sprite GetSpriteInfo() => m_spriteInformationGoal;

        public float GetCoinReward() => m_coinReward;

        public int GetLevelUnitCoinReward() => m_levelUnitCoinReward;
    }

    public enum TypeGoal {
        Slot, Store, Coin
    }
}
