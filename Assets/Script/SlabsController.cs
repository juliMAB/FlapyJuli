using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlabsController : MonoBehaviour
{
    [SerializeField] private GameObject[] slabs;
    [SerializeField] private GameObject lastSlabs;
    [SerializeField] private float speed;
    [SerializeField] private GameObject SpawnPoint;
    [SerializeField] private int maxAmplitud;
    private void Start()
    {
        
    }
    void Update()
    {
        for (int i = 0; i < slabs.Length; i++)
        {
            slabs[i].transform.Translate(new Vector3(-speed * Time.deltaTime, 0, -speed * Time.deltaTime));
            if (slabs[i].transform.position.x + slabs[i].transform.position.z <= -1)
            {
                if (lastSlabs==null)
                {
                    slabs[i].transform.position = SpawnPoint.transform.position;
                    lastSlabs = slabs[i];
                }
                else
                {
                    int randX = Random.Range(0, 2);
                    int randZ = 0;
                    if (randX == 0)
                        randZ = 1;
                    else
                        randZ = 0;
                    slabs[i].transform.position = lastSlabs.transform.position+new Vector3(randX,0,randZ);
                    //if (slabs[i].transform.position.x >= maxAmplitud)
                    //{
                    //    slabs[i].transform.position = lastSlabs.transform.position + new Vector3(-1, 0, 0);
                    //}
                    //if (slabs[i].transform.position.z >= maxAmplitud)
                    //{
                    //    slabs[i].transform.position = lastSlabs.transform.position + new Vector3(0, 0, -1);
                    //}
                    if (Mathf.Abs(slabs[i].transform.position.x) + Mathf.Abs(slabs[i].transform.position.z) >= maxAmplitud)
                    {
                        slabs[i].transform.position = lastSlabs.transform.position + new Vector3(-randX, 0, -randZ);
                        if (randX == 0)
                        {
                            randX = 1;
                            randZ = 0;
                        }
                        else
                        {
                            randZ = 1;
                            randX = 0;
                        }
                        slabs[i].transform.position = lastSlabs.transform.position + new Vector3(randX, 0, randZ);
                    }
                    lastSlabs = slabs[i];
                }
                    
            }
        }
    }
}
