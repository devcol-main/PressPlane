
using System.Collections;
using UnityEngine;
using DG.Tweening;


[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    // References
    private Movement movement;
    private Animator animator;

    private PolygonCollider2D polygonCollider2D;
    private Rigidbody2D rb2d;

    // Public Properties
    public int HP { get { return hp; } set { hp = value; } }
    public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
    public bool IsInvulnerable { get { return isInvulnerable; } set { isInvulnerable = value; } }

    // Serialized Fields
    [Header("References")]
    [SerializeField] private ParticleSystem dustPS;

    [Header("Settings")]
    [SerializeField] private float invulnerableDuration = 2.0f;

    // Properties
    // unserialize later
    [SerializeField] private int hp;


    //    
    private readonly Vector3 startingTransform = new Vector3(-1.5f, 1.5f, 0f);
    private readonly Vector3 firstTimeTransform = new Vector3 (-4f,-3f,0);
    

    //
    private bool isAlive;
    private bool isInvulnerable;
    private float angle;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        

        isAlive = true;
        isInvulnerable = false;


    }

    void Start()
    {
        SoundManager.Instance.SetupContinuousAudioSource(this.gameObject, SoundAsset.SFXGroup.PLAYER);
        SoundManager.Instance.SetupContinuousSFXFly();
    }


    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && isAlive)
        {
            ApplyUpwardForce();

            EffectManager.Instance.SpawnDustPS(this.gameObject, dustPS);

            // sound fly increase
            //sfx.ContinuousSFXFly(soundPower);
            SoundManager.Instance.ContinuousSFXFly(true, GlobalData.Player.SoundPower);
        }
        else if (isAlive)
        {
            // sound fly Decrease            
            //sfx.ContinuousSFXFly(-1 * soundPower);
            SoundManager.Instance.ContinuousSFXFly(false, GlobalData.Player.SoundPower);
        }

        ApplyAngle();

    }

    public void Initiate()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        this.transform.position = startingTransform;


    }

    public void PerformFirstimeSceneTransition()
    {
        SetupFirstTimeSceneTransition();

        float duration = 2f;
        this.transform.DOMove(startingTransform, duration)
                 .SetEase(Ease.InOutSine);
                 
    }

    private void SetupFirstTimeSceneTransition()
    {
        this.transform.position = firstTimeTransform;
    }


    public void GameStart()
    {
        rb2d.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezePositionX;

        rb2d.linearVelocity = Vector2.zero;
        ApplyUpwardForce();

    }

    public void Damage(int damageAmount = 1)
    {
        if (!isAlive) return;

        hp -= damageAmount;

        // Cam Shake
        EffectManager.Instance.PlayerDamagedEffect();

        SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.PLAYER, SoundAsset.SFXPlayerName.Damaged);

        if (hp <= 0)
        {
            Death();

            // Notify GameManager about player death
            //GameManager.Instance.OnPlayerDeath();
            SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.PLAYER, SoundAsset.SFXPlayerName.Death);

        }
        else
        {
            //SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.PLAYER, SoundAsset.SFXPlayerName.Damaged);
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

        GameManager.Instance.GameOver();

        Invoke("DisablePlayer", 2.0f);

    }

    private void DisablePlayer()
    {
        this.gameObject.SetActive(false);
    }

    private void ApplyAngle()
    {
        float targetAngle;

        if (isAlive)
        {
            //targetAngle = Mathf.Clamp(movement.Rigidbody2D.velocity.y * 5, -90, 45);
            targetAngle = Mathf.Atan2(rb2d.linearVelocityY, 10f) * Mathf.Rad2Deg;

        }
        else
        {
            targetAngle = -90;
        }

        angle = Mathf.Lerp(angle, targetAngle, Time.fixedDeltaTime * 10f);

        transform.rotation = Quaternion.Euler(0, 0, angle);

    }

    private void ApplyUpwardForce()
    {
        movement.ApplyUpwardForce();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(TagName.Ground) || collision.gameObject.tag.Equals(TagName.Obstacle))
        {
            Debug.Log("Player Collided with " + collision.gameObject.tag);
            Damage();
        }

    }








}
