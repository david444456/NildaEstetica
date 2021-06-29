using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Est.Mobile
{
    public class PlayerSession : IPlayerSession
    {
        private string dateQuitString = "";
        private TimeSpan timeSpan;
        private DateTime dateQuit = DateTime.MaxValue;

        public PlayerSession (DateTime dateTimeQuit){
            dateQuit = dateTimeQuit;

            DateTime dateNow = DateTime.Now;

            if (dateNow > dateQuit)
            {
                timeSpan = dateNow - dateQuit;
                Debug.Log(timeSpan.TotalSeconds);
            }
        }

        public float GetTimeHour() {
            return (float)timeSpan.TotalHours;
        }

        public float GetTimeMinute()
        {
            return (float)timeSpan.TotalMinutes;
        }

    }
}
