using System.Collections.Generic;
using UnityEngine;

public class PlayerMaskManager : MonoBehaviour
{
    public static float switchCooldown = 1f;
    private static float lastMaskChange = float.MinValue;
    List<Mask> masks = new List<Mask>();
    public delegate void OnMaskChange(Mask mask);
    public event OnMaskChange maskChange;
    private int currentID = 0;
    public Mask CurrentMask => masks[currentID];
    public void Init(IEnumerable<int> maskIDs) 
    {
        foreach (int maskID in maskIDs) 
        {
            if (!AllMasks.Masks.ContainsKey(maskID)) 
            {
                continue;
            }
            masks.Add(AllMasks.Masks[maskID]);
        }
    }
    public Mask ChangeMask(int maskID) 
    {
        if(Time.time - lastMaskChange< switchCooldown) 
        {
            return CurrentMask;
        }
        lastMaskChange = Time.time;
        foreach (Mask mask in masks) 
        {
            if(mask.GetID() == maskID) 
            {
                CurrentMask.Deactivate();
                currentID = maskID;
                maskChange?.Invoke(mask);
                CurrentMask.Activate();
            }
        }
        return CurrentMask;
    }
}
