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
    //public GameObject emuHead;
    //public VisualEffect deathVFX;
    public float normalSpeedforAnimation = 3f;
    public void Awake()
    {
        laserVFX.gameObject?.SetActive(false);
    }
    public void Update()
    {
        healthSystem.RegenHealthTick();
        EvaluateAnimatorSpeed();
    }
    private void EvaluateAnimatorSpeed()
    {
        animator.SetFloat("Speed", speedController._currentSpeed / normalSpeedforAnimation);
    }
}
