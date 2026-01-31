using Unity.Mathematics;
using UnityEngine;
using UnityEngineInternal;

public class InitLevel : MonoBehaviour
{
    private const float DEFAULT_LEVEL_OFFFSET = 50;
    [SerializeField] Vector3 levelOffset = Vector3.down*DEFAULT_LEVEL_OFFFSET;
    [SerializeField] Level levelPrefab;
    (Level level, PlayerSplitScreenInfo player) player1 = new();
    [SerializeField] RenderTexture player1Screen;
    [SerializeField] RenderTexture player2Screen;
    (Level level, PlayerSplitScreenInfo player) player2 = new();
    [SerializeField] PlayerSplitScreenInfo playerPrefab;

    private void InitGame() 
    {
        SetUpPlayer(ref player1,false);
        player1.player.ownCamera.targetTexture = player1Screen;
        SetUpPlayer(ref player2, true);
        player2.player.ownCamera.targetTexture = player2Screen;
    
    }
    public void Start()
    {
        InitGame();
    }
    private void SetUpPlayer(ref (Level level, PlayerSplitScreenInfo player) player, bool offsetUp) 
    {
        int tmpMod = offsetUp ? -1 : 1;
        player.level = Instantiate(levelPrefab,levelOffset*tmpMod, Quaternion.identity);
        player.player = Instantiate(playerPrefab);
        player.player.transform.position = player.level.playerRunRail[0].Position +  new float3(0,player.level.transform.position.y,0);
        player.player.transform.rotation = player.level.playerRunRail[0].Rotation;
    }
}
