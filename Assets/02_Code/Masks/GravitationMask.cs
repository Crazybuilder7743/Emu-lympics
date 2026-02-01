using System.Threading.Tasks;
using UnityEngine;

public class GravitationMask : Mask
{
    public float reversedGravityMultiplier = -1f;
    private const float DEFAULT_GRAVITY_MODIFER = 11f;
    public static int ID = 0;
    public float effectDuration;

    public override void Activate(Player player)
    {
        player.movement._activeGravityMultiplier = reversedGravityMultiplier; 
        //TODO : Implement gravitation switch and rotate player
    }
    public override void Deactivate(Player player)
    {
        player.movement._activeGravityMultiplier = DEFAULT_GRAVITY_MODIFER; 
        //RestoreNormalGravity
    }

    public override int GetID()
    {
        return ID;
    }
}
