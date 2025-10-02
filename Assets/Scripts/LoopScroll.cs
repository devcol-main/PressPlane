using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LoopScroll : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("How fast should the object scroll / Range -0.5~10")]
    [Range((float)0.5, (float)10.0)]
    [SerializeField] private float scrollSpeed;

    private SpriteRenderer spriteRenderer;
    private float width;
    private float startPosition;
    private float endPosition;

    public bool IsScrollOn { get; set; }

    private void Awake()
    {
        //
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        IsScrollOn = true;

        CalculateWidth();

    }
    
    private void CalculateWidth()
    {
        width = spriteRenderer.size.x;
        int halfWidth = (int)(width * 0.5f);

        startPosition = halfWidth;
        endPosition = -1 * halfWidth;
    }

    void FixedUpdate()
    {

        this.transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        // if at end position, reposition to start position
        if (this.transform.position.x < endPosition)
        {
             this.transform.position = new Vector2(startPosition, this.transform.position.y);
        }
    }

}
