using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIController_MainMenu : MonoBehaviour
{
    private UIDocument _uiDoc;
    private VisualElement _root;

    private VisualElement _createLobbyButton;
    private VisualElement _exitButton;



    private void Awake()
    {
        _uiDoc = GetComponent<UIDocument>();
        _root = _uiDoc.rootVisualElement;

        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;

        RegisterCallbacks();
    }

    private void Start()
    {

    }

    private void RegisterCallbacks()
    {
        _createLobbyButton = _root.Q<VisualElement>("CreateLobby");
        _createLobbyButton.RegisterCallback<ClickEvent>(OnLevelsClicked);

        _exitButton = _root.Q<VisualElement>("Exit");
        _exitButton.RegisterCallback<ClickEvent>(OnExitButtonClicked);
    }
    private void OnExitButtonClicked(ClickEvent eventArgs)
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }




    private void OnLevelsClicked(ClickEvent eventArgs)
    {
  

    }

}
