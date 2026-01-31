using UnityEngine;

[System.Serializable]
public class LevelInfo
{
    public int levelID;
    public string levelName;

    [Header("Masks for this Level")]
    public HeadMask mask1;
    public HeadMask mask2;
    public HeadMask mask3;

    public SceneField sceneToLoad;

    public static LevelInfo currentLevel;
}
