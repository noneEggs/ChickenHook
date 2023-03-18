using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public class Chicken : MonoBehaviour
{
    public virtual void OnHooked(Transform t)
    {
        //Debug.Log("This is called from CHicken.cs");
    }
}