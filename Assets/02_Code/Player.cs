using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerHealth healthSystem = new PlayerHealth();
    public PlayerMaskManager maskmanager = new PlayerMaskManager();
    public PlayerSplitScreenInfo playerSplitScreenInfo;
}
