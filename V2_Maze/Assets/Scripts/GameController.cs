using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameModel gameModel;
    [SerializeField]
    GameView gameView;
    //private bool isCounting = false;
    
    

    private void Start()
    {
        gameModel = gameObject.GetComponent<GameModel>();
        gameModel.SetTimer(1); //pour éviter qu'il détruise le niveau instantanément
        gameView = gameObject.GetComponent<GameView>();
        InitializeMenus();
    }

    private void Update()
    {
        if (isPlayerAlreadyDefined()&& !gameModel.GetIsGameOver())
        {
            HandleTimer();
            RefreshUserInterface();
            AttachCameraOnPlayer();
            if (gameModel.GetLevelController().GetTriggerExitState())
            {
                RestartLevel();
            }
            if (GetPlayer().transform.position.y < -5)
            {
                Destroy(GetPlayer());
            }
        }

        if (gameModel.GetTimer() <= 0 && !gameModel.GetIsGameOver())
        {
            gameModel.SetIsGameOver(true);
            gameView.ToggleShowingGameInterface();
            gameView.ToggleShowingMenuPanel();
            DestroyLevel();
        }

       
    }

    private void LateUpdate() //appellé juste après update dans la frame d'après
    {
         if(!isPlayerAlreadyDefined() && gameModel.isLevelReady())
        {
            InitializePlayer();
            AttachCameraOnPlayer();
        }
        if (isPlayerAlreadyDefined() && !gameModel.GetIsGameOver())
        {
            UseTriggeredItem();
        }
    }

    private void RestartLevel()
    {
        gameModel.SetIsGameOver(true);
        DestroyLevel();
        InitializeLevel();
        gameModel.SetIsGameOver(false);
    }

    private void RefreshUserInterface()
    {
        RefreshGlowingRocksDisplay();
        RefreshTimerDisplay();
        RefreshScoreDisplay();
        RefreshPlayerClassNameDisplay();
    }

    private void HandleTimer()
    {
        if (!gameModel.GetIsGameOver())
        {
            //StartCoroutine(CalculateTimer());
            gameModel.DeductTimer();

        }
    }

    /*IEnumerator CalculateTimer()
    {
        isCounting = true;
        yield return new WaitForSeconds(1);//return qui continue d'executer ce qu'y a en dessous
        isCounting = false;
    }*/

    public void UseRock()
    {
        bool result = gameModel.DeductGlowingRocks();
        if (result)
            InstantiateRock();
        //TODO : Couleur du cailloux random HAVERMOUT LINDENBLÜTEN FROLEIN
    }

    private void InstantiateRock()
    {
        GameObject Player = GetPlayer();
        GameObject Rock =  ElementBuilder.InstantiatePrefabOnPosition("GlowingRock", Player.transform.position); // on doit pas déclarer ElementBuilder car la méthode est statique, on peut l'utiliser directement.
        gameModel.AddGlowingRocks(Rock); //il a conscience des cailloux
    }

    private void RefreshTimerDisplay()
    {
        float timer = gameModel.GetTimer();
        gameView.ShowTimer(timer);
    }
    private void RefreshGlowingRocksDisplay()
    {
        int rocksLeft = gameModel.GetGlowingRocksAmount();
        gameView.ShowGlowingRocks(rocksLeft);
    }

    private void RefreshScoreDisplay()
    {
        int score = gameModel.GetScore();
        gameView.ShowScore(score);
    }

    private void RefreshPlayerClassNameDisplay()
    {
        string className = gameModel.GetPlayerClassName();
        gameView.ShowClass(className);
    }

    public void AttachCameraOnPlayer()
    {
        GameObject camera = GameObject.Find("Camera");
        Vector3 playerPosition = GetPlayer().transform.position;
        camera.transform.position = new Vector3(playerPosition.x, playerPosition.y + gameModel.GetCameraHeightOffset(), playerPosition.z);
        RotateCamera();
    }

    public void RotateCamera()
    {
        GameObject camera = GameObject.Find("Camera");
        camera.transform.LookAt(gameModel.GetPlayer().transform);
    }

    public bool isPlayerAlreadyDefined()
    {
        return gameModel.isPlayerAlreadyDefined();
    }

    public GameObject GetPlayer()
    {
        return gameModel.GetPlayer();
    }

    public float GetVelocityModifier()
    {
        return gameModel.GetVelocityModifier();
    }

    public void InitializeLevel()
    {
        gameModel.IncreaseScore();
        GameObject levelBuilder = ElementBuilder.InstantiatePrefabOnPosition("LevelBuilder", new Vector3(0, 0, 0));
        gameModel.SetLevel(levelBuilder, 150 + gameModel.GetScore()*5); //Met le nombre de case de base à 15 puis augmente en fonction du score*5
        gameModel.SetIsGameOver(false);
    }

    public void InitializePlayer()
    {
        GameObject Spawn = gameModel.GetLevelController().GetSpawn();
        GameObject Player = ElementBuilder.InstantiatePrefabOnPosition("Player", Spawn.transform.position); // on doit pas déclarer ElementBuilder car la méthode est statique, on peut l'utiliser directement.
        gameModel.SetPlayer(Player);
        
    }

    public void InitializeMenus() //ça crée l'action des boutons
    {
        gameView.SetPlayButtonListener(() => //on met plusieurs actions dans le bouton play (comme le + de OnClick dans l'inspector
        {
            gameView.ToggleShowingMenuPanel(); //Menu Play/Quit
            gameView.ToggleShowingClassSelectorPanel(); //toggle : inverse l'état
            //gameView.ToggleShowingGameInterface(); //Affiche les cailloux et le temps sans déclancher la logique
            //RefreshUserInterface();
            gameModel.ResetScore();
            gameModel.SetTimer(gameModel.GetInitialTimer());
            gameModel.SetGlowingRocks(gameModel.GetInitialGlowingRocksAmount());
            gameModel.SetVelocityModifier(gameModel.GetInitialVelocityModifier());
        });

        gameView.SetQuitButtonListener(() => //on met plusieurs actions dans le bouton play (comme le + de OnClick dans l'inspector
        {
            Application.Quit();
        });

        gameView.SetPlayAsTimeLordButtonListener(() =>
        {
            InitializeLevel();
            gameView.ToggleShowingClassSelectorPanel();
            gameModel.SetPlayerClassAsTimeLord();
            gameView.ToggleShowingGameInterface();
            gameModel.SetIsGameOver(false);

        });

        gameView.SetPlayAsStoneMasterButtonListener(() =>
        {
            InitializeLevel();
            gameView.ToggleShowingClassSelectorPanel();
            gameModel.SetPlayerClassAsStoneMaster();
            gameView.ToggleShowingGameInterface();
            gameModel.SetIsGameOver(false);

        });

        gameView.SetPlayAsRunnerButtonListener(() =>
        {
            InitializeLevel();
            gameView.ToggleShowingClassSelectorPanel();
            gameModel.SetPlayerClassAsRunner();
            gameView.ToggleShowingGameInterface();
            gameModel.SetIsGameOver(false);

        });
    }

    public void DestroyLevel()
    {
        gameModel.GetLevelController().DestroyLevelElements();
        foreach (GameObject rock in gameModel.GetGlowingRocksList())
        {
            Destroy(rock);
        }
        Destroy(gameModel.GetLevelBuilder());
        Destroy(gameModel.GetPlayer());
        //TODO : destroy les objets au sol restants
    }

    public void UseTriggeredItem()
    {
        GameObjectItem objectToRemove = null;
        LevelController lc = gameModel.GetLevelController();
        List<GameObjectItem> list = lc.GetItemBuffs();
        foreach (GameObjectItem item in list)
        {
            if (item.ItemObject.GetComponent<Trigger>().GetState())
            {
                Destroy(item.ItemObject);
                item.ItemStat.BonusModifier(ref gameModel);
                objectToRemove = item;
            }
        }
        if (objectToRemove != null)
            gameModel.GetLevelController().GetItemBuffs().Remove(objectToRemove);
    }
}
