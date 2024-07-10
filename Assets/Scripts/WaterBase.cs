using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBase : MonoBehaviour
{
    public GameObject[] cubeArr;
    public GameObject Cube;
    public GameObject Player;
    public PlayerControl playerControl;
    private int numOfCube = 10;
    float dx = 0.02f;
    int[] directionArr;

    bool dynamic = false;
    // Start is called before the first frame update
    void Start()
    {
        
        int ind = 0;
        cubeArr = new GameObject[numOfCube];
        directionArr = new int[numOfCube];
        float zz = transform.position.z;
        for(int i = 0; i < numOfCube/2; i++)
        {
            zz += 10.0f;
            Vector3 pos = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(1.3f, 2.0f), zz);
            cubeArr[ind++] = Instantiate(Cube, pos, Quaternion.identity);
            
        }

        zz = transform.position.z;
        for (int i = 0; i < (numOfCube/2) ; i++)
        {
            zz -= 10.0f;
            Vector3 pos = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(1.3f, 2.0f), zz);
            cubeArr[ind++] = Instantiate(Cube, pos, Quaternion.identity);
        }


        for(int i = 0;i < numOfCube; i++)
        {
            directionArr[i] = Random.Range(0, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - playerControl.startTime > 10.0) dynamic = true;

        
        if(transform.position.z+60 < Player.transform.position.z)
        {
            this.transform.Translate(new Vector3(0, 0, 200));
            for(int i = 0;i < numOfCube;i++)
            {

                cubeArr[i].transform.Translate(new Vector3(0, 0, 200));

                cubeArr[i].transform.position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(1.3f, 2.0f), cubeArr[i].transform.position.z);
            }
        }


        if(dynamic)
        {

            for(int i = 0; i < numOfCube; i++)
            {
                
                if (directionArr[i] == 1)
                {
                    cubeArr[i].transform.Translate(new Vector3(dx, 0, 0));
                    if (cubeArr[i].transform.position.x > 8) directionArr[i] = 0;
                }
                else
                {
                    cubeArr[i].transform.Translate(new Vector3(-dx, 0, 0));
                    if (cubeArr[i].transform.position.x < -8) directionArr[i] = 1;
                }
                
            }
        }

    }
}
