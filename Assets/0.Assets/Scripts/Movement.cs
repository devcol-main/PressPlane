using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    // References
    private Rigidbody2D rb2d;

    // Serialized Fields
    [Header("Settings")]
    //[Tooltip("How strong is the upward force when player taps / Range 50~500")]
    //[Range(50, 500)]
    [SerializeField] 
    private float upwardForce; // if rb2d.linearVelocity = Vector2.zero

    // Properties
    public float UpwardForce { get { return upwardForce; } set { upwardForce = value; } }

    private void Awake()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

    public void ApplyUpwardForce()
    {        
        rb2d.AddForce(Vector2.up * upwardForce);
        
    }
/*
    public void ApplyDownwardForce(float force = 10f)
    {
        rb2d.AddForce(Vector2.down * force);
    }
    */
    public void SetRigidbodyState(RigidbodyType2D state)
    {
        rb2d.bodyType = state;
        /*
        switch (state)
        {
            case RigidbodyType2D.Dynamic:
                rb2d.bodyType = RigidbodyType2D.Dynamic;
                break;
            case RigidbodyType2D.Kinematic:
                rb2d.bodyType = RigidbodyType2D.Kinematic;
                break;
            case RigidbodyType2D.Static:
                rb2d.bodyType = RigidbodyType2D.Static;
                break;
            default:
                Debug.LogWarning("Invalid RigidbodyType2D state.");
                break;
        }
        */
    }
 


}
