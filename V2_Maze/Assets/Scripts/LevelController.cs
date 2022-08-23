using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LevelController : MonoBehaviour
{
    [SerializeField]
    LevelModel LevelModel;

    void Start()
    {
        LevelModel = gameObject.GetComponent<LevelModel>();
        InitializeLevel();
    }

    public void InitializeLevel()
    {
        LevelModel.InitializeMazePartsList();
        LevelModel.InitializeItemBuffList();
        GeneratePath();
        GenerateItems();
        LevelModel.ToggleIsLevelReady();
        
    }

    public void GenerateMaze()
    {
        //int mazeSize = LevelModel.GetLevelDifficulty();
        int mazeSize = 5;
        //TODO: augmenter la difficulté via le gamecontroller
        for(int i = 0; i < mazeSize; i++)
        {
            for (int j = 0 ; j < mazeSize ; j++)
            {
                InstanciateMazePart(new Vector3( j * 10, 0, i * 10));
            }
        }
        GameObject Exit = ElementBuilder.InstantiatePrefabOnPosition("LevelExit", new Vector3(0, 0, (mazeSize-1)*10));
        LevelModel.SetLevelExit(Exit);
    }

    public Item SetItem()
    {
        System.Random random= new System.Random();
        Item response = null;
        switch (random.Next(3))
        {
            case 0:
                response = new TimeBuff();
                break;
            case 1:
                response = new StoneBuff();
                break;
            case 2:
                response = new SpeedBuff();
                break;
        }
        return response;
    }
 

    public void GenerateItems()
    {
        System.Random random = new System.Random();
        foreach (GameObject MazePart in LevelModel.GetMazePartsList())
        {
            if (random.Next(20) == 1)
            {
                GameObject itemGameObject = ElementBuilder.InstantiatePrefabOnPosition("Item", MazePart.transform.position);
                Item itemModifier = SetItem();
                GameObjectItem gameObjectItem = new GameObjectItem(itemGameObject, itemModifier);
                LevelModel.AddItemBuff(gameObjectItem);

            }
        }
    }

    public void GeneratePath()
    {
        List<Vector3> path = new List<Vector3>();
        int mazeBorderSize = 5;
        int pathSize = LevelModel.GetLevelDifficulty();
        int prefabSize = 10;
        // on instancie la 1ere case
        path.Add(GetRandomPosition(prefabSize, mazeBorderSize, mazeBorderSize)); //ça ajoute à path une position random
        InstanciateMazePart(path[0]); //On prend le 1er vector3 contenu dans la liste path, et on s'en sert comme position pour instanciatemazepart() //on crée la 1ere case du lairinthe
        
        GameObject Entry = ElementBuilder.InstantiatePrefabOnPosition("LevelEntry", new Vector3(path[0].x , 1, path[0].z));
        LevelModel.SetLevelEntry(Entry);

        System.Random random = new System.Random();
        for (int i = 1; i < pathSize; i++)
        {
            int direction = random.Next(4);

            Vector3 previousPosition = path[i - 1];
            Vector3 nextPosition = GetNextPosition(direction, previousPosition,prefabSize);
            int infinityLoopSecurity = 0;
            while (path.Contains(nextPosition) && infinityLoopSecurity <= 6) //On demande à la liste path si elle contient nextposition, ça renvoie vrai, et ça boucle tant que c'est vrai
            {
                //on essaie 7 fois de trouver une position vide pour y mettre une case en ayant connaissance de toutes les cases qui ont déjà été visitées
                //améliorer
                nextPosition = GetNextPosition(direction, previousPosition, prefabSize);
                infinityLoopSecurity++;
            }
            bool isNextPositionAlreadyInPath = path.Contains(nextPosition);
            path.Add(nextPosition);
            if (!isNextPositionAlreadyInPath)
                InstanciateMazePart(path[i]);

            if (i == pathSize - 1)
            {
                GameObject Exit = ElementBuilder.InstantiatePrefabOnPosition("LevelExit",path[i]);
                LevelModel.SetLevelExit(Exit);
            }
        }
        //TODO : changer la position de levelexit
    }

    public Vector3 GetNextPosition(int direction, Vector3 previousPosition, int prefabSize)
    {
        Vector3 result=new Vector3();
        if(direction == 0)
        {
            result = new Vector3(previousPosition.x, previousPosition.y, previousPosition.z + 1 * prefabSize);
        }
        if (direction == 1)
        {
            result= new Vector3(previousPosition.x + 1 * prefabSize, previousPosition.y, previousPosition.z);
        }
        if (direction == 2)
        {
            result= new Vector3(previousPosition.x, previousPosition.y,previousPosition.z - 1 * prefabSize);
        }
        if (direction == 3)
        {
            result= new Vector3(previousPosition.x - 1* prefabSize, previousPosition.y, previousPosition.z);
        }
        return result;
    }

    public Vector3 GetRandomPosition(int PrefabSize, int XBoundary, int ZBoundary)
    {
        System.Random random = new System.Random();
        return new Vector3(random.Next(XBoundary) * PrefabSize, 0, random.Next(ZBoundary) * PrefabSize); //retourne une position random mais qui est comprise dans les limites de la map
    }

    public void InstanciateMazePart(Vector3 Position)
    {
        GameObject mazePart = ElementBuilder.InstantiatePrefabOnPosition("MazeFloor", Position);
        LevelModel.AddMazePart(mazePart);
    }

    public void DestroyLevelElements()
    {
        foreach (GameObject mazePart in LevelModel.GetMazePartsList())
        {
            Destroy(mazePart);
        }
        foreach(GameObjectItem itemBuffs in LevelModel.GetItemBuffs())
        {
            Destroy(itemBuffs.ItemObject);
        }
        Destroy(LevelModel.GetLevelExit());
        Destroy(LevelModel.GetLevelEntry());
    }


    public GameObject GetSpawn()
    {
        return LevelModel.GetLevelEntry();
    }

    public bool GetTriggerExitState()
    {
        return LevelModel.GetLevelExitTrigger().GetState();
    }

   public List<GameObjectItem> GetItemBuffs()
    {
        return LevelModel.GetItemBuffs();
    }

    public bool isLevelReady()
    {
        return LevelModel.GetIsLevelReady();
    }

    public void SetDifficulty(int Difficulty)
    {
        LevelModel.SetLevelDifficulty(Difficulty);
    }
}
