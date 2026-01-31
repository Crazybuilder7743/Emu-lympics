using System.Collections.Generic;
using UnityEngine;

public class PlayerMaskManager
{
    public static float switchCooldown = 1f;
    public float CurrentMaskChangeCooldown => Mathf.Clamp(Time.time - lastMaskChange,0,switchCooldown)/switchCooldown;
    private float lastMaskChange = float.MinValue;
    List<Mask> masks = new List<Mask>();
    public delegate void OnMaskChange(int id);
    public event OnMaskChange maskChange;
    public int currentID = 0;
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
        int i = 0;
        foreach (Mask mask in masks) 
        {
            if(mask.GetID() == maskID) 
            {
                CurrentMask.Deactivate();
                currentID = i;
                maskChange?.Invoke(currentID);
                CurrentMask.Activate();
            }
            i++;
        }
        return CurrentMask;
    }
}
