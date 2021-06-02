using UnityEngine;

public class RateMore : MonoBehaviour
{
    [SerializeField] GameObject GOShowRateAppCanvas = null;
    [SerializeField] int countLimitToShowRate = 6;

    bool desactiveForever = false;
    int countChangeScene = 0;

    void Start()
    {
        //count dies
        countChangeScene = PlayerPrefs.GetInt("CountDead", 0);

        //desactive rate app forever
        if (PlayerPrefs.GetInt("NoRateUs", 0) == 1)
        {
            //hide Rate Us button
            desactiveForever = true;
        }
    }

    public void NewChangeScene() {
        if (desactiveForever) return;

        countChangeScene++;

        if (countChangeScene >= countLimitToShowRate) {
            GOShowRateAppCanvas.SetActive(true);
            countChangeScene = 0;
        }

        //save
        PlayerPrefs.SetInt("CountDead", countChangeScene);

    }

    public void OnlyMethodForInternalTest() {
        desactiveForever = false;

        PlayerPrefs.SetInt("NoRateUs", 0);
    }

    public void DesactiveCanvasRate() {
        GOShowRateAppCanvas.SetActive(false);
    }

    public void OnRemoveForeverClick()
    {
        DesactiveCanvasRate();

        desactiveForever = true;

        PlayerPrefs.SetInt("NoRateUs", 1);
    }

    public void Rate ()
	{
       // OnRemoveForeverClick();

        // "market" works for android  (iOS: put your app link
        Application.OpenURL ("market://details?id=com.test.testtest");
    }

	public void More ()
	{
		// Android  ,(iOS: put you app store link)
		Application.OpenURL ("https://play.google.com/store/apps/dev?id=1551996653619230352");
	}

	public void Feedback ()
	{
		Application.OpenURL ("mailto:youremail@gmail.com");
	}

    public void OpenUrl(string url) {
        Application.OpenURL(url);
    }

}
