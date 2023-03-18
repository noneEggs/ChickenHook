using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleChicken : Chicken
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //Overriden from base class - different chickens have different behaviour
    public override void OnHooked(Transform hook_head)
    {
        Debug.Log("This is called from LittleChicken.cs");
        Destroy(GetComponent<Rigidbody2D>());
        transform.position = hook_head.position;
        transform.parent = hook_head;
        GetComponent<Chicken_movement>().OnHooked();
    }
}
