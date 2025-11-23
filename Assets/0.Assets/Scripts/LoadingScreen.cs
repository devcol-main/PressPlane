
using System.Collections;

using UnityEngine;

using System;
using UnityEngine.SceneManagement;


#region LOADING_TRANSITION_TYPE

public enum LOADING_TRANSITION_TYPE
{
    NONE, LOGO, CROSS_FADE,
}

[Serializable]
public class Logo
{
    public GameObject _logo;
    public Animator animator;
}


[Serializable]
public class CrossFade
{
    public GameObject _crossFade;
    public Animator animator;

}


#endregion

//When edit, need to check: SceneBase, SceneLoader, LoadingScreenEditor, Each Scene

public class LoadingScreen : MonoBehaviour
{

    //
    private SceneLoader sceneLoader;
    //
    
    [HideInInspector][SerializeField] public LOADING_TRANSITION_TYPE LoadingTransitionType { get; set; }
    //
    [Space(5f)]
    [SerializeField] private Logo logo;

    [Space(5f)]
    [SerializeField] private CrossFade crossFade;    


    //
    [Header("===== =====")]
    [Space(10f)]
    //[SerializeField] private GameObject loadingBar;
    [SerializeField] private bool isLoadingbarOn;

    private AsyncOperation operation;

    //
    public Logo LOGO { get { return logo; } }
    public CrossFade CROSSFADE { get { return crossFade; } }



    public bool IsLoadingStart { get; set; }

    public bool IsLoadingAnimationDone { get; set; }


    private void Awake()
    {
        sceneLoader = FindAnyObjectByType<SceneLoader>();

        DisableAllLoading();

        IsLoadingStart = false;
        IsLoadingAnimationDone = false;
    }

    // move it to coroutine
    private void Update()
    {

        // if(true == IsLoadingStart)
        // {
        //     switch(LoadingTransitionType)
        //     {
        //         case LOADING_TRANSITION_TYPE.LOGO:
        //             {
        //                 Debug.Log("FixedUpdated at loading sc");
        //                 if (logo.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        //                 {
        //                     IsLoadingAnimationDone = true;

        //                 }
        //             }
        //             break;

        //         case LOADING_TRANSITION_TYPE.CROSS_FADE:
        //             {

        //                 if (crossFade.animator.GetCurrentAnimatorStateInfo(0).IsName("CrossFade_Out"))
        //                 {

        //                     if (crossFade.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        //                     {
        //                         IsLoadingAnimationDone = true;
        //                     }
        //                 }
                        

        //                 /*
        //                 if (crossFade.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        //                 {
        //                     IsLoadingAnimationDone = true;
        //                 }
        //                 */
        //             }
        //             break;


        //         case LOADING_TRANSITION_TYPE.NONE:
        //             {
        //                 IsLoadingAnimationDone = true;

        //             }
        //             break;
        //     }
        //     // logo
        //     // 추후 제네릭 으로 변LoadingLogo()

        // }
    }

    IEnumerator CheckLoadingAnimation()
    {
        //if(true == IsLoadingStart)
        while(IsLoadingStart)
        {
            switch(LoadingTransitionType)
            {
                case LOADING_TRANSITION_TYPE.LOGO:
                    {
                        Debug.Log("FixedUpdated at loading sc");
                        if (logo.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                        {
                            IsLoadingAnimationDone = true;

                        }
                    }
                    break;

                case LOADING_TRANSITION_TYPE.CROSS_FADE:
                    {

                        if (crossFade.animator.GetCurrentAnimatorStateInfo(0).IsName("CrossFade_Out"))
                        {

                            if (crossFade.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                            {
                                IsLoadingAnimationDone = true;
                            }
                        }
                        

                        /*
                        if (crossFade.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                        {
                            IsLoadingAnimationDone = true;
                        }
                        */
                    }
                    break;


                case LOADING_TRANSITION_TYPE.NONE:
                    {
                        IsLoadingAnimationDone = true;

                    }
                    break;
            }
            // logo
            // 추후 제네릭 으로 변LoadingLogo()

            Debug.Log("CheckLoadingAnimation inside while");

            yield return null;
            Debug.Log("CheckLoadingAnimation inside while - after");
        }
        
        Debug.Log("CheckLoadingAnimation outsite while");
        yield return null;
        Debug.Log("CheckLoadingAnimation outsite while - after");
    }


    private void DisableAllLoading()
    {
        //loadingBar.SetActive(false);

        //
        crossFade._crossFade.SetActive(false);
        logo._logo.SetActive(false);
    }

    //

    public void SelectLoadingType(LOADING_TRANSITION_TYPE loadingTransitionType)
    {
        IsLoadingStart = true;

        LoadingTransitionType = loadingTransitionType;

        StartCoroutine(CheckLoadingAnimation());

        switch (loadingTransitionType)
        {
            case LOADING_TRANSITION_TYPE.LOGO:
                {
                    LoadingLogo();

                }
                break;

            case LOADING_TRANSITION_TYPE.CROSS_FADE:
                {
                    //LoadingCrossFadeEnd();
                    //LoadingCrossFade();

                    CrossFadeOut();
                }
                break;


            case LOADING_TRANSITION_TYPE.NONE:
                {

                }
                break;

        }
    }


    // Order: StartCrossFade() activate cross fade animation / then End CrossFadeEnd() 
    private void CrossFadeIn()
    {
        crossFade._crossFade.SetActive(true);

    }

    private void CrossFadeOut()
    {
        crossFade.animator.SetTrigger("Start");       

    }

    // 
    private void LoadingLogo()
    {

        logo._logo.SetActive(true);
        logo.animator.SetTrigger("Start");

        
    }



}
