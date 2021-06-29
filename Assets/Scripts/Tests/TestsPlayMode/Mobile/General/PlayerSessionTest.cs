using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Est.Mobile;
using System;

namespace Tests.Mobile
{
    public class PlayerSessionTest
    {
        [UnityTest]
        public IEnumerator PlayerSession_CreateNewPlayerSession_GetTimeHour()
        {
            PlayerSession ps = new PlayerSession(DateTime.MinValue);
            yield return new WaitForEndOfFrame();

            Assert.IsTrue(ps.GetTimeHour() > 200, "The time hour is: " + ps.GetTimeHour());
        }

        [UnityTest]
        public IEnumerator PlayerSession_CreateNewPlayerSession_GetTimeMinute()
        {
            PlayerSession ps = new PlayerSession(DateTime.MinValue);
            yield return new WaitForEndOfFrame();

            Assert.IsTrue(ps.GetTimeMinute() > 200000, "The time minute is: " + ps.GetTimeMinute());
        }

        [UnityTest]
        public IEnumerator PlayerSession_CreateNewPlayerSession_NotSetCorrectlyTime()
        {
            PlayerSession ps = new PlayerSession(DateTime.MaxValue);
            yield return new WaitForEndOfFrame();

            Assert.IsFalse(ps.GetTimeMinute() > 200000, "The time minute is: " + ps.GetTimeMinute());
        }
    }
}
