// Copyright (C) 2015 ricimi - All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement.
// A Copy of the Asset Store EULA is available at http://unity3d.com/company/legal/as_terms.


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinWheel : MonoBehaviour
{
    // This animation curve drives the spin wheel motion.
    //public AnimationCurve AnimationCurve;

    
    [SerializeField] private List<GameObject> spinningObjectList = new List<GameObject>();
    //[SerializeField] private List<GameObject> spinningObjectList = new List<GameObject>();
    //
    //[SerializeField] private Button spinButton;
    //
    [Header("Debug")]
    [SerializeField] [ReadOnly] private const int numItemsInWheel = 8;
    [SerializeField] [ReadOnly] private const float eachAngleOfItemsInWheel = (360 / numItemsInWheel);
    [SerializeField] [ReadOnly] private const float spinningTime = 3.0f;

    //
    [SerializeField] [ReadOnly] private int startRand;
    [SerializeField] [ReadOnly] private int rand;
    [SerializeField] [ReadOnly] private int finalStartEndRand;

    [SerializeField] private float delayDonePopup = 0.5f;
    // Set Rand Start pos
    void OnEnable()
    {
        // setting rands
        //spinButton.gameObject.SetActive(true);
        //spinButton.onClick.AddListener(OnSpinButtonClick);

        int min = 0;
        // max exclusive in Random.Range
        int max = 7 + 1;
        startRand = Random.Range(min, (max));
        rand = Random.Range(min, (max));
        //

        finalStartEndRand = startRand + rand;

        Debug.Log("startRand: " + startRand + "| rand: " + rand + " | startRand+rand: "+  (startRand + rand));

        if (finalStartEndRand > 7)
        {
            finalStartEndRand = startRand + rand - 8;
            Debug.Log("newt: " + finalStartEndRand);
        }

        SetRandStartPosZ(); 
    }

    private void SetRandStartPosZ()
    {
        // eachAngleOfItemsInWheel = 45f
        var startAngleZ = (eachAngleOfItemsInWheel * startRand);        

        foreach(var v in spinningObjectList)
        {
            v.transform.eulerAngles = new Vector3(0.0f, 0.0f, startAngleZ);
        }
    }

    

    public void OnSpinObject()
    {
        //spinObject.GetComponent<Spinning>().Spin(rand);
        foreach(var v in spinningObjectList)
        {
            v.GetComponent<Spinning>().Spin(rand,eachAngleOfItemsInWheel, spinningTime);
        }

        Invoke("DoneSpinning" ,spinningTime + delayDonePopup);
    }

    internal void DoneSpinning()
    {
        Debug.Log("DoneSpinning()");
        // Start Pize
        // Prize # finalStartEndRand 0~7
        // 0 & 6 : Nothing
        // 1 & 5 : DoubleLife
        // 2,3,4,7 : Life

        switch(finalStartEndRand)
		{
			case 0:
			case 6:
				{
					Debug.Log("Nothing 0&6: " + finalStartEndRand);
                    
				}
				break;
			case 1:
			case 5:
				{
					Debug.Log(" DoubleLife 1&5: " + finalStartEndRand);

				}
				break;
			case 2:
			case 3:
			case 4:
			case 7:
				{
					Debug.Log("Life 2,3,4,7: " + finalStartEndRand);

				}
				break;
				
		}


    }

    internal void PrizeForWheel()
    {
        
    }

}


