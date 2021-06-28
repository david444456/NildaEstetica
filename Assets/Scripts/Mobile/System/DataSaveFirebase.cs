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
            RetrieveFromDataBase();
        }

        private void PostToDataBases() {
            User user = new User("Hola", 5);
            RestClient.Put("https://nilda-esthetic-default-rtdb.firebaseio.com/" + "Hola" +".json", user);
            print("Publish ");
        }

        private void RetrieveFromDataBase() {
            User user = new User("Hola", 0);
            RestClient.Get<User>("https://nilda-esthetic-default-rtdb.firebaseio.com/" + "Hola" + ".json").Then( response => 
            
            {
                user = response;
                print(user.userScore);
            }   
                );
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
