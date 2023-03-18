using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_movement : MonoBehaviour
{
    float StartTime;
    Vector2 CurrentDestination;
    int DirectionX;  //-1 = going left 1 = going right
    int DirectionY;  //-1 = going down 1 = going up
    bool NeedToChangeDirection;

    const float MAX_X = 12.5f;
    const float MIN_X = -12.5f;
    const float MAX_Y = 7f;
    const float MIN_Y = -2.5f;


    public float Speed;
    public float ExpectedDistance;

    public GameObject test;


    void Start()
    {
        NeedToChangeDirection = false;
        DirectionX = transform.position.x > 0 ? -1 : 1;
        DirectionY = transform.position.y > 2.5f ? -1 : 1;
        if (transform.position.x > 0)
            DoFlipX();
        StartCoroutine(MovementLoop());
    }

    void Update()
    {
        
    }

    public void OnHooked()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        StopAllCoroutines();
    }

    bool ReachCurrentDestination()
    {
        if (Vector2.Distance(transform.position, CurrentDestination) < 0.25f)
            return true;
        else
            return false;
    }

    void DoFlipX()
    {
        GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX ? false : true;
    }

    IEnumerator MovementLoop()
    {
        Vector3 p = transform.position;
        //construct a vector that has pre-determined length
        float x_offset = DirectionX * Random.Range(0.9f * ExpectedDistance, ExpectedDistance);
        float y_offset = DirectionY * Mathf.Sqrt(Mathf.Abs(ExpectedDistance * ExpectedDistance - x_offset * x_offset));  //Abs just for sure

        //Debug.Log(x_offset + " / " + y_offset);

        CurrentDestination = new Vector3(p.x + x_offset, p.y + y_offset);
        
        if (CurrentDestination.x > MAX_X || CurrentDestination.x < MIN_X)
        {
            DirectionX *= -1;
            x_offset *= -1;
            DoFlipX();
            CurrentDestination = new Vector3(p.x + x_offset, p.y + y_offset);
        }
        if (CurrentDestination.y > MAX_Y || CurrentDestination.y < MIN_Y)
        {
            DirectionY *= -1;
            y_offset *= -1;
            CurrentDestination = new Vector3(p.x + x_offset, p.y + y_offset);
        }

        //Instantiate(test, CurrentDestination, test.transform.rotation);     //VISUALIZE TEST

        Vector2 SpeedVector = new Vector2(x_offset, y_offset);
        SpeedVector = Speed * SpeedVector.normalized;
        GetComponent<Rigidbody2D>().velocity = SpeedVector;

        yield return new WaitUntil(ReachCurrentDestination);

        //DirectionY = -DirectionY;
        StartCoroutine(MovementLoop());         //and again and again
    }

}
