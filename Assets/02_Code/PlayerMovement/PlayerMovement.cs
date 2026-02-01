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
    [SerializeField] float _activeSpeed;
    bool _jumping;
    bool _grounded;
    Rigidbody _rb;
    Vector3 _moveDirection;
    private const float _gravity = -9.81f; 
    [SerializeField] Transform groundProbeStand;
    [SerializeField] Transform groundProbeSlide;

    [SerializeField] float groundRadius = 0.18f;
    [SerializeField] float groundCheckDistance = 0.12f;

    bool _sliding = false;


    public void Init(bool player1)
    {
        _pIndex = player1? PlayerIndex.Player1 : PlayerIndex.Player2;
        _rb = GetComponent<Rigidbody>();
        _activeGravityMultiplier = 2f;
        //_activeSpeed = 1f;
        if (_pIndex == PlayerIndex.Player1)
        {
            PlayerInput.Instance.Player1JumpInput += Jump;
        }
        else if (_pIndex == PlayerIndex.Player2)
        {
            PlayerInput.Instance.Player2JumpInput += Jump;
        }
        _rb = GetComponent<Rigidbody>();
        Debug.Log($"{name} Initialized");
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

            Debug.Log($"Grounded: {_grounded}");

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
        Debug.Log($"{name} Jump() called. grounded={_grounded} jumping={_jumping}");
        var v = _rb.linearVelocity;
        v.y = 0f;
        _rb.linearVelocity = v;

        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }


    void ApplyMovement()
    {
        if (!_rb) return;
        Vector3 v = _rb.linearVelocity;

        float y = v.y;

        float axis = _moveDirection.z;

        Vector3 horiz = transform.right * (axis * _activeSpeed);

        v = new Vector3(horiz.x, y, horiz.z);

        if (!_grounded)
            v.y += _gravity * _activeGravityMultiplier * Time.fixedDeltaTime;
        else if (v.y < 0f)
            v.y = -0.01f;

        _rb.linearVelocity = v;
    }


    public bool GroundCheck()
    {
        Transform probe = _sliding ? groundProbeSlide : groundProbeStand;

        if (!probe)
        {
            Debug.LogWarning("Ground probe missing!");
            return false;
        }

        Vector3 origin = probe.position;

        return Physics.SphereCast(
            origin,
            groundRadius,
            Vector3.down,
            out _,
            groundCheckDistance,
            groundLayer,
            QueryTriggerInteraction.Ignore
        );
    }

    public void ResetToNeutral()
    {
        transform.localPosition = Vector3.zero;
    }
    

}
[Serializable]
public enum PlayerIndex
{
    None,
    Player1,
    Player2
}