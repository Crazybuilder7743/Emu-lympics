using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIController_LevelSelection : MonoBehaviour
{
    private UIDocument _uiDoc;
    private VisualElement _root;

    private ScrollView _scrollViewLevelSelection;
    [SerializeField] private LevelHolder _levelHolder;


    [SerializeField] private VisualTreeAsset _baseLevelSlot;

    private void Awake()
    {
        InitUI();
    }

    public void InitUI()
    {
        _uiDoc = GetComponent<UIDocument>();
        _root = _uiDoc.rootVisualElement;

        RefreshScrollViewLevelSelection();

        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;

    }


    public void RefreshScrollViewLevelSelection()
    {
        _scrollViewLevelSelection = _root.Q<ScrollView>("LevelList");

        _scrollViewLevelSelection.Clear();


        var wrapContainer = new VisualElement();
        wrapContainer.style.flexDirection = FlexDirection.Column;
        _scrollViewLevelSelection.Add(wrapContainer);

        int itemsPerRow = 4;
        int counter = 0;

        VisualElement currentRow = null;

        //foreach (var player in _testPlayer)
        foreach (var level in _levelHolder.levels)
        {

            if (counter % itemsPerRow == 0)
            {
                currentRow = new VisualElement();
                currentRow.style.flexDirection = FlexDirection.Row;
                wrapContainer.Add(currentRow);
            }



            var element = _baseLevelSlot.CloneTree();

            Label levelName = element.Q<Label>("LevelName");
            VisualElement mask1 = element.Q<VisualElement>("Mask1");
            VisualElement mask2 = element.Q<VisualElement>("Mask2");
            VisualElement mask3 = element.Q<VisualElement>("Mask3");



            if (levelName != null)
            {
                levelName.text = level.levelName;
            }

            //if (profilePic != null)
            //{

            //    //profilePic.style.backgroundImage = texture;
            //}

            if (mask1 != null && level.mask1 != null)
            {
                mask1.style.unityBackgroundImageTintColor = level.mask1.maskColor;
            }

            if (mask2 != null && level.mask2 != null)
            {
                mask2.style.unityBackgroundImageTintColor = level.mask2.maskColor;
            }

            if (mask3 != null && level.mask3 != null)
            {
                mask3.style.unityBackgroundImageTintColor = level.mask3.maskColor;
            }

            if(level.sceneToLoad != null)
            {
                element.RegisterCallback<ClickEvent>(evt =>
                {
                    OnLevelClicked(level);

                });
            }





            element.userData = level;


            //element.style.scale = new Scale(new Vector2(1.3f, 1.3f));

            currentRow.Add(element);

            counter++;

        }

        //_playerList = _scrollViewLevelSelection.contentContainer;
    }

    private void OnLevelClicked(LevelInfo level)
    {
        LevelInfo.currentLevel = level;
        SceneManager.LoadScene(level.sceneToLoad);
    }
}
