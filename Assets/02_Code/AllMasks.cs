using System.Collections.Generic;
using UnityEngine;

public static class AllMasks
{
    private static bool initzialed;
    public static Dictionary<int,Mask> Masks { get {
            if (!initzialed) 
            {
                Init();
            }
            return masks; 
        }
        set {  masks = value; }
    }
    private static Dictionary<int,Mask> masks = new();

    private static void Init() 
    {
        initzialed = true;
        masks.Add(GravitationMask.ID,new GravitationMask());
        masks.Add(GasMask.ID,new GasMask());
        masks.Add(TurtleMask.ID,new TurtleMask());
        masks.Add(BeserkMask.ID,new BeserkMask());
        masks.Add(SupermanMask.ID,new SupermanMask());
    }
}
