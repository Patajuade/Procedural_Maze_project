using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBuilder : MonoBehaviour
{ 
 
    public static GameObject InstantiatePrefabOnPosition(string prefabName,Vector3 position)//si prefabName = geraldine
    {
        GameObject prefab = Resources.Load(prefabName) as GameObject;//On prend dans les assets l'objet "geraldine"
        GameObject instance = Instantiate(prefab);//on le "drag & drop" en code dans la scène
        instance.transform.position = position;//on change sa position et on le téléporte sur "position" passé en argument
        return instance;//On retrourne l'objet pour pouvoir l'utiliser
    }
}
