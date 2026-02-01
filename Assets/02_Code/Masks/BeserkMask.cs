using UnityEngine;

public class BeserkMask : Mask
{
    public static int ID = 2;
    private const float DEFAULT_DAMAGE_MOD = 1f;
    private const float DEFAULT_SPEED_MOD = 1f;
    public float damageModifier = 2;
    public float speedModifier = 2f;
    public override void Activate(Player player)
    {
        player.healthSystem.damageMod = damageModifier;
        player.speedController.AdjustSpeedMultiplier(speedModifier);
    }

    public override void Deactivate(Player player)
    {
        player.healthSystem.damageMod = DEFAULT_DAMAGE_MOD;
        player.speedController.ResetSpeedMultiplier();
    }

    public override int GetID()
    {
        return ID;  
    }
}
