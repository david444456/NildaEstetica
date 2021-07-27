using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Est.AI;

namespace Est.Interact
{
    public class SunbedFunctionality : MonoBehaviour
    {
        [SerializeField] GameObject _GOLightPointSunseb;

        public void ChangeValueOfActiveLight(bool newValue) {
            _GOLightPointSunseb.SetActive(newValue);
        }
    }
}
