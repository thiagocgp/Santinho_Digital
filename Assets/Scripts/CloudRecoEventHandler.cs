/*==============================================================================
Copyright (c) 2015-2018 PTC Inc. All Rights Reserved.

Copyright (c) 2012-2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
==============================================================================*/
using UnityEngine;
using Vuforia;
using UnityEngine.Video;

/// <summary>
/// This MonoBehaviour implements the Cloud Reco Event handling for this sample.
/// It registers itself at the CloudRecoBehaviour and is notified of new search results as well as error messages
/// The current state is visualized and new results are enabled using the TargetFinder API.
/// </summary>
public class CloudRecoEventHandler : MonoBehaviour
{
    #region PRIVATE_MEMBERS
    CloudRecoBehaviour m_CloudRecoBehaviour;
    ObjectTracker m_ObjectTracker;
    TargetFinder m_TargetFinder;
    #endregion // PRIVATE_MEMBERS


    #region PUBLIC_MEMBERS
    /// <summary>
    /// Can be set in the Unity inspector to reference a ImageTargetBehaviour 
    /// that is used for augmentations of new cloud reco results.
    /// </summary>
    [Tooltip("Here you can set the ImageTargetBehaviour from the scene that will be used to " +
             "augment new cloud reco search results.")]
    public ImageTargetBehaviour m_ImageTargetBehaviour;
    #endregion // PUBLIC_MEMBERS


    #region MONOBEHAVIOUR_METHODS
    /// <summary>
    /// Register for events at the CloudRecoBehaviour
    /// </summary>
    void Start()
    {
        // Register this event handler at the CloudRecoBehaviour
        m_CloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        if (m_CloudRecoBehaviour)
        {
            m_CloudRecoBehaviour.RegisterOnInitializedEventHandler(OnInitialized);
            m_CloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);
            m_CloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
        }
    }
    #endregion // MONOBEHAVIOUR_METHODS


    #region INTERFACE_IMPLEMENTATION_ICloudRecoEventHandler

    /// <summary>
    /// called when TargetFinder has been initialized successfully
    /// </summary>
    public void OnInitialized(TargetFinder targetFinder)
    {
        Debug.Log("Cloud Reco initialized successfully.");

        m_ObjectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        m_TargetFinder = targetFinder;
    }

    /// <summary>
    /// when we start scanning, unregister Trackable from the ImageTargetBehaviour, 
    /// then delete all trackables
    /// </summary>
    public void OnStateChanged(bool scanning)
    {
        Debug.Log("<color=blue>OnStateChanged(): </color>" + scanning);

        // Changing CloudRecoBehaviour.CloudRecoEnabled to false will call:
        // 1. TargetFinder.Stop()
        // 2. All registered ICloudRecoEventHandler.OnStateChanged() with false.

        // Changing CloudRecoBehaviour.CloudRecoEnabled to true will call:
        // 1. TargetFinder.StartRecognition()
        // 2. All registered ICloudRecoEventHandler.OnStateChanged() with true.
    }

    /// <summary>
    /// Handles new search results
    /// </summary>
    /// <param name="targetSearchResult"></param>
    public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
    {
        Debug.Log("<color=blue>OnNewSearchResult(): </color>" + targetSearchResult.TargetName);

        TargetFinder.CloudRecoSearchResult cloudRecoResult = (TargetFinder.CloudRecoSearchResult)targetSearchResult;

        // This code demonstrates how to reuse an ImageTargetBehaviour for new search results
        // and modifying it according to the metadata. Depending on your application, it can
        // make more sense to duplicate the ImageTargetBehaviour using Instantiate() or to
        // create a new ImageTargetBehaviour for each new result. Vuforia will return a new
        // object with the right script automatically if you use:
        // TargetFinder.EnableTracking(TargetSearchResult result, string gameObjectName)

        // Check if the metadata isn't null
        if (cloudRecoResult.MetaData == null)
        {
            Debug.Log("Target metadata not available.");
        }
        else
        {
            Debug.Log("MetaData: " + cloudRecoResult.MetaData);
            Debug.Log("TargetName: " + cloudRecoResult.TargetName);
            Debug.Log("Pointer: " + cloudRecoResult.TargetSearchResultPtr);
            Debug.Log("TrackingRating: " + cloudRecoResult.TrackingRating);
            Debug.Log("UniqueTargetId: " + cloudRecoResult.UniqueTargetId);
        }

        //Starteth - Pega o metadata em JSON e adiciona os links na url do player de videos e nos botoes das redes sociais
        //Starteth - Aletra a cor do icone do botão da rede social caso nao tenha a url da mesma no JSON metadata
        SantinhoMetadata dados = new SantinhoMetadata();
        dados = JsonUtility.FromJson<SantinhoMetadata>(cloudRecoResult.MetaData);
        m_ImageTargetBehaviour.GetComponentInChildren<VideoPlayer>().url = dados.url;
        m_ImageTargetBehaviour.GetComponentInChildren<ButtonsController>().urlButton1 = dados.instagram;
        if (dados.instagram.Equals(""))
        {
            m_ImageTargetBehaviour.GetComponentInChildren<ButtonsController>().ChangeButtonStatus(1);
        }
        m_ImageTargetBehaviour.GetComponentInChildren<ButtonsController>().urlButton2 = dados.facebook;
        if (dados.facebook.Equals(""))
        {
            m_ImageTargetBehaviour.GetComponentInChildren<ButtonsController>().ChangeButtonStatus(2);
        }
        m_ImageTargetBehaviour.GetComponentInChildren<ButtonsController>().urlButton3 = dados.twitter;
        if (dados.twitter.Equals(""))
        {
            m_ImageTargetBehaviour.GetComponentInChildren<ButtonsController>().ChangeButtonStatus(3);
        }
        m_ImageTargetBehaviour.GetComponentInChildren<ButtonsController>().urlButton4 = dados.site;
        if (dados.site.Equals(""))
        {
            m_ImageTargetBehaviour.GetComponentInChildren<ButtonsController>().ChangeButtonStatus(4);
        }

        // Changing CloudRecoBehaviour.CloudRecoEnabled to false will call TargetFinder.Stop()
        // and also call all registered ICloudRecoEventHandler.OnStateChanged() with false.
        m_CloudRecoBehaviour.CloudRecoEnabled = false;

        // Clear any existing trackables
        m_TargetFinder.ClearTrackables(false);

        // Enable the new result with the same ImageTargetBehaviour:
        m_TargetFinder.EnableTracking(cloudRecoResult, m_ImageTargetBehaviour.gameObject);

        // Pass the TargetSearchResult to the Trackable Event Handler for processing
        m_ImageTargetBehaviour.gameObject.SendMessage("TargetCreated", cloudRecoResult, SendMessageOptions.DontRequireReceiver);
    }
    #endregion // INTERFACE_IMPLEMENTATION_ICloudRecoEventHandler
}
