using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngineInternal;

public class InitLevel : MonoBehaviour
{
    public static InitLevel Instance;
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
    private bool initBruv = false;
    public delegate void OnWin(Player playerWhoWon, bool playerOne);
    public OnWin onWin;
    private void InitGame() 
    {
        initBruv = true;
        SetUpPlayer(ref player1,false);
        player1.player.playerSplitScreenInfo.ownCamera.targetTexture = player1Screen;
        SetUpPlayer(ref player2, true);
        player2.player.playerSplitScreenInfo.ownCamera.targetTexture = player2Screen;
        List<int> maskIDS = new List<int>();
        maskIDS.Add(LevelInfo.currentLevel.mask1.maskID);
        maskIDS.Add(LevelInfo.currentLevel.mask2.maskID);
        maskIDS.Add(LevelInfo.currentLevel.mask3.maskID);
        player1.player.maskmanager = new(player1.player) ;
        player2.player.maskmanager = new(player2.player) ;
        player1.player.maskmanager.Init(maskIDS) ;
        player2.player.maskmanager.Init(maskIDS) ;
        PlayerInput.Instance.Player1ChooseMask1Input += SwitchToMask1ForPlayer1;
        PlayerInput.Instance.Player2ChooseMask1Input += SwitchToMask1ForPlayer2;
        PlayerInput.Instance.Player1ChooseMask2Input += SwitchToMask2ForPlayer1;
        PlayerInput.Instance.Player2ChooseMask2Input += SwitchToMask2ForPlayer2;
        PlayerInput.Instance.Player1ChooseMask3Input += SwitchToMask3ForPlayer1;
        PlayerInput.Instance.Player2ChooseMask3Input += SwitchToMask3ForPlayer2;
        PlayerInput.Instance.Player1NextMaskInput += Player1NextMask;
        PlayerInput.Instance.Player1PrevMaskInput += Player1PrevMask;
        PlayerInput.Instance.Player2NextMaskInput += Player2NextMask;
        PlayerInput.Instance.Player2PrevMaskInput += Player2PrevMask;
        
        player1obj = player1.player;
        player2obj = player2.player;

        UIController_HUD.instance.InitUI();
    }
    private void Awake()
    {
        if(Instance==null|| Instance == this) 
        {
            Instance = this;
            return;
        }
        Destroy(this);

    }
    public void OnDestroy()
    {
        if (!initBruv)
        {
            return;
        }
        PlayerInput.Instance.Player1ChooseMask1Input -= SwitchToMask1ForPlayer1;
        PlayerInput.Instance.Player2ChooseMask1Input -= SwitchToMask1ForPlayer2;
        PlayerInput.Instance.Player1ChooseMask2Input -= SwitchToMask2ForPlayer1;
        PlayerInput.Instance.Player2ChooseMask2Input -= SwitchToMask2ForPlayer2;
        PlayerInput.Instance.Player1ChooseMask3Input -= SwitchToMask3ForPlayer1;
        PlayerInput.Instance.Player2ChooseMask3Input -= SwitchToMask3ForPlayer2;
        PlayerInput.Instance.Player1NextMaskInput -= Player1NextMask;
        PlayerInput.Instance.Player1PrevMaskInput -= Player1PrevMask;
        PlayerInput.Instance.Player2NextMaskInput -= Player2NextMask;
        PlayerInput.Instance.Player2PrevMaskInput -= Player2PrevMask;
    }

    private void Player1PrevMask()
    {
        player1.player.maskmanager.ChangeMask(false);
    }

    private void Player1NextMask()
    {
        player1.player.maskmanager.ChangeMask(true);
    }private void Player2PrevMask()
    {
        player2.player.maskmanager.ChangeMask(false);
    }

    private void Player2NextMask()
    {
        player2.player.maskmanager.ChangeMask(true);
    }

    private void SwitchToMask2ForPlayer1()
    {
        player1.player.maskmanager.ChangeMask(1);
    }private void SwitchToMask3ForPlayer1()
    {
        player1.player.maskmanager.ChangeMask(2);
    }

    private void SwitchToMask1ForPlayer1()
    {
        player1.player.maskmanager.ChangeMask(0);
    }private void SwitchToMask2ForPlayer2()
    {
        player2.player.maskmanager.ChangeMask(1);
    }private void SwitchToMask3ForPlayer2()
    {
        player2.player.maskmanager.ChangeMask(2);
    }

    private void SwitchToMask1ForPlayer2()
    {
        player2.player.maskmanager.ChangeMask(0);
    }

    public void Start()
    {
        InitGame();
    }

    public void StartGane() 
    {
        player1.player.speedController.Init();
        player2.player.speedController.Init();
        player1.player.movement.Init(true);
        player2.player.movement.Init(false);
        player1.player.splineAnimator.Play();
        player2.player.splineAnimator.Play();
        player1.player.maskmanager.CurrentMask.Activate(player1.player);
        player2.player.maskmanager.CurrentMask.Activate(player2.player);

    }
    private void SetUpPlayer(ref (Level level, Player player) player, bool offsetUp) 
    {
        int tmpMod = offsetUp ? -1 : 1;
        player.level = Instantiate(levelPrefab,levelOffset*tmpMod, Quaternion.identity);
        player.player = Instantiate(playerPrefab);
        player.player.splineAnimator.Container = player.level.splineContainer;
        player.player.healthSystem.ResetToFullHp();
        player.player.healthSystem.SetSplineSpeedController(player.player.speedController);
        //player.player.transform.position = player.level.playerRunRail[0].Position + new float3(0, player.level.transform.position.y + 0.5f, 0);
        //player.player.transform.rotation = player.level.playerRunRail[0].Rotation;

        Material emuMat = new Material(player.player.maskRenderer.material);
        player.player.maskRenderer.material = emuMat;
    }


    public void Win(bool playerOneWon) 
    {
        player1.player.splineAnimator.Pause();
        Destroy(player1.player.splineAnimator);
        player2.player.splineAnimator.Pause();
        Destroy(player2.player.splineAnimator);
        player1.player.maskmanager.CurrentMask.Deactivate(player1.player);
        player2.player.maskmanager.CurrentMask.Deactivate(player2.player);
        player1.player.speedController.StopImmediate();
        player2.player.speedController.StopImmediate();
        if (playerOneWon) 
        { 
            onWin?.Invoke(player1obj,true);
            player2.player.animator.SetTrigger("Dead");
            //kill player 2
        }
        else 
        {
            onWin?.Invoke(player2obj,false);
            player1.player.animator.SetTrigger("Dead");
            //kill player 1
            
        
        }
    }
}
