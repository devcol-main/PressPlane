using UnityEngine;
using System.Collections;

public class Spinning : MonoBehaviour
{
    public AnimationCurve AnimationCurve;
    
    private bool m_spinning = false;  

    [SerializeField] [ReadOnly] private const float wheelAngle = 360;
    [SerializeField] [ReadOnly] private const int numMinSpin = 4;

    public void Spin(int rand,float eachAngleOfItemsInWheel,float spinningTime = 3.0f )
    {
        if (!m_spinning)
            StartCoroutine(DoSpin(rand,eachAngleOfItemsInWheel, spinningTime));
    }

    private IEnumerator DoSpin(int rand, float eachAngleOfItemsInWheel, float spinningTime = 3.0f)
    {
        m_spinning = true;
        
        var timer = 0.0f;
        var startAngle = transform.eulerAngles.z;

        // it has to be const bc anim
        //const float time = 3.0f;
        // 8 items / 45 degree each
        // 0~7 + 0 ~7 : 0~14 

        //var maxAngle = (360f * 4f) + (45f * rand);
        var baseSpin = wheelAngle * numMinSpin;
        var maxAngle = (baseSpin) + (eachAngleOfItemsInWheel * rand);

        while (timer < spinningTime)
        {
            var angle = AnimationCurve.Evaluate(timer / spinningTime) * maxAngle;
            transform.eulerAngles = new Vector3(0.0f, 0.0f, angle + startAngle);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();            
        }

        transform.eulerAngles = new Vector3(0.0f, 0.0f, maxAngle + startAngle);
        m_spinning = false;
        Debug.Log("Spin done");


    }

/*
    public void Spin()
    {
        if (!m_spinning)
            StartCoroutine(DoSpin());
    }

    private IEnumerator DoSpin()
    {
        m_spinning = true;
        var timer = 0.0f;
        var startAngle = transform.eulerAngles.z;

        // it has to be const bc anim
        const float time = 3.0f;

        // 8 items / 45 degree each

        var maxAngle = (360f * 4f) + (45f * rand);//270.0f;

        Debug.Log("Rand: " + rand);
        // 0~7 + 0 ~7 : 0~14 

        Debug.Log("St + Rand: " + startRand + " |  " + rand + " = " + (startRand + rand));

        if (startRand + rand > 7)
        {
            int finalNum = startRand + rand - 8;
            Debug.Log("newt: " + finalNum);
        }

        while (timer < time)
        {
            var angle = AnimationCurve.Evaluate(timer / time) * maxAngle;
            transform.eulerAngles = new Vector3(0.0f, 0.0f, angle + startAngle);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();

            
        }

        transform.eulerAngles = new Vector3(0.0f, 0.0f, maxAngle + startAngle);
        m_spinning = false;
        Debug.Log("Spin done");


    }
*/

}
