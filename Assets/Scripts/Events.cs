using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public event Action Shooted = delegate { };
    public static event Action<int> UpdateScore = delegate { };
    bool down = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Leaf"))
        {
            //UpdateScore?.Invoke(collision.gameObject.GetComponent<MoveCube>().id);
        }
    }
    private void OnMouseDown()
    {
        down = true;
    }

    private void OnMouseUp()
    {
        if (down)
        {
            Shooted?.Invoke();
            down = false;
        }
    }

}
