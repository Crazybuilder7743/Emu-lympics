using UnityEngine;

[CreateAssetMenu(fileName = "HeadMask", menuName = "Scriptable Objects/HeadMask"), System.Serializable]
public class HeadMask : ScriptableObject
{
    public int maskID;
    public string maskName;
    public Color maskColor;
    public string maskDesc;

}
