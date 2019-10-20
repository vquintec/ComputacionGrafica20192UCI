using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class NormalGameMode : MonoBehaviourPun, IPunObservable {

    public float SpawnTime = 10;
    float timer;
    int count = 0;
    bool HasPlayerSpawned = false;
    bool isFoxSpawned = false;
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer >= SpawnTime) {
            if (!HasPlayerSpawned) {
                HasPlayerSpawned = true;
                PhotonNetwork.Instantiate("MaleFree1", new Vector3(0, 2, 0), Quaternion.identity, 0);
                Vector3 bearRotation = Vector3.up * Random.Range(0, 360);
                GameObject myBear = PhotonNetwork.Instantiate("BEAR2", new Vector3(-2, 3, 2), Quaternion.Euler(bearRotation), 0);
                
            }
            timer = 0;
        }
        //myBear.transform.
        //Debug.Log(count);
        if (count > 500 && !isFoxSpawned)
        {
            GameObject myFox = PhotonNetwork.Instantiate("FOX", new Vector3(0.5f, 0, 0), Quaternion.identity, 0);
        }
        count++;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            
        } else if (stream.IsReading) {
            
        }
    }
}
