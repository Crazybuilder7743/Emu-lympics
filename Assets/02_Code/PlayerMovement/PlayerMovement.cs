using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerIndex _pIndex;
    [SerializeField] float _jumpForce;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundIgnoreAfterJump = 0.12f;
    float _ignoreGroundUntil;
    bool _jumpRequested;
    [HideInInspector] public float _activeGravityMultiplier;
    float _activeSpeed;
    bool _jumping;
    bool _grounded;
    Rigidbody _rb;
    Vector3 _moveDirection;
    private const float _gravity = -9.81f;

    public void Init(bool player1)
    {
        _pIndex = player1? PlayerIndex.Player1 : PlayerIndex.Player2;
        _rb = GetComponent<Rigidbody>();
        _activeGravityMultiplier = 2f;
        _activeSpeed = 1f;
        if (_pIndex == PlayerIndex.Player1)
        {
            PlayerInput.Instance.Player1JumpInput += Jump;
        }
        else if (_pIndex == PlayerIndex.Player2)
        {
            PlayerInput.Instance.Player2JumpInput += Jump;
        }
    }

    void OnDestroy()
    {
        if (!PlayerInput.Instance) return;

        if (_pIndex == PlayerIndex.Player1)
            PlayerInput.Instance.Player1JumpInput -= Jump;
        else if (_pIndex == PlayerIndex.Player2)
            PlayerInput.Instance.Player2JumpInput -= Jump;
    }


    void Jump() => _jumpRequested = true;


    void FixedUpdate()
    {
        float axis = PlayerInput.Instance.GetMoveAxis(_pIndex);
        _moveDirection = new Vector3(0f, 0f, axis);

        bool ignoreGround = Time.time < _ignoreGroundUntil;
        _grounded = ignoreGround ? false : GroundCheck();

        if (_jumpRequested)
        {
            _jumpRequested = false;

            if (_grounded)
            {
                DoJump();
                _ignoreGroundUntil = Time.time + groundIgnoreAfterJump;
                _grounded = false; // immediately treat as airborne
            }
        }

        ApplyMovement();
    }

    void DoJump()
    {
        var v = _rb.linearVelocity;
        v.y = 0f;
        _rb.linearVelocity = v;

        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }


    void ApplyMovement()
    {
        Vector3 v = _rb.linearVelocity;

        float y = v.y;

        float axis = _moveDirection.z;

        Vector3 horiz = transform.forward * (axis * _activeSpeed);

        v = new Vector3(horiz.x, y, horiz.z);

        if (!_grounded)
            v.y += _gravity * _activeGravityMultiplier * Time.fixedDeltaTime;
        else if (v.y < 0f)
            v.y = -0.01f;

        _rb.linearVelocity = v;
    }


    public bool GroundCheck()
    {
        if (!_rb) _rb = GetComponent<Rigidbody>();

        Collider col = GetComponent<Collider>();
        Bounds b = col.bounds;

        float skin = Mathf.Max(0.02f, b.size.y * 0.02f);

        float checkDistance = Mathf.Clamp(b.size.y * 0.1f, 0.05f, 0.25f);

        Vector3 halfExtents = new Vector3(
            b.extents.x * 0.9f,
            skin,
            b.extents.z * 0.9f
        );

        Vector3 boxCenter = b.center + Vector3.down * (b.extents.y - halfExtents.y);

        bool grounded = Physics.BoxCast(
            boxCenter,
            halfExtents,
            Vector3.down,
            Quaternion.identity,
            checkDistance,
            groundLayer,
            QueryTriggerInteraction.Ignore
        );

        if (grounded) _jumping = false;
        return grounded;
    }
    void Slide()
    {

    }
    

}
[Serializable]
public enum PlayerIndex
{
    None,
    Player1,
    Player2
}