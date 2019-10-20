using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPosition : MonoBehaviour
{
    public Transform spawner;
    public float x;
    public float z;
    public float y;
    public int radio;

    // Start is called before the first frame update
    void Start()
    {
        x = Random.Range(spawner.transform.position.x - radio, spawner.transform.position.x + radio);
        z = Random.Range(spawner.transform.position.z - radio, spawner.transform.position.z + radio);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(x, y, z);
        bool foundHit = false;
        RaycastHit hit;
        int layerUse = (1 << 4) | (1 << 8);

        foundHit = Physics.Raycast(transform.position, transform.up, out hit, Mathf.Infinity);
        if(foundHit)
        {
            if (hit.collider.gameObject.tag == "Landscape")
            {
                y = hit.point.y - 0.2f;
            }
        }
    }

    void Player()
    {
        spawner = GameObject.FindGameObjectWithTag("MaleFree1").transform;
        radio = 20;

        x = Random.Range(spawner.transform.position.x - radio, spawner.transform.position.x + radio);
        z = Random.Range(spawner.transform.position.z - radio, spawner.transform.position.z + radio);
    }
}
