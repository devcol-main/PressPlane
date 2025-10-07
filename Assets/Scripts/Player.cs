using System;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    // References
    private Movement movement;
    private Animator animator;

    private PolygonCollider2D polygonCollider2D;

    // Public Properties
    public int HP { get { return hp; } set { hp = value; } }
    public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
    public bool IsInvulnerable { get { return isInvulnerable; } set { isInvulnerable = value; } }

    // Serialized Fields
    [SerializeField] private float invulnerableDuration = 2.0f;

    // Properties
    // unserialize later
    [SerializeField] private int hp;


    private bool isAlive = true;
    private bool isInvulnerable = false;


    
    // test
    float angle;


    private void Awake()
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();

        isAlive = true;

    }


    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && isAlive)
        {
            ApplyUpwardForce();

        }

    }


    public void ApplyUpwardForce()
    {
        movement.ApplyUpwardForce();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(TagName.Ground) || collision.gameObject.tag.Equals(TagName.Obstacle))
        {
            Debug.Log("Player Collided with " + collision.gameObject.tag);
            Damage();
        }

    }

    public void Damage(int damageAmount = 1)
    {
        if (!isAlive) return;

        hp -= damageAmount;

        CameraManager.Instance.ShakeCamera();

        if (hp <= 0)
        {
            Death();

            // Notify GameManager about player death
            //GameManager.Instance.OnPlayerDeath();
        }
    }

    public void Invulnerable()
    {
        if (!isAlive) return;

        if (!isInvulnerable)
        {
            isInvulnerable = true;
            Invoke("ResetInvulnerability", invulnerableDuration);
        }
    }

    public void Death()
    {
        Debug.Log("Player Died");

        isAlive = false;
        polygonCollider2D.enabled = false;

        animator.SetTrigger("Die");

        Invoke("DisablePlayer", 2.0f);        

    }
    
    private void DisablePlayer()
    {
        this.gameObject.SetActive(false);
    }









}
