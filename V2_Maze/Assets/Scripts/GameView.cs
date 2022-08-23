using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField]
    private Text TimerText;
    [SerializeField]
    private Text GlowingRocksText;
    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    private Text ClassText;
    [SerializeField]
    private Button PlayButton;
    [SerializeField]
    private Button QuitButton;
    [SerializeField]
    private Button PlayAsRunnerButton;
    [SerializeField]
    private Button PlayAsTimeLordButton;
    [SerializeField]
    private Button PlayAsRockMasterButton;
    [SerializeField]
    private GameObject MenuInterface;
    [SerializeField]
    private GameObject GameInterface;
    [SerializeField]
    private GameObject MenuPanel;
    [SerializeField]
    private GameObject ClassSelectorPanel;


    private void Start()
    {
        TimerText = GameObject.Find("TimerText").GetComponent<Text>();
        GlowingRocksText = GameObject.Find("GlowingRocksText").GetComponent<Text>();
        ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        ClassText = GameObject.Find("ClassText").GetComponent<Text>();
        PlayButton = GameObject.Find("PlayButton").GetComponent<Button>();
        QuitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        PlayAsRunnerButton = GameObject.Find("PlayAsRunnerButton").GetComponent<Button>();
        PlayAsTimeLordButton = GameObject.Find("PlayAsTimeLordButton").GetComponent<Button>();
        PlayAsRockMasterButton = GameObject.Find("PlayAsRockMasterButton").GetComponent<Button>();
        MenuInterface = GameObject.Find("MenuInterface");
        GameInterface = GameObject.Find("GameInterface");
        MenuPanel = GameObject.Find("MenuPanel");
        ClassSelectorPanel = GameObject.Find("ClassSelectorPanel");

        InitializeMenusState();
    }

    public void InitializeMenusState()//On utilise pas les méthodes d'en dessous pour éviter un bug avec l'état true et false, vu que les méthodes en dessous inversent cet état
    {
        MenuInterface.SetActive(true); 
        GameInterface.SetActive(false);
        MenuPanel.SetActive(true);
        ClassSelectorPanel.SetActive(false);
    }

    public void ShowTimer(float TimeLeft)
    {
        TimeLeft += 1;

        float minutes = Mathf.FloorToInt(TimeLeft / 60);
        float seconds = Mathf.FloorToInt(TimeLeft % 60);

        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        /*if (TimeLeft < 10)
        {
            TimerText.text = "Time Left : 00:0" + TimeLeft;
        }
        else
        {
            TimerText.text = "Time Left : 00:" + TimeLeft;
        }*/
    } 

    public void ShowGlowingRocks(int GlowingRocksLeft)
    {
        GlowingRocksText.text = "Rocks Left : "+GlowingRocksLeft;
    }

    public void ShowScore(int Score)
    {
        ScoreText.text = "Score : " + Score;
    }

    public void ShowClass(string PlayerClassName)
    {
        ClassText.text = "Class : " + PlayerClassName ;
    }

    public void SetPlayButtonListener(UnityAction action)
    {
        PlayButton.onClick.AddListener(action); //Comme utiliser le + de On click dans un bouton dans l'inspector
    }

    public void SetQuitButtonListener(UnityAction action)
    {
        QuitButton.onClick.AddListener(action); //Comme utiliser le + de On click dans un bouton dans l'inspector
    }

    public void SetPlayAsTimeLordButtonListener(UnityAction action)
    {
        PlayAsTimeLordButton.onClick.AddListener(action); //Comme utiliser le + de On click dans un bouton dans l'inspector
    } 
    public void SetPlayAsStoneMasterButtonListener(UnityAction action)
    {
        PlayAsRockMasterButton.onClick.AddListener(action); //Comme utiliser le + de On click dans un bouton dans l'inspector
    } 
    public void SetPlayAsRunnerButtonListener(UnityAction action)
    {
        PlayAsRunnerButton.onClick.AddListener(action); //Comme utiliser le + de On click dans un bouton dans l'inspector
    }


    public void ToggleShowingMenuInterface()
    {
        //MenuInterface.activeSelf; //C'est la case de menuInterface
        MenuInterface.SetActive(!MenuInterface.activeSelf); //SetActive = cocher la case pour activer/desactiver un GameObject
    }

    public void ToggleShowingGameInterface()
    {
        GameInterface.SetActive(!GameInterface.activeSelf);
    }

    public void ToggleShowingMenuPanel()
    {
        MenuPanel.SetActive(!MenuPanel.activeSelf);
    }

    public void ToggleShowingClassSelectorPanel()
    {
        ClassSelectorPanel.SetActive(!ClassSelectorPanel.activeSelf);
    }


    public void MaMereEstAlleeChercherDesLegumesAuMarcheCeMatin()
    {

    }
}
