using System.Threading.Tasks;
using UnityEngine;

public class GravitationMask : Mask
{
    private static bool flippedGravity = false;
    public static int ID = 0;
    public float effectDuration;
    public override void Activate()
    {
        //TODO : Implement gravitation switch and rotate player
    }
    public override void Deactivate()
    {
        //RestoreNormalGravity
    }

    public override int GetID()
    {
        return ID;
    }
}
