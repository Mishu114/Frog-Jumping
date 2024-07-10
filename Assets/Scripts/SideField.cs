using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideField : MonoBehaviour
{
    public GameObject Player;
    // Update is called once per frame
    void Update()
    {
        if (transform.position.z + 60 < Player.transform.position.z)
        {
            this.transform.Translate(new Vector3(0, 0, 200));
        }
    }
}
