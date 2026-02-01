using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

[DisallowMultipleComponent]
public class SplineSpeedController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SplineAnimate _animate;

    [Header("Speed")]
    public float _startSpeed = 1.0f;
    public float _maxSpeed = 8f;
    public float _currentSpeed;
    [SerializeField] float _acceleration = 20f;   // units/sec^2
    [SerializeField] float _deceleration = 30f;   // units/sec^2

    [Header("Checkpoints (normalized 0..1)")]
    [SerializeField] List<float> _checkpoints = new();

    float _targetSpeed;
    float _speedMultiplier;
    float _standardSpeedMultiplier = 1f;

    public void Init()
    {
        if (!_animate) _animate = GetComponent<SplineAnimate>();

        // Ensure we're in Speed mode if you want runtime speed control.
        _animate.AnimationMethod = SplineAnimate.Method.Speed;

        // Optional: start from a clean state
        _currentSpeed = _startSpeed;
        _targetSpeed = _maxSpeed;
        _speedMultiplier = _standardSpeedMultiplier;
        ApplySpeedImmediate(_currentSpeed);
    }

    void Update()
    {
        float rate = (_targetSpeed >= _currentSpeed) ? _acceleration : _deceleration;
        _currentSpeed = Mathf.MoveTowards(_currentSpeed, _targetSpeed, rate * Time.deltaTime);
        _currentSpeed = _currentSpeed * _speedMultiplier;
        ApplySpeedImmediate(_currentSpeed);
    }

    // --- Public API ---

    public void AdjustSpeedMultiplier(float multiplier)
    {
        _speedMultiplier = multiplier;
    }

    public void ResetSpeedMultiplier() 
    { 
        _speedMultiplier = _standardSpeedMultiplier; 
    }

    public void AccelerateTo(float speed)
    {
        _targetSpeed = Mathf.Clamp(speed, 0f, _maxSpeed);
        if (!_animate.IsPlaying) _animate.Play();
    }

    public void StopSmooth()
    {
        _targetSpeed = 0f;
    }

    public void StopImmediate()
    {
        _targetSpeed = 0f;
        _currentSpeed = 0f;
        ApplySpeedImmediate(0f);
        _animate.Pause();
    }

    public void ResetToStart(bool playAfter = false)
    {
        _targetSpeed = 0f;
        _currentSpeed = 0f;
        ApplySpeedImmediate(0f);

        _animate.Restart(playAfter); // puts object at beginning 
        if (!playAfter) _animate.Pause(); // ensure it stays there
    }

    public void SaveCheckpoint()
    {
        float t = Mathf.Repeat(_animate.NormalizedTime, 1f); // 0..1 in current loop 
        _checkpoints.Add(t);
    }

    public void ResetToCheckpoint(int index, bool playAfter = false)
    {
        if (index < 0 || index >= _checkpoints.Count) return;

        _targetSpeed = _maxSpeed;
        _currentSpeed = _startSpeed;
        ApplySpeedImmediate(_currentSpeed);

        _animate.NormalizedTime = Mathf.Clamp01(_checkpoints[index]);
        if (playAfter) _animate.Play(); else _animate.Pause();
    }

    // --- Internals ---

    void ApplySpeedImmediate(float speed)
    {
        // Preserve progress to avoid “pops” when adjusting maxSpeed during runtime.
        float t = _animate.NormalizedTime;
        _animate.MaxSpeed = speed;
        _animate.NormalizedTime = t;
    }
}
