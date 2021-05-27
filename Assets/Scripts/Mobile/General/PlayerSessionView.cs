using Est.Mobile.Save;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Est.Mobile
{
    public class PlayerSessionView : MonoBehaviour, ISaveable, IPlayerSession
    {
        PlayerSession playerSession;

        private DateTime dateQuit = DateTime.MaxValue;

        private void Awake()
        {
            playerSession = new PlayerSession(dateQuit);
        }

        public float GetTimeHour() => playerSession.GetTimeHour();

        public float GetTimeMinute() => playerSession.GetTimeMinute();

        public object CaptureState()
        {
            DateTime quitDateTime = DateTime.Now;
            return quitDateTime;
        }

        public void RestoreState(object state)
        {
            dateQuit = (DateTime)state;
        }
    }
}
