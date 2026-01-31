using Unity.Mathematics;
using UnityEngine;
using UnityEngineInternal;

public class InitLevel : MonoBehaviour
{
    private const float DEFAULT_LEVEL_OFFFSET = 50;
    [SerializeField] Vector3 levelOffset = Vector3.down*DEFAULT_LEVEL_OFFFSET;
    [SerializeField] Level levelPrefab;
    (Level level, Player player) player1 = new();
    (Level level, Player player) player2 = new();
    public static Player player1obj;
    public static Player player2obj;
    [SerializeField] RenderTexture player1Screen;
    [SerializeField] RenderTexture player2Screen;
    [SerializeField] Player playerPrefab;

    [SerializeField] UIController_HUD _HUD;
    private void InitGame() 
    {
        SetUpPlayer(ref player1,false);
        player1.player.playerSplitScreenInfo.ownCamera.targetTexture = player1Screen;
        SetUpPlayer(ref player2, true);
        player2.player.playerSplitScreenInfo.ownCamera.targetTexture = player2Screen;
        player1obj = player1.player;
        player2obj = player2.player;

        _HUD.InitUI();
    }
    public void Start()
    {
        InitGame();
    }
    private void SetUpPlayer(ref (Level level, Player player) player, bool offsetUp) 
    {
        int tmpMod = offsetUp ? -1 : 1;
        player.level = Instantiate(levelPrefab,levelOffset*tmpMod, Quaternion.identity);
        player.player = Instantiate(playerPrefab);
        player.player.transform.position = player.level.playerRunRail[0].Position +  new float3(0,player.level.transform.position.y,0);
        player.player.transform.rotation = player.level.playerRunRail[0].Rotation;
    }
}
