using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook_HitChicken : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        GameObject Player = transform.parent.gameObject;
        if (!Player.GetComponent<PC_ClickToFireHook>().IsHookAvailable())
            return;
        hit.GetComponent<Chicken>().OnHooked(transform);    
        Player.GetComponent<PC_ClickToFireHook>().OnHitSomething(hit.gameObject);
    }
}
