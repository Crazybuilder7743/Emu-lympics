using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIController_HUD : MonoBehaviour
{
    private UIDocument _uiDoc;
    private VisualElement _root;

    private VisualElement _p1MaskContainer;
    private VisualElement _p1Mask1;
    private VisualElement _p1CDMask1;
    private VisualElement _p1Mask2;
    private VisualElement _p1CDMask2;
    private VisualElement _p1Mask3;
    private VisualElement _p1CDMask3;

    private VisualElement _p2MaskContainer;
    private VisualElement _p2Mask1;
    private VisualElement _p2CDMask1;
    private VisualElement _p2Mask2;
    private VisualElement _p2CDMask2;
    private VisualElement _p2Mask3;
    private VisualElement _p2CDMask3;

    [SerializeField] private LevelInfo _currentDebugLevel;

    private static int MASK_HEIGHT = 229;

    private bool _hasP1SwitchedMaskRecently;
    private bool _isP1Mask1OnCD;
    private bool _isP1Mask2OnCD;
    private bool _isP1Mask3OnCD;

    private float _p1CD = 1;
    private float _p1PercentCD = 0;
    private float _currentP1CD = 0;
    private int _p1CurrentPixelValue = 0;


    private void Awake()
    {
        _uiDoc = GetComponent<UIDocument>();
        _root = _uiDoc.rootVisualElement;

        RegisterCallbacks();
    }

    public void InitUI()
    {
        _currentDebugLevel = LevelInfo.currentLevel;
        InitLevel.player1obj.maskmanager.maskChange += OnP1MaskChanged;
    }

    private void Update()
    {
        if (_hasP1SwitchedMaskRecently)
        {
            //_p1CDMask1.style.display = DisplayStyle.Flex;

            _currentP1CD = InitLevel.player1obj.maskmanager.CurrentMaskChangeCooldown;
            //Debug.Log("currentSignatureCooldown = " + _currentSignatureCooldown);
            //Debug.Log("SignatureCooldown = " + _signatureCooldown);
            _p1PercentCD = 1 - (_currentP1CD / _p1CD);
            //Debug.Log("cooldownPercent = " + _percentCooldown);
            _p1CurrentPixelValue = MASK_HEIGHT - (Mathf.FloorToInt(_p1PercentCD * MASK_HEIGHT));
            //Debug.Log("currentPixelValue = " + _currentPixelValue);

            if(_isP1Mask1OnCD)
            {
                _p1CDMask1.style.height = _p1CurrentPixelValue;
            }

            if (_isP1Mask2OnCD)
            {
                _p1CDMask2.style.height = _p1CurrentPixelValue;
            }

            if (_isP1Mask3OnCD)
            {
                _p1CDMask3.style.height = _p1CurrentPixelValue;
            }


            if (_p1CurrentPixelValue <= 0)
            {
                _isP1Mask1OnCD = false;
                _isP1Mask2OnCD = false;
                _isP1Mask3OnCD = false;
                _hasP1SwitchedMaskRecently = false;
            }


            
        }
    }

    private void OnP1MaskChanged(Mask mask)
    {
        _hasP1SwitchedMaskRecently = true;

        throw new System.NotImplementedException();
    }

    private void RegisterCallbacks()
    {
        _p1MaskContainer = _root.Q<VisualElement>("P1MaskContainer");

        _p1Mask1 = _p1MaskContainer.Q<VisualElement>("P1Mask1");
        _p1CDMask1 = _p1Mask1.Q<VisualElement>("CooldownMask");
        _p1CDMask1.style.unityBackgroundImageTintColor = LevelInfo.currentLevel.mask1.maskColor;

        _p1Mask2 = _p1MaskContainer.Q<VisualElement>("P1Mask2");
        _p1CDMask2 = _p1Mask2.Q<VisualElement>("CooldownMask");
        _p1CDMask2.style.unityBackgroundImageTintColor = LevelInfo.currentLevel.mask2.maskColor;

        _p1Mask3 = _p1MaskContainer.Q<VisualElement>("P1Mask3");
        _p1CDMask3 = _p1Mask3.Q<VisualElement>("CooldownMask");
        _p1CDMask3.style.unityBackgroundImageTintColor = LevelInfo.currentLevel.mask3.maskColor;


        _p2MaskContainer = _root.Q<VisualElement>("P2MaskContainer");

        _p2Mask1 = _p2MaskContainer.Q<VisualElement>("P2Mask1");
        _p2CDMask1 = _p2Mask1.Q<VisualElement>("CooldownMask");
        _p2CDMask1.style.unityBackgroundImageTintColor = LevelInfo.currentLevel.mask1.maskColor;

        _p2Mask2 = _p2MaskContainer.Q<VisualElement>("P2Mask2");
        _p2CDMask2 = _p2Mask2.Q<VisualElement>("CooldownMask");
        _p2CDMask2.style.unityBackgroundImageTintColor = LevelInfo.currentLevel.mask2.maskColor;

        _p2Mask3 = _p2MaskContainer.Q<VisualElement>("P2Mask3");
        _p2CDMask3 = _p2Mask3.Q<VisualElement>("CooldownMask");
        _p2CDMask3.style.unityBackgroundImageTintColor = LevelInfo.currentLevel.mask3.maskColor;

        

    }




}
