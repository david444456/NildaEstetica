using System;
using System.Collections.Generic;
using UnityEngine;

namespace Est.Interact
{
    public class SlotType : MonoBehaviour
    {
        [SerializeField] TypeSlotMainBusiness typeSlot;

        public TypeSlotMainBusiness GetTypeSlot() => typeSlot;

    }

    [Serializable]
    public enum TypeSlotMainBusiness {
        Hairdressing,
        Massages,
        GYM,
        Esthetic,
        Nutritionist,
        Sunbed,
        Jacuzzi,
    }
}
