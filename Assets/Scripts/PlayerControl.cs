using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 dir;
    public int speed;
    private float theta;
    public float DestroyBarY = -5;

    public GameObject[] indicators;
    public GameObject indicator;

    public Camera camera;

    bool show = false;
    public Rigidbody rb;

    bool touch = false;
    float touchTime = 0;

    bool canShoot = true;
    
    public int cnt = 0;

    public float startTime = 0;
    float cam_x = 0;

    public GameObject RippleEffect;
    GameObject ripple;
    bool rippleShow = false;
    float rippleEffectStart = 0;


    private void Awake()
    {
        startTime = Time.time; 
    }

    private void Update()
    {
        /*if (Time.time - touchTime < 5)
        {
            rb.isKinematic = true;
            print(touchTime);
        }

        else
            rb.isKinematic = false;*/


        if(!rippleShow && this.transform.position.y < 1.5)
        {
            rippleShow = true;
            ripple = Instantiate(RippleEffect, new Vector3(this.transform.position.x, this.transform.position.y-0.2f, this.transform.position.z), Quaternion.identity);
            rippleEffectStart = Time.time;
        }

        if(ripple!= null && rippleShow)
        {
            if(Time.time - rippleEffectStart > 2)
                Destroy(ripple);
        }

        if (show)
        {
            dir = Input.mousePosition - pos1;
            var dirr = new Vector3(dir.x, dir.y, dir.y);
            Vector3 vel = (dirr * speed)/GetComponent<Rigidbody>().mass;
            float TotalDuration = (2 * vel.y) / Physics.gravity.y;
            float time = TotalDuration/indicators.Length;
            for (int t = 0; t < indicators.Length; t++)
            {
                float x = vel.x * t * time;
                float z =  vel.z * t * time;
                float y = (vel.y * t * time - 0.5f*Physics.gravity.y*t*t* time*time);


                indicators[t].transform.position =transform.position - new Vector3(x,y,z);

                
            }
        }


        camera.transform.position = new Vector3(transform.position.x * 0.4f, 25, transform.position.z - 15);


    }
    // Start is called before the first frame update

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("Leaf"))
        {
            canShoot = true;
            cnt = (int)((transform.position.z + 5) / 10);
        }
        if (c.gameObject.layer == LayerMask.NameToLayer("DeadZone"))
        {
            rippleShow = false;
            startTime = Time.time;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }


    private void OnMouseDown()
    {
        if(canShoot)
        {
            show = true;
            pos1 = Input.mousePosition;
            for (int i = 0; i < indicators.Length; i++)
            {
                indicators[i] = Instantiate(indicator, this.transform.position, Quaternion.identity);
                indicators[i].transform.localScale -= new Vector3(i * 0.01f, i * 0.01f, i * 0.01f);
            }
        }
        
        //print(this.transform.position);

    }
    private void OnMouseUp()
    {
        if(canShoot)
        {
            show = false;
            touch = false;
            pos2 = Input.mousePosition;
            dir = pos1 - pos2;
            Shoot();
            for (int i = 0; i < indicators.Length; i++) Destroy(indicators[i]);
        }
        
    }
    void Shoot()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(dir.x,dir.y,dir.y)*speed, ForceMode.Impulse);
        canShoot = false;
    }
}
