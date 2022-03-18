using System.Collections;
using UnityEngine;
//using UnityEngine.Rendering.Universal;

public class PlayerController : KinematicObject
{
    public float JumpModifier = 0.9f;
    public float JumpDeceleration = 0f;
    public float MaxSpeed = 7f;
    public float JumpTakeOffSpeed = 7f;

    public AudioClip JumpAudio;
    public AudioClip RespawnAudio;
    public AudioClip OuchAudio;

    public Bounds Bounds => _collider2d.bounds;

    private bool _jump, _stopJump;
    private bool _controlEnabled = true;
    private Vector2 _move;
    private JumpState _jumpState = JumpState.Grounded;

    private AudioSource _audioSource;
    private Collider2D _collider2d;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private GameObject[] objectsbullet;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _collider2d = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

    }

    public void EnteredDeathZone()
    {
        
        _controlEnabled = false;
        _collider2d.enabled = false;

        if (_audioSource && OuchAudio)
            _audioSource.PlayOneShot(OuchAudio);

        //Ligne pour supprimer les bullets
        objectsbullet = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in objectsbullet)
        {
            Destroy(bullet, 1f);
        }

        _animator.SetTrigger("hurt");
        _animator.SetBool("dead", true);

        StartCoroutine(RespawnCoroutine());   
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(1.5f);

        _collider2d.enabled = true;

        Teleport(GameObject.FindGameObjectWithTag("Respawn").transform.position);


        if (_audioSource && RespawnAudio)
            _audioSource.PlayOneShot(RespawnAudio);

        _jumpState = JumpState.Grounded;
        _animator.SetBool("dead", false);

        yield return new WaitForSeconds(2.5f);

        _controlEnabled = true;
    }

    protected override void Update()
    {
        if (_controlEnabled)
        {
            _move.x = Input.GetAxis("Horizontal");
            if (_jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                _jumpState = JumpState.PrepareToJump;

            else if (Input.GetButtonUp("Jump"))
                _stopJump = true;
        }
        else
            _move.x = 0f;
        UpdateJumpState();
        base.Update();
    }

    void UpdateJumpState()
    {
        _jump = false;
        switch (_jumpState)
        {
            case JumpState.PrepareToJump:
                _jumpState = JumpState.Jumping;
                _jump = true;
                _stopJump = false;
                break;

            case JumpState.Jumping:
                if (!IsGrounded)
                    _jumpState = JumpState.InFlight;
                break;

            case JumpState.InFlight:
                if (IsGrounded)
                    _jumpState = JumpState.Landed;
                break;

            case JumpState.Landed:
                _jumpState = JumpState.Grounded;
                break;
        }
    }

    protected override void ComputeVelocity()
    {
        if (_jump && IsGrounded)
        {
            Velocity.y = JumpTakeOffSpeed * JumpModifier;
            _jump = false;
        }
        else if (_stopJump)
        {
            _stopJump = false;
            if (Velocity.y > 0f)
                Velocity.y *= JumpDeceleration;
        }

        if (_move.x > 0.01f)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_move.x < -0.01f)
        {
            _spriteRenderer.flipX = true;
        }
        _animator.SetBool("grounded", IsGrounded);
        _animator.SetFloat("velocityX", Mathf.Abs(Velocity.x) / MaxSpeed);
        _animator.SetFloat("velocityY", Mathf.Abs(Velocity.y) / MaxSpeed);

        _targetVelocity = _move * MaxSpeed;
    }

    public enum JumpState
    {
        Grounded,
        PrepareToJump,
        Jumping,
        InFlight,
        Landed
    }
}