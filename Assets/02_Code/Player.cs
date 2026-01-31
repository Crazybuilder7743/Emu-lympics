using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerHealth healthSystem = new PlayerHealth();
    public PlayerMaskManager maskmanager;
    public PlayerSplitScreenInfo playerSplitScreenInfo;
    public void Awake()
    {
        maskmanager = new(this);
    }
    public void Update()
    {
        healthSystem.RegenHealthTick();
    }
}
