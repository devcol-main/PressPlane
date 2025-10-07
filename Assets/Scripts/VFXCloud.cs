using System.Collections;
using UnityEditor.Purchasing;
using UnityEngine;

public class VFXCloud : MonoBehaviour
{
    // Public Properties
    public bool IsScrollOn { get; set; }

    // Serialized Fields    
    [SerializeField] private float scrollSpeed;

    // Properties
    private float minScrollSpeed = 1f;
    private float maxScrollSpeed = 5f;

    private float minPositionY = -4.5f;
    private float maxPositionY = 4.5f;

    private float minPositionX = 6f;
    private float maxPositionX = 9f;


    private float endPosition = -6f;
    //private float relocatePosition;


    private bool isUp;
    private float movememtRangeYDuration;
    private float randomYMovement;


    private void Awake()
    {
        Setting();

        IsScrollOn = true;

    }


    private void Start()
    {
        StartCloudScroll();
    }

    public void StartCloudScroll()
    {
        IsScrollOn = true;

        StartCoroutine(ScrollCloudCoroutine());
    }

    IEnumerator ScrollCloudCoroutine()
    {
        while (IsScrollOn)
        {
            //transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);                      

            transform.Translate(new Vector2(-scrollSpeed * Time.deltaTime, randomYMovement * Time.deltaTime));

            movememtRangeYDuration -= Time.deltaTime;

            if(movememtRangeYDuration <= 0f)
            {
                if(isUp)
                {
                    DownwardMovement();
                }
                else
                {
                    UpwardMovement();

                }

                SetmovememtRangeYDuration();
            }

            if(transform.position.y >= maxPositionY)
            {
                DownwardMovement();
            }
            else if(transform.position.y <= minPositionY)
            {
                UpwardMovement();
            }   


            if (transform.position.x <= endPosition)
            {
                OnScrollEnd();
            }

            yield return null;
        }
    }


    public void OnScrollEnd()
    {
        Setting();
    }

    private void Setting()
    {
        SetRandomStartingPosition();
        SetRandomScrollSpeed();

        // Set y movement
        if (Random.value > 0.5f)
        {
            UpwardMovement();
        }
        else
        {
            DownwardMovement();
        }

        SetmovememtRangeYDuration();

    }

    private void UpwardMovement()
    {
        isUp = true;
        randomYMovement = Random.Range(0.1f, 3f);
    }

    private void DownwardMovement()
    {
        isUp = false;
        randomYMovement = Random.Range(-3f, -0.1f);
    }

    private void SetmovememtRangeYDuration()
    {
        movememtRangeYDuration = Random.Range(1f, 2f);
    }
    
    

    public void SetRandomStartingPosition()
    {

        float randomPositionY = Random.Range(minPositionY, maxPositionY);
        float randomPositionX = Random.Range(minPositionX, maxPositionX);

        transform.position = new Vector2(randomPositionX, randomPositionY);

    }
    
    public void SetRandomScrollSpeed()
    {
        scrollSpeed = Random.Range(minScrollSpeed, maxScrollSpeed);
    }
    
}
