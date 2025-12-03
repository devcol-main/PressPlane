
using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Threading;


// normal scene
[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    // Components
    private Movement movement;
    private Animator animator;

    private PolygonCollider2D polygonCollider2D;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    // References

    // Public Properties
    public int HP { get { return hp; } set { hp = value; } }
    public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
    public bool IsInvulnerable { get { return isInvulnerable; } set { isInvulnerable = value; } }

    // Serialized Fields
    [Header("References")]
    [SerializeField] private ParticleSystem dustPS;

    [Header("Settings")]
    [SerializeField] private float invulnerableDuration = 1f;

    // Properties
    // unserialize later
    [SerializeField] private int hp;


    // Readyonly
    private readonly Vector3 startingTransform = new Vector3(-1.5f, 1.5f, 0f);
    private readonly Vector3 firstTimeTransform = new Vector3 (-4f,-3f,0);
    

    // private
    private bool isAlive;
    private bool isInvulnerable;
    private float angle;


    private void Awake()
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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

        if(isInvulnerable) return;

        hp -= damageAmount;

        // Cam Shake
        // default camShakeIntensity is 2
        EffectManager.Instance.PlayerDamagedEffect(camShakeIntensity: 3.0f);        

        if (hp <= 0)
        {
            //Debug.Log("Death() at" + gameObject.name);
            Death();

            // Notify GameManager about player death
            //GameManager.Instance.OnPlayerDeath();
            SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.PLAYER, SoundAsset.SFXPlayerName.Death);

        }
        else
        {           
            //Debug.Log("Damge() at" + gameObject.name);
            SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.PLAYER, SoundAsset.SFXPlayerName.Damaged);
            

            StartCoroutine(InvulnerableTimer(invulnerableDuration));
            StartCoroutine(BlinkCoroutine(times: 5, duration: invulnerableDuration));
            
        }
    }
    IEnumerator InvulnerableTimer(float invulnerableDuration)
    {
        isInvulnerable = true;

        float timer = 0f;

        while(timer < invulnerableDuration)
        {
            //Debug.Log("timer: " + timer);
            timer += Time.deltaTime;

            yield return null;
        }

        //Debug.Log("end timer");

        isInvulnerable = false;


    }

     IEnumerator BlinkCoroutine(int times, float duration)
    {
        Color originalColor = spriteRenderer.color;
        Color blinkColor = new Color(1f,1f,1f, a: 0.4f);
        //Color blinkColor = new Color(245f,85f,93f, a: 255f);
        //Color blinkColor = new Color(0.9607843f,0.3333333f,0.3647059f, a: 1f);

        float blinkDuration = duration / times;
        float halfBlink = blinkDuration / 2f;
        
        for (int i = 0; i < times; i++)
        {
            // Lerp to blink color
            float elapsed = 0f;
            while (elapsed < halfBlink)
            {
                spriteRenderer.color = Color.Lerp(originalColor, blinkColor, elapsed / halfBlink);
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            // Lerp back to original color
            elapsed = 0f;
            while (elapsed < halfBlink)
            {
                spriteRenderer.color = Color.Lerp(blinkColor, originalColor, elapsed / halfBlink);
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
        
        spriteRenderer.color = originalColor; // Ensure we end on original color
    }

    //

    public void Death()
    {
        //Debug.Log("Player Died");

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
            //Debug.Log("Player Collided with " + collision.gameObject.tag);
            Damage();
        }

    }








}
