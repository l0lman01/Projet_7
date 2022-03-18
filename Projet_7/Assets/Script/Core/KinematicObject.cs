using UnityEngine;


/// <summary>
/// Implements game physics for some in game entity.
/// </summary>
public class KinematicObject : MonoBehaviour
{
    protected const float MIN_MOVE_DISTANCE = 0.001f;
    protected const float SHELL_RADIUS = 0.01f;

    /// <summary>
    /// The minimum normal (dot product) considered suitable for the entity sit on.
    /// </summary>
    public float MinGroundNormalY = .65f;

    /// <summary>
    /// A custom gravity coefficient applied to this entity.
    /// </summary>
    public float GravityModifier = 1f;

    /// <summary>
    /// The current velocity of the entity.
    /// </summary>
    public Vector2 Velocity;

    /// <summary>
    /// Is the entity currently sitting on a surface?
    /// </summary>
    /// <value></value>
    public bool IsGrounded { get; private set; }

    protected Vector2 _targetVelocity;
    protected Vector2 _groundNormal;
    protected Rigidbody2D _rigidbody;
    protected ContactFilter2D _contactFilter;
    protected RaycastHit2D[] _hitBuffers = new RaycastHit2D[16];


    /// <summary>
    /// Bounce the object's vertical velocity.
    /// </summary>
    /// <param name="value"></param>
    public void Bounce(float value)
    {
        Velocity.y = value;
    }

    /// <summary>
    /// Bounce the objects velocity in a direction.
    /// </summary>
    /// <param name="dir"></param>
    public void Bounce(Vector2 dir)
    {
        Velocity.y = dir.y;
        Velocity.x = dir.x;
    }

    /// <summary>
    /// Teleport to some position.
    /// </summary>
    /// <param name="position"></param>
    public void Teleport(Vector3 position)
    {
        _rigidbody.position = position;
        Velocity *= 0f;
        _rigidbody.velocity *= 0f;
    }

    protected virtual void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        _contactFilter.useLayerMask = true;
    }

    protected virtual void FixedUpdate()
    {
        //if already falling, fall faster than the jump speed, otherwise use normal gravity.
        if (Velocity.y < 0f)
            Velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;
        else
            Velocity += Physics2D.gravity * Time.deltaTime;

        Velocity.x = _targetVelocity.x;

        IsGrounded = false;

        var deltaPosition = Velocity * Time.deltaTime;
        var moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        var move = moveAlongGround * deltaPosition.x;

        PerformMovement(move, false);
        move = Vector2.up * deltaPosition.y;
        PerformMovement(move, true);
    }

    protected virtual void Update()
    {
        _targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.isKinematic = true;
    }

    protected virtual void OnDisable()
    {
        _rigidbody.isKinematic = false;
    }

    protected virtual void ComputeVelocity()
    { 
    }

    void PerformMovement(Vector2 move, bool yMovement)
    {
        var distance = move.magnitude;
        if (distance > MIN_MOVE_DISTANCE)
        {
            //check if we hit anything in current direction of travel
            var count = _rigidbody.Cast(move, _contactFilter, _hitBuffers, distance + SHELL_RADIUS);
            for (var i = 0; i < count; i++)
            {
                var currentNormal = _hitBuffers[i].normal;

                //is this surface flat enough to land on?
                if (currentNormal.y > MinGroundNormalY)
                {
                    IsGrounded = true;
                    // if moving up, change the groundNormal to new surface normal.
                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0f;
                    }
                }
                if (IsGrounded)
                {
                    //how much of our velocity aligns with surface normal?
                    var projection = Vector2.Dot(Velocity, currentNormal);
                    if (projection < 0f)
                    {
                        //slower velocity if moving against the normal (up a hill).
                        Velocity = Velocity - projection * currentNormal;
                    }
                }
                else
                {
                    //We are airborne, but hit something, so cancel vertical up and horizontal velocity.
                    Velocity.x *= 0f;
                    Velocity.y = Mathf.Min(Velocity.y, 0f);
                }
                //remove shellDistance from actual move distance.
                var modifiedDistance = _hitBuffers[i].distance - SHELL_RADIUS;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }
        _rigidbody.position = _rigidbody.position + move.normalized * distance;
    }
}