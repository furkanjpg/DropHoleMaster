using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;
using GoogleMobileAds.Api;

public class UndergroundCollision : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] AudioSource dropSound;
    [SerializeField] AudioSource loseSound;
    [SerializeField] AudioSource winSound;

    private InterstitialAd interstitialAd;

    public static float holescale = 0.8f;

    private void Start()
    {
        holescale = 0.8f;
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        LoadInterstitialAd();
    }
    void OnTriggerEnter (Collider other)
    {
        if (!Game.isGameover) 
        {
            string tag = other.tag;

            if (tag.Equals ("Object"))
            {
                Level.Instance.objectsInScene--;
                UIManager.Instance.UpdateLevelProgress();
                Destroy (other.gameObject);
                //deliðin büyüme oraný
                holescale += .025f;
                // Delik büyüdüðünde çalacak ses
                dropSound.Play();

                if (Level.Instance.objectsInScene == 0)
                {
                    UIManager.Instance.ShowLevelCompletedUI();
                    // KAZANILDIÐINDA ÇALACAK SES
                    winSound.Play();
                    Level.Instance.PlayWinFx();
                    Invoke ("NextLevel", 2f);
                }

            }
            if (tag.Equals ("Obstacle"))
            {
                Game.isGameover = true;
                // KAYBEDÝLDÝÐÝNDE ÇALACAK SES
                loseSound.Play();
                Camera.main.transform.DOShakePosition (1f,.2f,20,90f)
                .OnComplete (() => {
                    Level.Instance.RestartLevel ();
                });
            }
        }
    }

    void NextLevel ()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
        }
        Level.Instance.LoadNextLevel ();
    }
    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
            });
    }
   

#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-2396560093862385/2523475268";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-2396560093862385/2523475268";
#else
  private string _adUnitId = "unused";
#endif

}
