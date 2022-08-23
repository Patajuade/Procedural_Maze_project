using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLight : MonoBehaviour
{
    bool IsRising = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = gameObject.transform.position;
        gameObject.transform.position = new Vector3(position.x, Pulse(position.y), position.z);
        if (position.y >= 3.0f)
        {
            IsRising = false;
        }
        if (position.y <= 1.0f)
        {
            IsRising = true;
        }
    }

    public float Pulse(float Axe)
    {
        if (IsRising)
        {
            return Axe + 0.005f;
        }
        return Axe - 0.005f;
    }
}
