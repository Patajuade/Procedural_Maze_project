using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : MonoBehaviour
{
    [SerializeField]
    public float InitialTimer;
    private float Timer;
    [SerializeField]
    public int InitialGlowingRocksAmount;
    private int GlowingRocksAmount;
    [SerializeField]
    public float InitialVelocityModifier;
    private float VelocityModifier;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject LevelBuilder;
    [SerializeField]
    private LevelController LevelController;
    [SerializeField]
    int CameraHeightOffset;
    [SerializeField]
    List<GameObject> GlowingRocks;
    [SerializeField]
    int Score;
    [SerializeField]
    PlayerClass PlayerClass;
    [SerializeField]
    string PlayerClassName;
    [SerializeField]
    private bool isGameOver = false;

    //TODO : Buffs (Des objets)
    //TODO : Classes

    public void DeductTimer()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            Timer = 0;
            isGameOver = false;
        }
    }

    public float GetTimer()
    {
        return Timer;
    }
  
    public bool DeductGlowingRocks()
    {
        if (GlowingRocksAmount > 0)
        {
            GlowingRocksAmount--;
            return true;
        }
        return false;
    }

    public bool GetIsGameOver()
    {
        return isGameOver;
    }

    public int GetGlowingRocksAmount()
    {
        return GlowingRocksAmount;
    }
    
    public float GetVelocityModifier()
    {
        return VelocityModifier;
    }

    public int GetScore()
    {
        return Score;
    }

    public string GetPlayerClassName()
    {
        return PlayerClassName;
    }

    public float GetInitialTimer()
    {
        return InitialTimer;
    }

    public int GetInitialGlowingRocksAmount()
    {
        return InitialGlowingRocksAmount;
    }
    public float GetInitialVelocityModifier()
    {
        return InitialVelocityModifier;
    }

    public void IncreaseScore()
    {
        Score++;
    }

    public void ResetScore()
    {
        Score = 0;
    }

    public void SetIsGameOver(bool isGameOver)
    {
        this.isGameOver = isGameOver;
    }

    public void SetPlayer(GameObject player)
    {
        Player = player;
    }
    public bool isPlayerAlreadyDefined()
    {
        if (Player != null)
            return true;
        else
            return false;
    }

    public GameObject GetPlayer()
    {
        return Player;
    }

    public void SetLevel(GameObject levelBuilder, int difficulty)
    {
        LevelBuilder = levelBuilder;
        LevelController = levelBuilder.GetComponent<LevelController>();
        LevelBuilder.GetComponent<LevelModel>().SetLevelDifficulty(difficulty);
    }

    public void SetTimer(float seconds)
    {
        Timer = seconds;
    }

    public void SetGlowingRocks(int amount)
    {
        GlowingRocksAmount = amount;
    }

    public void SetVelocityModifier(float VelocityModifier)
    {
        this.VelocityModifier = VelocityModifier;
    }

    public bool isLevelReady()
    {
        if (LevelController != null)
            return true;
        else
            return false;
    }

    public GameObject GetLevelBuilder()
    {
        return LevelBuilder;
    }

    public LevelController GetLevelController() //pour parler au levelcontroller sans faire des find partout
    {
        return LevelController;
    }

    public List<GameObject> GetGlowingRocksList()
    {
        return GlowingRocks;
    }

    public int GetCameraHeightOffset() 
    {
        return CameraHeightOffset;
    }

    public void InitializeGlowingRocksList()
    {
        GlowingRocks = new List<GameObject>();
    }

    public void AddGlowingRocks(GameObject glowingRock) //on ajoute les cailloux à une liste pour pouvoir les destroy
    {
        GlowingRocks.Add(glowingRock);
    }

    public void SetPlayerClassName(string name)
    {
        PlayerClassName = name;
    }

    public void SetPlayerClassAsTimeLord()
    {
        PlayerClass = new TimeLord();
        GameModel gameModel = gameObject.GetComponent<GameModel>();
        PlayerClass.StatModifier( ref gameModel );
    }
    public void SetPlayerClassAsStoneMaster()
    {
        PlayerClass = new StoneMaster();
        GameModel gameModel = gameObject.GetComponent<GameModel>();
        PlayerClass.StatModifier(ref gameModel);
    }
    public void SetPlayerClassAsRunner()
    {
        PlayerClass = new Runner();
        GameModel gameModel = gameObject.GetComponent<GameModel>();
        PlayerClass.StatModifier(ref gameModel);
    }
}
public abstract class PlayerClass //abstrat = on peut pas la définir (utiliser New)
{
    public abstract void StatModifier(ref GameModel gameModel);
}
public class TimeLord : PlayerClass
{
    public override void StatModifier(ref GameModel gameModel)
    {
        gameModel.SetTimer(gameModel.GetTimer() + 15);
        gameModel.SetPlayerClassName("TimeLord");
    }
}
public class StoneMaster : PlayerClass
{
    public override void StatModifier(ref GameModel gameModel)
    {
        gameModel.SetGlowingRocks(gameModel.GetGlowingRocksAmount() + 10);
        gameModel.SetPlayerClassName("StoneMaster");
    }
}
public class Runner : PlayerClass
{
    public override void StatModifier(ref GameModel gameModel)
    {
        gameModel.SetVelocityModifier(gameModel.GetVelocityModifier()*1.5f);
        gameModel.SetPlayerClassName("Runner");
    }
}

public abstract  class Item
{
    public abstract void BonusModifier(ref GameModel gameModel);
}
public class TimeBuff : Item
{
    public override void BonusModifier(ref GameModel gameModel)
    {
        gameModel.SetTimer(gameModel.GetTimer() + 10);
    }
}
public class StoneBuff : Item
{
    public override void BonusModifier(ref GameModel gameModel)
    {
        gameModel.SetGlowingRocks(gameModel.GetGlowingRocksAmount() + 2);
    }
}
public class SpeedBuff : Item
{
    public override void BonusModifier(ref GameModel gameModel)
    {
        gameModel.SetVelocityModifier(gameModel.GetVelocityModifier() * 1.1f);
    }
}
