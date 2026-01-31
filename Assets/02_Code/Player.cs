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
    //public VisualEffect laserVFX;
    public SplineAnimate splineAnimator;
    public PlayerMovement movement;
    public SplineSpeedController speedController;
    public void Awake()
    {
        //laserVFX.gameObject?.SetActive(false);
    }
    public void Update()
    {
        healthSystem.RegenHealthTick();
    }
}
