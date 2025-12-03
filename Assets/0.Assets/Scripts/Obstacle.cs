
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [SerializeField] [ReadOnly]
    private float minHeight = -3.3f;

    [SerializeField] [ReadOnly]
    private float maxHeight = 3f;

    private BoxCollider2D boxCollider2D;

    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();

    }
    void Start()
    {
        SetRandomHeight();
    }

    public void SetRandomHeight()
    {
        float randomHeight = Random.Range(minHeight, maxHeight);
        transform.position = new Vector2(transform.position.x, randomHeight);
    }

    public void OnScrollEnd()
    {
        SetRandomHeight();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(TagName.Player))
        {
            //Debug.Log("Player Passed Obstacle");

            GameManager.Instance.IncreaseScore();

        }
    }



}
