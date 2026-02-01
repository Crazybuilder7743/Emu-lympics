using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIController_HUD : MonoBehaviour
{
    private UIDocument _uiDoc;
    private VisualElement _root;

    public static UIController_HUD instance { get; private set; }

    private VisualElement _p1MaskContainer;
    private VisualElement _p1Mask1;
    private VisualElement _p1CDMask1;
    private VisualElement _p1EyesMask1;
    private VisualElement _p1Mask2;
    private VisualElement _p1CDMask2;
    private VisualElement _p1EyesMask2;
    private VisualElement _p1Mask3;
    private VisualElement _p1CDMask3;
    private VisualElement _p1EyesMask3;

    private VisualElement _p1HealthbarContainer;
    private ProgressBar _p1Healthbar;

    private VisualElement _p2HealthbarContainer;
    private ProgressBar _p2Healthbar;

    private VisualElement _p2MaskContainer;
    private VisualElement _p2Mask1;
    private VisualElement _p2CDMask1;
    private VisualElement _p2EyesMask1;
    private VisualElement _p2Mask2;
    private VisualElement _p2CDMask2;
    private VisualElement _p2EyesMask2;
    private VisualElement _p2Mask3;
    private VisualElement _p2CDMask3;
    private VisualElement _p2EyesMask3;

    [SerializeField] private LevelInfo _currentDebugLevel;

    private static int MASK_HEIGHT = 229;

    private bool _hasP1SwitchedMaskRecently = false;
    private bool _isP1Mask1OnCD = false;
    private bool _isP1Mask2OnCD = false;
    private bool _isP1Mask3OnCD = false;

    private float _p1CD = 1;
    private float _p1PercentCD = 0;
    private float _currentP1CD = 0;
    private int _p1CurrentPixelValue = 0;

    private bool _hasP2SwitchedMaskRecently = false;
    private bool _isP2Mask1OnCD = false;
    private bool _isP2Mask2OnCD = false;
    private bool _isP2Mask3OnCD = false;

    private float _p2CD = 1;
    private float _p2PercentCD = 0;
    private float _currentP2CD = 0;
    private int _p2CurrentPixelValue = 0;

    private VisualElement _startButton;
    private VisualElement _countdownContainer;
    private Label _countdown;



    private bool _isUIInitialized = false;


    private void Awake()
    {
        _uiDoc = GetComponent<UIDocument>();
        _root = _uiDoc.rootVisualElement;

        if (instance != null)
        {
            Destroy(gameObject);
            Debug.Log("found more than one UIController_HUD in the scene");
            return;
        }
        instance = this;

        RegisterCallbacks();
    }

    public void InitUI()
    {
        _currentDebugLevel = LevelInfo.currentLevel;
        InitLevel.player1obj.maskmanager.maskChange += OnP1MaskChanged;
        InitLevel.player2obj.maskmanager.maskChange += OnP2MaskChanged;

        _p1Healthbar.highValue = InitLevel.player1obj.healthSystem.maxHealth;
        _p2Healthbar.highValue = InitLevel.player2obj.healthSystem.maxHealth;

        _isUIInitialized = true;
        //TestMyUI();
    }

    private void Update()
    {
        if (_hasP1SwitchedMaskRecently)
        {
            //_p1CDMask1.style.display = DisplayStyle.Flex;

            _currentP1CD = InitLevel.player1obj.maskmanager.CurrentMaskChangeCooldown;
            //Debug.Log("currentP1Cooldown = " + _currentP1CD);
            //Debug.Log("P1Cooldown = " + _p1CD);
            _p1PercentCD = 1 - (_currentP1CD / _p1CD);
            //Debug.Log("cooldownPercent = " + _p1PercentCD);
            _p1CurrentPixelValue = MASK_HEIGHT - (Mathf.FloorToInt(_p1PercentCD * MASK_HEIGHT));
            //Debug.Log("currentPixelValue = " + _p1CurrentPixelValue);

            if (_isP1Mask1OnCD)
            {
                _p1CDMask2.style.height = _p1CurrentPixelValue;
                _p1CDMask3.style.height = _p1CurrentPixelValue;
            }

            if (_isP1Mask2OnCD)
            {
                _p1CDMask1.style.height = _p1CurrentPixelValue;
                _p1CDMask3.style.height = _p1CurrentPixelValue;
            }

            if (_isP1Mask3OnCD)
            {

                _p1CDMask1.style.height = _p1CurrentPixelValue;
                _p1CDMask2.style.height = _p1CurrentPixelValue;
            }


            if (_p1CurrentPixelValue >= MASK_HEIGHT)
            {
                _isP1Mask1OnCD = false;
                _isP1Mask2OnCD = false;
                _isP1Mask3OnCD = false;
                _hasP1SwitchedMaskRecently = false;
            }



        }

        if (_hasP2SwitchedMaskRecently)
        {
            //_p1CDMask1.style.display = DisplayStyle.Flex;

            _currentP2CD = InitLevel.player2obj.maskmanager.CurrentMaskChangeCooldown;
            //Debug.Log("currentP1Cooldown = " + _currentP1CD);
            //Debug.Log("P1Cooldown = " + _p1CD);
            _p2PercentCD = 1 - (_currentP2CD / _p2CD);
            //Debug.Log("cooldownPercent = " + _p1PercentCD);
            _p2CurrentPixelValue = MASK_HEIGHT - (Mathf.FloorToInt(_p2PercentCD * MASK_HEIGHT));
            //Debug.Log("currentPixelValue = " + _p1CurrentPixelValue);

            if (_isP2Mask1OnCD)
            {
                _p2CDMask2.style.height = _p2CurrentPixelValue;
                _p2CDMask3.style.height = _p2CurrentPixelValue;
            }

            if (_isP2Mask2OnCD)
            {
                _p2CDMask1.style.height = _p2CurrentPixelValue;
                _p2CDMask3.style.height = _p2CurrentPixelValue;
            }

            if (_isP2Mask3OnCD)
            {

                _p2CDMask1.style.height = _p2CurrentPixelValue;
                _p2CDMask2.style.height = _p2CurrentPixelValue;
            }


            if (_p2CurrentPixelValue >= MASK_HEIGHT)
            {
                _isP2Mask1OnCD = false;
                _isP2Mask2OnCD = false;
                _isP2Mask3OnCD = false;
                _hasP2SwitchedMaskRecently = false;
            }



        }



        if (_isUIInitialized)
        {
            UpdateHealthbars();
        }

    }

    private void OnP1MaskChanged(int index)
    {

        switch (index)
        {
            case 0:
                _isP1Mask1OnCD = true;
                InitLevel.player1obj.colorCoordinater.ChangeMaskColor(LevelInfo.currentLevel.mask1);

                _p1EyesMask1.style.visibility = Visibility.Visible;
                _p1EyesMask2.style.visibility = Visibility.Hidden;
                _p1EyesMask3.style.visibility = Visibility.Hidden;
                break;
            case 1:
                _isP1Mask2OnCD = true;
                InitLevel.player1obj.colorCoordinater.ChangeMaskColor(LevelInfo.currentLevel.mask2);

                _p1EyesMask1.style.visibility = Visibility.Hidden;
                _p1EyesMask2.style.visibility = Visibility.Visible;
                _p1EyesMask3.style.visibility = Visibility.Hidden;
                break;
            case 2:
                _isP1Mask3OnCD = true;
                InitLevel.player1obj.colorCoordinater.ChangeMaskColor(LevelInfo.currentLevel.mask3);

                _p1EyesMask1.style.visibility = Visibility.Hidden;
                _p1EyesMask2.style.visibility = Visibility.Hidden;
                _p1EyesMask3.style.visibility = Visibility.Visible;
                break;
        }

        _hasP1SwitchedMaskRecently = true;
    }

    private void OnP2MaskChanged(int index)
    {

        switch (index)
        {
            case 0:
                _isP2Mask1OnCD = true;
                InitLevel.player2obj.colorCoordinater.ChangeMaskColor(LevelInfo.currentLevel.mask1);
                
                _p2EyesMask1.style.visibility = Visibility.Visible;
                _p2EyesMask2.style.visibility = Visibility.Hidden;
                _p2EyesMask3.style.visibility = Visibility.Hidden;
                break;
            case 1:
                _isP2Mask2OnCD = true;
                InitLevel.player2obj.colorCoordinater.ChangeMaskColor(LevelInfo.currentLevel.mask2);

                _p2EyesMask1.style.visibility = Visibility.Hidden;
                _p2EyesMask2.style.visibility = Visibility.Visible;
                _p2EyesMask3.style.visibility = Visibility.Hidden;
                break;
            case 2:
                _isP2Mask3OnCD = true;
                InitLevel.player2obj.colorCoordinater.ChangeMaskColor(LevelInfo.currentLevel.mask3);

                _p2EyesMask1.style.visibility = Visibility.Hidden;
                _p2EyesMask2.style.visibility = Visibility.Hidden;
                _p2EyesMask3.style.visibility = Visibility.Visible;
                break;
        }

        _hasP2SwitchedMaskRecently = true;
    }

    private void RegisterCallbacks()
    {
        _p1MaskContainer = _root.Q<VisualElement>("P1MaskContainer");

        _p1Mask1 = _p1MaskContainer.Q<VisualElement>("P1Mask1");
        _p1CDMask1 = _p1Mask1.Q<VisualElement>("CooldownMask");
        _p1CDMask1.style.unityBackgroundImageTintColor = LevelInfo.currentLevel.mask1.maskColor;
        _p1EyesMask1 = _p1Mask1.Q<VisualElement>("Eyes");

        _p1Mask2 = _p1MaskContainer.Q<VisualElement>("P1Mask2");
        _p1CDMask2 = _p1Mask2.Q<VisualElement>("CooldownMask");
        _p1CDMask2.style.unityBackgroundImageTintColor = LevelInfo.currentLevel.mask2.maskColor;
        _p1EyesMask2 = _p1Mask2.Q<VisualElement>("Eyes");

        _p1Mask3 = _p1MaskContainer.Q<VisualElement>("P1Mask3");
        _p1CDMask3 = _p1Mask3.Q<VisualElement>("CooldownMask");
        _p1CDMask3.style.unityBackgroundImageTintColor = LevelInfo.currentLevel.mask3.maskColor;
        _p1EyesMask3 = _p1Mask3.Q<VisualElement>("Eyes");


        _p1HealthbarContainer = _root.Q<VisualElement>("P1HealthbarContainer");
        _p1Healthbar = _p1HealthbarContainer.Q<ProgressBar>("Healthbar");


        _p2HealthbarContainer = _root.Q<VisualElement>("P2HealthbarContainer");
        _p2Healthbar = _p2HealthbarContainer.Q<ProgressBar>("Healthbar");



        _p2MaskContainer = _root.Q<VisualElement>("P2MaskContainer");

        _p2Mask1 = _p2MaskContainer.Q<VisualElement>("P2Mask1");
        _p2CDMask1 = _p2Mask1.Q<VisualElement>("CooldownMask");
        _p2CDMask1.style.unityBackgroundImageTintColor = LevelInfo.currentLevel.mask1.maskColor;
        _p2EyesMask1 = _p2Mask1.Q<VisualElement>("Eyes");

        _p2Mask2 = _p2MaskContainer.Q<VisualElement>("P2Mask2");
        _p2CDMask2 = _p2Mask2.Q<VisualElement>("CooldownMask");
        _p2CDMask2.style.unityBackgroundImageTintColor = LevelInfo.currentLevel.mask2.maskColor;
        _p2EyesMask2 = _p2Mask2.Q<VisualElement>("Eyes");

        _p2Mask3 = _p2MaskContainer.Q<VisualElement>("P2Mask3");
        _p2CDMask3 = _p2Mask3.Q<VisualElement>("CooldownMask");
        _p2CDMask3.style.unityBackgroundImageTintColor = LevelInfo.currentLevel.mask3.maskColor;
        _p2EyesMask3 = _p2Mask3.Q<VisualElement>("Eyes");



        _startButton = _root.Q<VisualElement>("StartButton");
        _startButton.RegisterCallback<ClickEvent>(OnStartButtonClicked);

        _countdownContainer = _root.Q<VisualElement>("CountdownContainer");
        _countdown = _root.Q<Label>("Countdown");
        
    }

    private void OnStartButtonClicked(ClickEvent eventArgs)
    {
        PlayCountdown();
        _startButton.style.display = DisplayStyle.None;
    }

    private async void PlayCountdown()
    {
        _countdownContainer.style.display = DisplayStyle.Flex;
        _countdown.text = "3";
        await Task.Delay(1000);
        _countdown.text = "2";
        await Task.Delay(1000);
        _countdown.text = "1";
        await Task.Delay(1000);
        _countdownContainer.style.display = DisplayStyle.None;
        InitLevel.Instance.StartGane();

    }

    private void UpdateHealthbars()
    {
        _p1Healthbar.value = InitLevel.player1obj.healthSystem.currentHealth;
        _p2Healthbar.value = InitLevel.player2obj.healthSystem.currentHealth;
    }



    private async void TestMyUI()
    {
        //await Task.Delay(5000);
        //InitLevel.player1obj.maskmanager.ChangeMask(1);
        //Debug.Log("Change P1 Mask to 1");
        //await Task.Delay(3000);
        //InitLevel.player2obj.maskmanager.ChangeMask(2);
        //Debug.Log("Change P2 Mask to 2");
        //await Task.Delay(3000);
        //InitLevel.player1obj.maskmanager.ChangeMask(0);
        //Debug.Log("Change P1 Mask to 0");
    }

}
