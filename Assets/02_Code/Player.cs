using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    public PlayerHealth healthSystem = new PlayerHealth();
    public PlayerMaskManager maskmanager;
    public PlayerSplitScreenInfo playerSplitScreenInfo;
    public Renderer maskRenderer;
    public ColorCoordinater colorCoordinater;
    public VisualEffect laserVFX;
    public SplineAnimate splineAnimator;
    public PlayerMovement movement;
    public SplineSpeedController speedController;
    public Animator animator;
    public float normalSpeedforAnimation = 3f;
    public bool started = false;
    public void Awake()
    {
        laserVFX.gameObject?.SetActive(false);
    }
    public void Update()
    {
        if (!started)
        {
            return;
        }
        healthSystem.RegenHealthTick();
        EvaluateAnimatorSpeed();
    }
    private void EvaluateAnimatorSpeed()
    {
        Debug.Log(speedController._currentSpeed);
        if(speedController._currentSpeed == 0)
        {
            animator.speed = 0;// / currentSpeed;
            return;
        }
        else if(speedController._currentSpeed < 1) 
        {
            animator.speed = normalSpeedforAnimation / 1;// / currentSpeed;
            return;
        }
        animator.speed = normalSpeedforAnimation / speedController._currentSpeed;// / currentSpeed;

    }
}
