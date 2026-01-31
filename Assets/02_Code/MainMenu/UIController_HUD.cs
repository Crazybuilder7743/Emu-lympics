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
    private VisualElement _p1Mask2;
    private VisualElement _p1Mask3;

    private VisualElement _p2MaskContainer;



    private void Awake()
    {
        _uiDoc = GetComponent<UIDocument>();
        _root = _uiDoc.rootVisualElement;

        RegisterCallbacks();
    }

    private void Start()
    {

    }

    private void RegisterCallbacks()
    {
        _p1MaskContainer = _root.Q<VisualElement>("P1MaskContainer");
        _p1Mask1 = _p1MaskContainer.Q<VisualElement>("P1Mask1");
        _p1Mask2 = _p1MaskContainer.Q<VisualElement>("P1Mask2");
        _p1Mask3 = _p1MaskContainer.Q<VisualElement>("P1Mask3");


        _p2MaskContainer = _root.Q<VisualElement>("P2MaskContainer");


    }


}
