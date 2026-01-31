using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIController_MainMenu : MonoBehaviour
{
    private UIDocument _uiDoc;
    private VisualElement _root;

    private VisualElement _levelsButton;
    private VisualElement _exitButton;

    [SerializeField] private SceneField _levelSelectionScene;



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
        _levelsButton = _root.Q<VisualElement>("LevelsButton");
        _levelsButton.RegisterCallback<ClickEvent>(OnLevelsClicked);

        _exitButton = _root.Q<VisualElement>("ExitButton");
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
        SceneManager.LoadScene(_levelSelectionScene);

    }

}
