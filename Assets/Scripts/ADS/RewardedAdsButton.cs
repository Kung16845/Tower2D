using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms
    int _adsWatched = 0; // Add this variable to keep track of the number of times ads have been watched
    int _maxAdsAllowed = 3; // Add this variable to set the maximum number of ads allowed
    void Awake()
    {   
       _adUnitId = _androidAdUnitId;
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
    _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
    _adUnitId = _androidAdUnitId;
#endif
        _adsWatched = 0;
        // Disable the button until the ad is ready to show:
        _showAdButton.interactable = true;

        LoadAd();
    }
 
    // Call this public method when you want to get an ad ready to show.
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }
 
    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
 
        if (adUnitId.Equals(_adUnitId))
        {
            // Configure the button to call the ShowAd() method when clicked:
            _showAdButton.onClick.AddListener(ShowAd);
            // Enable the button for users to click:
            _showAdButton.interactable = true;
        }
    }
 
    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        // Disable the button:
        _showAdButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
    }
 
    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
{
    if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
    {
        Debug.Log("Unity Ads Rewarded Ad Completed");
        _adsWatched++;
        if (_adsWatched >= _maxAdsAllowed)
        {
            _showAdButton.interactable = false;
            _showAdButton.onClick.RemoveAllListeners();
        }
        else
        {
            // Load the next ad
            LoadAd();
        }
        Debug.Log("You are gained Coin");
        
        int numberOfTries = 10; // จำนวนครั้งที่ดูโฆษณา
        int numberOfSuccesses = 2; // จำนวนครั้งที่ประสบความสำเร็จในการรับเงิน 100 เหรียญ
        float probabilityOfSuccess = 0.2f; // ความน่าจะเป็นในการประสบความสำเร็จ (20%)

        float binomialProbability = ProbabilityUtil.CalculateBinomialProbability(numberOfTries, numberOfSuccesses, probabilityOfSuccess);
        
        if (Random.value <= binomialProbability) // ให้เงิน 100 เหรียญ
        {
            Debug.Log("You gained 100 Coins");
            GameManager.instance.AddGold(100);
        }
        else if (Random.value <= ProbabilityUtil.CalculateBinomialProbability(numberOfTries, numberOfSuccesses, 0.5f)) // ให้เงิน 50 เหรียญ
        {
            Debug.Log("You gained 50 Coins");
            GameManager.instance.AddGold(50);
        }
        else // ให้เงิน 25 เหรียญ
        {
            Debug.Log("You gained 25 Coins");
            GameManager.instance.AddGold(25);
        }
    }
}

 
    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
 
    void OnDestroy()
    {
        // Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
    }
}