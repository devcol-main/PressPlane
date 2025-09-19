using System.Collections;
using UnityEngine;

// with 2d material scroll
public class BackgroundScroll : MonoBehaviour
{
    //
    public bool IsScrollOn { get; set; }
    
    /*
    [Header("Settings")]
    [Tooltip("How fast should the texture scroll / Range -0.5~0.5")]
    [Range((float)-0.5,(float)0.5)]
    [SerializeField] private float scrollSpeed;
    */

    // control by wind
    private float scrollSpeed;

    //[Range((float)0, (float)1)]
    [SerializeField] private float scrollObjectWeight;

    /*
    [Header("References")]
    [Tooltip("Either MeshRenderer or SpriteRenderer")]
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] SpriteRenderer spriteRenderer;
    */
    private MeshRenderer meshRenderer;
    private SpriteRenderer spriteRenderer;

    //
    private bool isMeshRendererOn = true;

    

    private void Awake()
    {
        // either or
        meshRenderer = this.GetComponent<MeshRenderer>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        rendererNullCheck();

        //IsScrollOn = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // StartCoroutine(StartScroll());

        StartWithRandomOffset();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // TODO: move it to Coroutine
        scrollSpeed = wind.WindSpeed;

        Vector2 vector2ScrollSpeed = new Vector2((scrollSpeed + scrollObjectWeight) * Time.deltaTime, 0);

        //vector2ScrollSpeed = new Vector2(vector2ScrollSpeed.x, 0);

        meshRenderer.material.mainTextureOffset += vector2ScrollSpeed;
        */
    }

    //
    private void StartWithRandomOffset()
    {
        float randomValue = Random.Range((float)0, (float)1);

        Vector2 randomStartOffset = new Vector2(randomValue, 0);

        if (isMeshRendererOn)
        {
            meshRenderer.material.mainTextureOffset = randomStartOffset;
        }
        else
        {
            spriteRenderer.material.mainTextureOffset = randomStartOffset;

        }
    }

    private void rendererNullCheck()
    {
        // null check
        if (null == meshRenderer)
        {
            isMeshRendererOn = false;
            // error check
            if (null == spriteRenderer)
            {
                Debug.LogError("meshRenderer & spriteRenderer are both null");
            }
        }
        else
        {
            isMeshRendererOn = true;
        }
    }

    public void ScrollBackground()
    {
        StartCoroutine(ScrollBackgroundCoroutine());
    }

    public void StopScrollBackground()
    {
        StopCoroutine(ScrollBackgroundCoroutine());

    }

    private IEnumerator ScrollBackgroundCoroutine()
    {
        /*
        if(wind.IsWindPositive)
        {
            scrollSpeed = scrollObjectWeight + wind.WindSpeed;
        }
        else
        {
            scrollSpeed = (scrollObjectWeight * -1) + wind.WindSpeed;
        }
        */
        
        Vector2 vector2ScrollSpeed = new Vector2(scrollSpeed * Time.deltaTime, 0);
        
        //Debug.Log("vector2ScrollSpeed: " + vector2ScrollSpeed);

        if(isMeshRendererOn)
        {
            // meshRenderer
            while (true == isMeshRendererOn && true == IsScrollOn)
            {
                meshRenderer.material.mainTextureOffset += vector2ScrollSpeed;
                yield return null;
            }
        }
        else
        {
            // spriteRenderer
            while (false == isMeshRendererOn && true == IsScrollOn)
            {
                spriteRenderer.material.mainTextureOffset += vector2ScrollSpeed;
                yield return null;
            }
        }

        //yield return new WaitForSeconds(0.5f);

        yield return null;

    }
}
