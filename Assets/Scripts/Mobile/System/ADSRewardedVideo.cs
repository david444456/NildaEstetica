using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using System;

namespace Est.Mobile
{
    public class ADSRewardedVideo : IUnityAdsListener
    {
        public Action ICompleteVideo = delegate { };
        public Action IRewardedFailed = delegate { };

        [Header("View")]
        string placementNormalVideoID = "video"; //ID del anuncio
        string placementRewardedID = "rewardedVideo"; //ID del anuncio

        string GooglePlay_Id = "4173135";

        public ADSRewardedVideo(string newID)
        {
            placementRewardedID = newID;

            Advertisement.AddListener(this);
            Advertisement.Initialize(GooglePlay_Id);
        }

        public void ShowVideo()
        {
            //ShowOptions es una coleccion que nos permite trabajar con los diferentes resultados del video
            //ShowOptions options = new ShowOptions ();

            //Devolución de llamada para recibir el resultado del anuncio.
            //options.resultCallback = HandleShowResult;

            //Si esta listo, muestra el video
            if (Advertisement.IsReady(placementRewardedID))
            {
                //Advertisement.Show (placementRewardedID, options);
                Advertisement.Show(placementRewardedID);
                Debug.Log("REWARDED - Video abierto.");

            }
            else
            {
                Debug.Log("El Video Recompensado aun no esta listo.");
                //"El Video Recompensado aun no esta listo.";
            }
        }

        void HandleShowResult(ShowResult result)
        {
            if (result == ShowResult.Finished)
            {
                Debug.Log("REWARDED - Recompensado.");
                //REWARDED - Recompensado.";
                ICompleteVideo.Invoke();

            }
            else if (result == ShowResult.Skipped)
            {
                Debug.Log("REWARDED - Video salteado.");
                IRewardedFailed.Invoke();
                // "REWARDED - Video salteado.";
            }
            else if (result == ShowResult.Failed)
            {
                Debug.Log("REWARDED - Falla al cargar el video.");
                IRewardedFailed.Invoke();
                // "REWARDED - Falla al cargar el video.";
            }
        }

        public void OnUnityAdsReady(string placementId)
        {

        }

        public void OnUnityAdsDidError(string message)
        {
            IRewardedFailed.Invoke();
        }

        public void OnUnityAdsDidStart(string placementId)
        {

        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (showResult == ShowResult.Finished)
            {
                ICompleteVideo.Invoke();
            }
        }
    }
}
