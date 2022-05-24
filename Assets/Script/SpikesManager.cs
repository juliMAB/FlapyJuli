using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesManager : MonoBehaviour
{
    [SerializeField]
    enum SIDE
    {
        RIGHT,
        LEFT
    }
    [SerializeField] private GameObject[] rightSpikes;
    [SerializeField] private GameObject[] leftSpikes;
    [SerializeField] private GameObject rightParent;
    [SerializeField] private GameObject leftParent;
    [SerializeField] private SIDE side = SIDE.RIGHT;
    [SerializeField] private bool activeAnim = false;
    [SerializeField] private float animationTime = 1.0f;


    public void Init(ref System.Action OnTouchWall)
    {
        OnTouchWall += ActiveSpikes;
        
    }


    private void ActiveSpikes()
    {
        if (side==SIDE.RIGHT)
        {
            StartCoroutine( Anim(rightParent, rightParent.transform.position - Vector3.right*2, animationTime));
        }
        else
        {
            StartCoroutine(Anim(leftParent, leftParent.transform.position + Vector3.right*2, animationTime));
        }
        ActiveRandomSpikes();
        DisableCurrentSpikes();
        
    }
    private void DisableCurrentSpikes()
    {
        if (side == SIDE.RIGHT)
        {
            StartCoroutine(Anim(leftParent, leftParent.transform.position - Vector3.right*2, animationTime));
            side = SIDE.LEFT;
        }
        else
        {
            StartCoroutine(Anim(rightParent, rightParent.transform.position + Vector3.right*2, animationTime));
            side = SIDE.RIGHT;
        }
    }

    void ActiveRandomSpikes()
    {
        if (rightSpikes.Length == 0 || leftSpikes.Length == 0)
            return;
        bool almosOne = true;
        while (almosOne)
        {
            if (side == SIDE.RIGHT)
            {
                for (int i = 0; i < rightSpikes.Length; i++)
                {
                    bool randBool = System.Convert.ToBoolean(Random.Range(0, 2));
                    rightSpikes[i].SetActive(randBool);
                    if (randBool == false)
                        almosOne = randBool;
                }

            }
            else
            {
                for (int i = 0; i < leftSpikes.Length; i++)
                {
                    bool randBool = System.Convert.ToBoolean(Random.Range(0, 2));
                    leftSpikes[i].SetActive(randBool);
                    if (randBool == false)
                        almosOne = randBool;
                }
            }
        }
       
    }

    IEnumerator move(GameObject go,Vector3 to,float time)
    {
        Vector3 bgin = go.transform.position;
        float dTime = 0;
        float percent = 0;
        while (dTime<time)
        {
            dTime+=Time.deltaTime;
            percent = dTime/time;
            if (dTime>=time)
                percent = 1;
            go.transform.position = Vector3.Lerp(bgin, to, percent);
            yield return null;
        }
        activeAnim = false;
    }
    IEnumerator Anim(GameObject go, Vector3 to, float time)
    {
        activeAnim = true;
        while (activeAnim)
        {
            StartCoroutine(move(go, to, time));
            yield return null;
        }
    }

}
