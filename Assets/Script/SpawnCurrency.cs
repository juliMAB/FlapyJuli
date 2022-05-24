using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCurrency : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPoints;

    [SerializeField] GameObject cointPrifab;

    public void  SpawnCoint(Vector3 playerPos)
    {
        GameObject largePos=null;
        float maxDist=0;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (maxDist < Vector3.Distance (playerPos, spawnPoints[i].transform.position))
            {
                largePos = spawnPoints[i];
                maxDist = Vector3.Distance(playerPos, spawnPoints[i].transform.position);
            }
        }
        cointPrifab.SetActive(true);
        cointPrifab.transform.position = largePos.transform.position;
    }
}
