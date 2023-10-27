using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string androidAdUnitId = "Interstitial_Android";
    [SerializeField] string iOsAdUnitId = "Interstitial_iOS";
    public string adUnitId;

    public int best;
    public int score;

    public int currentStage = 0;

    public static GameManager singleton;

    private void Awake()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize("5458560", true, this);
        }

        adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? iOsAdUnitId
            : androidAdUnitId;


        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
            Destroy(gameObject);

        best = PlayerPrefs.GetInt("Highscore");
    }

    public void NextLevel()
    {
        currentStage++;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
        Debug.Log("Next Level");
    }
    
    public void RestartLevel() 
    {
        Advertisement.Load(adUnitId, this);
        Advertisement.Show(adUnitId, this);

        singleton.score = 0;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt("Highscore", score);
        }
    }

    public void OnInitializationComplete()
    {
        throw new System.NotImplementedException();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
    }
}
