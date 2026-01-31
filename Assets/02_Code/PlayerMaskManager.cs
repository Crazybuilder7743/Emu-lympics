using System.Collections.Generic;
using UnityEngine;

public class PlayerMaskManager : MonoBehaviour
{
    List<Mask> masks = new List<Mask>();
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
        foreach (Mask mask in masks) 
        {
            if(mask.GetID() == maskID) 
            {
                currentID = maskID;
            }
        }
        return CurrentMask;
    }
}
