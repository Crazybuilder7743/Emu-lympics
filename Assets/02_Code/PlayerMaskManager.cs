using System.Collections.Generic;
using UnityEngine;

public class PlayerMaskManager
{
    private Player owner;
    public static float switchCooldown = 1f;
    public float CurrentMaskChangeCooldown => Mathf.Clamp(Time.time - lastMaskChange, 0, switchCooldown) / switchCooldown;
    private float lastMaskChange = float.MinValue;
    List<Mask> masks = new List<Mask>();
    public delegate void OnMaskChange(int id);
    public event OnMaskChange maskChange;
    public int currentID = 0;
    public Mask CurrentMask => masks[currentID];

    public PlayerMaskManager(Player owner) {  this.owner = owner; }
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
        maskChange?.Invoke(currentID);
    }


    public Mask ChangeMask(bool nextMask) 
    {
        if (Time.time - lastMaskChange < switchCooldown)
        {
            return CurrentMask;
        }
        lastMaskChange = Time.time;
        CurrentMask.Deactivate(owner);
        if (nextMask) 
        {
            currentID++;
            currentID = currentID>=masks.Count ? 0 : currentID;
        }
        else 
        {
            currentID--;
            currentID = currentID <0? masks.Count-1 : currentID;
        }
        maskChange?.Invoke(currentID);
        CurrentMask.Activate(owner);
        return CurrentMask;
    
    }
    public Mask ChangeMask(int maskID)
    {
        if (Time.time - lastMaskChange < switchCooldown ||maskID ==currentID)
        {
            return CurrentMask;
        }
        lastMaskChange = Time.time;
        CurrentMask.Deactivate(owner);
        currentID = maskID;
        maskChange?.Invoke(currentID);
        CurrentMask.Activate(owner);

        return CurrentMask;
    }
}
