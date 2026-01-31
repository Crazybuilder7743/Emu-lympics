using System.Threading.Tasks;
using UnityEngine;

public class GravitationMask : Mask
{
    public new static int ID = 0;
    public float effectDuration;
    public override async void Activate()
    {
        base.Activate();
        //TODO : Implement gravitation switch and rotate player
        await Task.Delay((int) (effectDuration * 1000));
        //revert effect
    }

    public override int GetID()
    {
        return ID;
    }
}
