using UnityEngine;

public class TurtleMask : Mask
{
    public static int ID = 1;
    private const float DEFAULT_REGEN_MOD = 1f;
    private const float DEFAULT_SPEED_MOD = 1f;
    public float healthRegenmodifier = 5;
    public float speedModifier = 0.75f;
    public override void Activate(Player player)
    {
        player.healthSystem.healthRegenModifier = healthRegenmodifier;
        //todo: set speed mod
    }

    public override void Deactivate(Player player)
    {
        player.healthSystem.healthRegenModifier = DEFAULT_REGEN_MOD;
        //todo: set speed mod
    }

    public override int GetID()
    {
        return ID;
    }
}
