using System.Collections;
using UnityEngine;

// 2d object scroll

[RequireComponent(typeof(SpriteRenderer))]
public class LoopScroll : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("How fast should the object scroll / Range -0.5~10")]
    [Range((float)0.5, (float)10.0)]
    [SerializeField] private float scrollSpeed;


    private SpriteRenderer spriteRenderer;
    private float width;

    //private Vector2 startPosition;
    private float startPosition;
    private float endPosition;

    //
    public bool IsScrollOn { get; set; }

    private void Awake()
    {
        //
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        width = spriteRenderer.size.x;

        //

        //startPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        
        int halfWidth = (int)(width * 0.5f);

        startPosition = halfWidth;

        IsScrollOn = true;


        print("startPosition: " + startPosition);

        print("spriteRenderer: " + spriteRenderer.size.x);
        // width = 25

        // 25/2 = 12.5
        // 12.5/4

        endPosition = -1 * halfWidth;

        print("endPosition: " + endPosition);

    }

    void Update()
    {
        // scroll left (Vector2.left == (-1,0)  )
        this.transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        // if at end position, reposition to start position
        if (this.transform.position.x < endPosition)
        {
            //this.transform.position = new Vector2(startPosition.x, this.transform.position.y);
             this.transform.position = new Vector2(startPosition, this.transform.position.y);
        }

        /*


        if (this.transform.position.x < startPosition.x - (width * 0.5f))
        {
            this.transform.position = new Vector2(startPosition.x + (width * 0.5f), this.transform.position.y);
        }
        */
    }

}
