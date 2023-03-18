using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_ClickToFireHook : MonoBehaviour
{
    public Transform HookHead;

    private Vector3 initPos;
    private Vector2 initRot;

    private GameObject Thing_that_got_hooked;
    private bool is_hook_able_to_catch_something;   //if it's aleady hook something or on its way back to player, it wont be able to catch anything

    public float HookSpeed;
    private HookStatus moveMode;
    public enum HookStatus
    {
        IDLE = 0,
        GO_FORWARD,
        GO_BACKWARD
    }

    public GameObject HookEye;
    List<GameObject> Hookeyes; 
    int HookEyeCount;


    const float MAX_DISTANCE = 18.0f;

    void Start()
    {
        moveMode = 0;
        is_hook_able_to_catch_something = true;
        initPos = new Vector3(HookHead.position.x, HookHead.position.y, HookHead.position.z);
        initRot = HookHead.up;
        HookEyeCount = 0;
        Hookeyes = new List<GameObject>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && moveMode == HookStatus.IDLE)
        {
            HookHead.transform.position = transform.position;
            Vector3 click = Input.mousePosition;
            click = Camera.main.ScreenToWorldPoint(click);
            HookHead.up = new Vector2(
                    click.x - HookHead.position.x,
                    click.y - HookHead.position.y
                );
            FireHook();
        }

        if (moveMode == HookStatus.GO_FORWARD)
        {
            Vector2 distance = new Vector2(HookHead.position.x - transform.position.x, HookHead.position.y - transform.position.y);

            if (distance.magnitude > MAX_DISTANCE)
            {
                RetrieveHook();
            }
        }

        if (moveMode == HookStatus.GO_BACKWARD)
        {
            Vector2 distance = new Vector2(HookHead.position.x - transform.position.x, HookHead.position.y - transform.position.y);

            if (distance.magnitude < 1.5f)
            {
                ResetHook();
            }
        }

    }

    void FixedUpdate()
    {
        //bla bla
        if (moveMode == HookStatus.GO_FORWARD)
        {
            spawnHookEye();
        }
        if (moveMode == HookStatus.GO_BACKWARD)
        {
            despawnHookEye();
        }
    }

    void FireHook()
    {
        
        moveMode = HookStatus.GO_FORWARD;
        HookHead.GetComponent<Rigidbody2D>().velocity = HookSpeed * HookHead.up;
    }

    void RetrieveHook()
    {
        moveMode = HookStatus.GO_BACKWARD;
        HookHead.GetComponent<Rigidbody2D>().velocity = -HookSpeed * HookHead.up;
        is_hook_able_to_catch_something = false;
    }

    void ResetHook()
    {
        moveMode = HookStatus.IDLE;
        HookHead.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        HookHead.transform.position = initPos;
        HookHead.up = initRot;
        Destroy(Thing_that_got_hooked);
        foreach (GameObject eye in Hookeyes)
        {
            Destroy(eye);
        }
        HookEyeCount = 0;
        Hookeyes.RemoveAll(eye => true);
        is_hook_able_to_catch_something = true;
    }

    void spawnHookEye()
    {
        GameObject tmp = Instantiate(HookEye, HookHead.position, HookHead.rotation);
        tmp.transform.position = new Vector3(tmp.transform.position.x, tmp.transform.position.y, 4);
        Hookeyes.Add(tmp);
        HookEyeCount++;
    }

    void despawnHookEye()
    {
        Destroy(Hookeyes[HookEyeCount-1]);
        Hookeyes.RemoveAt(HookEyeCount-1);
        HookEyeCount--;
    }

    public void OnHitSomething(GameObject hit)
    {
        is_hook_able_to_catch_something = false;
        Thing_that_got_hooked = hit;
        RetrieveHook();
    }

    public bool IsHookAvailable()
    {
        return is_hook_able_to_catch_something;
    }
}
