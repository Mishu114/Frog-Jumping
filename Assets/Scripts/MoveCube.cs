using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float change = .05f;
    private float dir = 1;
    private Rigidbody rb;
    private FixedJoint joint;
    private bool once = true;

    public int id;
    // Start is called before the first frame update

    // Update is called once per frame

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.layer);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && once)
        {
            print("hi");
            //dir = 0;
            joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = collision.rigidbody;
            once = false;
            var playerDrag = collision.gameObject.GetComponent<Events>();
            playerDrag.Shooted += removeJoint;
            //Debug.Log(GeneratePlatform.tileCount);

        }
    }

    void removeJoint()
    {
        Destroy(joint);
    }
}
