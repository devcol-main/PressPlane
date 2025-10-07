using System.Collections;
using UnityEngine;

// with 2d material scroll
[RequireComponent(typeof(MeshRenderer))]

public class BackgroundScroll : MonoBehaviour
{
    // References
    private MeshRenderer meshRenderer;

    // Public Properties
    public bool IsScrollOn { get; set; }


    // Serialized Fields
    [Header("Settings")]
    [Tooltip("How fast should the texture scroll")]
    [Range((float)0.01, (float)1.0)]
    [SerializeField] private float scrollSpeed;


    // Properties
    private void Awake()
    {
        meshRenderer = this.GetComponent<MeshRenderer>();
        IsScrollOn = true;
    }

    void Start()
    {
        StartWithRandomOffset();
        StartScrollBackground();
    }
    //
    private void StartWithRandomOffset()
    {
        float randomValue = Random.Range((float)0, (float)1);

        Vector2 randomStartOffset = new Vector2(randomValue, 0);
        meshRenderer.material.mainTextureOffset = randomStartOffset;

    }

    public void StartScrollBackground()
    {
        StartCoroutine(ScrollBackgroundCoroutine());
    }

    public void StopScrollBackground()
    {
        StopCoroutine(ScrollBackgroundCoroutine());

        IsScrollOn = false;

    }

    private IEnumerator ScrollBackgroundCoroutine()
    {

        Vector2 vector2ScrollSpeed = new Vector2(scrollSpeed * Time.deltaTime, 0);

        while (true == IsScrollOn)
        {

            // for meshRenderer.material.mainTextureOffset only use x value (beccause background scrolls horizontally)
            meshRenderer.material.mainTextureOffset += vector2ScrollSpeed;

            // if scroll speed is changed in runtime, update vector2ScrollSpeed
            vector2ScrollSpeed = new Vector2(scrollSpeed * Time.deltaTime, 0);

            yield return null;
        }

        yield return null;

    }
}
