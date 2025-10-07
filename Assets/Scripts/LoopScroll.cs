
using UnityEngine;


// Loop scroll with sprite renderer / using 3 same sprites side by side and repositioning
//[RequireComponent(typeof(SpriteRenderer))]
public class LoopScroll : MonoBehaviour
{


    // References
    //private SpriteRenderer spriteRenderer;

    // Public Properties
    //public bool IsScrollOn { get; set; }
    public bool IsScrollOn { get; set; }

    public float ScrollSpeed { get { return scrollSpeed; } set { scrollSpeed = value; } }

    // Properties
    private float objectWidth;
    private float endPosition;
    private float relocatePosition;

    private float scrollSpeed;

    /*
    private float width;
    private float halfWidth;
    */

    private void Awake()
    {
        //
        //spriteRenderer = this.GetComponent<SpriteRenderer>();

        IsScrollOn = true;

        switch (gameObject.tag)
        {
            case TagName.Ground:
                {
                    objectWidth = 12.0f;

                    endPosition = -objectWidth;
                    relocatePosition = objectWidth * 2;
                }
                break;

            case TagName.Obstacle:
                {
                    endPosition = -8f;
                    relocatePosition = 8f;
                }
                break;

            default:
                Debug.LogWarning("Environment tag not set properly");
                break;
        }


    }


    void FixedUpdate()
    {
        Scrolling();

    }

    public void Scrolling()
    {
        if (true == IsScrollOn)
        {
            transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

            // if at end position, reposition to start position
            if (transform.position.x <= endPosition)
            {
                // sendmessage is costly but it's ok since it's small project
                SendMessage("OnScrollEnd", SendMessageOptions.DontRequireReceiver);

            }
        }

    }

    public void OnScrollEnd()
    {
        float currentPosition = transform.position.x;
        float offset = currentPosition - endPosition;

        relocatePosition += offset;
        //relocatePosition += Mathf.Round(offset * 100.0f) * 0.01f;

        //Debug.Log("endPosition : " + endPosition + " | " + currentPosition + " | offset : " + offset + " | relocatePosition : " + relocatePosition);

        transform.position = new Vector2(relocatePosition, transform.position.y);

    }


    // Courtine is efficient but FixedUpdate is more stable, it it creates small apart bewteen sprite. So decied to use FixedUpdate

    // private IEnumerator StartScrollCoroutine()
    // {
    //     while (true == IsScrollOn)
    //     {
    //         // move left
    //         this.transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

    //         // if at end position, reposition to start position
    //         if (this.transform.position.x <= endPosition)
    //         {
    //             Debug.Log("endPosition : " + endPosition +" | "+ this.transform.position.x);
    //             this.transform.position = new Vector2(relocatePosition, this.transform.position.y);

    //             Debug.Log("Repositioned : " + this.transform.position.x);
    //         }

    //         yield return null;
    //     }

    // }   

}
