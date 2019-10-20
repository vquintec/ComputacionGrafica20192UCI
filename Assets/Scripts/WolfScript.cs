using UnityEngine;
using System.Collections;

/// <summary>
/// Creates wandering behaviour for a CharacterController.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class WolfScript : MonoBehaviour
{
    public float speed = 5f;
    public float directionChangeInterval = 1;
    public float maxHeadingChange = 90;
    public Animator anim;
    private int count = 0;
    private Vector3 noMove = new Vector3(0, 0, 0);
    private bool moving = false;

    CharacterController controller;
    float heading;
    Vector3 targetRotation;

    private void Start()
    {
        //anim = GetComponent<Animator>();
        //anim = GetComponent<Animator>();
    }

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        // Set random initial rotation
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);

        StartCoroutine(NewHeading());
    }

    void Update()
    {
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
        var forward = transform.TransformDirection(Vector3.forward);
        if (count < 400)
        {
            noMove = new Vector3(0, 0, 0);
            anim.SetBool("walk", false);
        }
        else if (count < 1200)
        {
            anim.SetBool("walk", true);
            noMove = forward / 5 * speed;
            if (count >= 800)
            {
                count = 0;
            }
        }
        controller.SimpleMove(noMove);
        count++;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Change the cube color to green.
        if (other.tag == "Player")
        {
            anim.SetBool("howl", true);
            controller.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("howl", false);
            controller.enabled = true;
        }
    }

    /// <summary>
    /// Repeatedly calculates a new direction to move towards.
    /// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
    /// </summary>
    IEnumerator NewHeading()
    {
        while (true)
        {
            NewHeadingRoutine();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    /// <summary>
    /// Calculates a new direction to move towards.
    /// </summary>
    void NewHeadingRoutine()
    {
        var floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
        var ceil = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }
}