using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelHolder", menuName = "Scriptable Objects/LevelHolder")]
public class LevelHolder : ScriptableObject
{
    public List<LevelInfo> levels = new List<LevelInfo>();

}
