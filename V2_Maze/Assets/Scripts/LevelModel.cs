
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelModel : MonoBehaviour
{
    [SerializeField]
    private GameObject LevelEntry;
    [SerializeField]
    private GameObject LevelExit;
    [SerializeField]
    private int LevelDifficulty;
    [SerializeField]
    private bool isLevelReady;
    [SerializeField]
    private List<GameObject> MazeParts;
    [SerializeField]
    private List<GameObjectItem> ItemBuffs;

    //TODO : increase leveldifficulty
    //TODO : getdiffuculty

    public GameObject GetLevelEntry()
    {
        return LevelEntry;
    }

    public GameObject GetLevelExit()
    {
        return LevelExit;
    }

    public Trigger GetLevelExitTrigger()
    {
        return LevelExit.GetComponent<Trigger>();
    }

    public int GetLevelDifficulty()
    {
        return LevelDifficulty;
    }

    public bool GetIsLevelReady()
    {
        return isLevelReady;
    }

    public List<GameObject> GetMazePartsList()
    {
        return MazeParts;
    }

    public List<GameObjectItem> GetItemBuffs()
    {
        return ItemBuffs;
    }

    public void SetLevelEntry(GameObject levelEntry)
    {
        LevelEntry = levelEntry;
    }

    public void SetLevelExit(GameObject levelExit)
    {
        LevelExit = levelExit;
    }

    public void SetLevelDifficulty(int levelDifficulty)
    {
        LevelDifficulty = levelDifficulty;
    }

    public void ToggleIsLevelReady()
    {
        isLevelReady = !isLevelReady;
    }

    public void InitializeMazePartsList()
    {
        MazeParts = new List<GameObject>();
    }

    public void InitializeItemBuffList()
    {
        ItemBuffs = new List<GameObjectItem>();  
    }

    public void AddMazePart(GameObject mazePart)
    {
        MazeParts.Add(mazePart);
    }

    public void AddItemBuff(GameObjectItem itemBuffs)
    {
        ItemBuffs.Add(itemBuffs);
    }

}

public class GameObjectItem
{
    public GameObject ItemObject { get; set; }
    public Item ItemStat { get; set; }

    public GameObjectItem(GameObject ItemObject, Item itemStat)
    {
        this.ItemObject = ItemObject;
        this.ItemStat = itemStat;

    }
}