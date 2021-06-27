using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

namespace Est.Mobile
{
    public class DataSaveFirebase : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            PostToDataBases();
        }

        private void PostToDataBases() {
            User user = new User("David", 5);
            RestClient.Post("https://nilda-esthetic-default-rtdb.firebaseio.com/.json", user);
            print("Publish ");
        }
    }

    public class User {

        public string userName = "";
        public int userScore;

        public User(string newUser, int score) {
            userName = newUser;
            userScore = score;
        }
    }
}
