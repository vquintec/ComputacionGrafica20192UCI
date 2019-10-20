using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BearScript : MonoBehaviour
{
    public Transform target;
    public bool amenazado;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.Find("punto");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!amenazado)
        {
            Caminar();
        }
    }

    void Caminar()
    {
        GetComponent<NavMeshAgent>().destination = target.position;
    }
}
