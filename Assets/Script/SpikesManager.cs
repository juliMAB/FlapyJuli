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
    [SerializeField] private Collider[] rightSpikesCollider;
    [SerializeField] private Collider[] leftSpikesCollider;
    [SerializeField] private GameObject rightParent;
    [SerializeField] private GameObject leftParent;
    [SerializeField] private SIDE side = SIDE.RIGHT;
    [SerializeField] private bool activeAnim = false;
    [SerializeField] private float animationTime = 1.0f;

    private Vector3 posRightParentStart;
    private Vector3 posLeftParentStart;


    public void Init(ref System.Action OnTouchWall)
    {
        OnTouchWall += ActiveSpikes;
        
    }
    private void Start()
    {
        posRightParentStart = rightParent.transform.position;
        posLeftParentStart = leftParent.transform.position;
        for (int i = 0; i < rightSpikes.Length; i++)
        {
            rightSpikes[i].SetActive(true);
            leftSpikes[i].SetActive(true);

            rightSpikesCollider[i] = rightSpikes[i].GetComponent<Collider>();
            leftSpikesCollider[i] =  leftSpikes[i].GetComponent<Collider>();

            rightSpikes[i].SetActive(false);
            leftSpikes[i].SetActive(false);
        }
    }
    public void MyReset()
    {
        StopAllCoroutines();
        rightParent.transform.position = posRightParentStart;
        leftParent.transform.position = posLeftParentStart;
        for (int i = 0; i < rightSpikes.Length; i++)
        {
            rightSpikes[i].SetActive(false);
        }
        side = SIDE.LEFT;
        DisableColliderSpikes();
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
        DisableColliderSpikes();
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
    void DisableColliderSpikes()
    {
        if (side == SIDE.RIGHT)
        {
            for (int i = 0; i < leftSpikesCollider.Length; i++)
            {
                leftSpikesCollider[i].enabled = false;
                rightSpikesCollider[i].enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < rightSpikesCollider.Length; i++)
            {
                leftSpikesCollider[i].enabled = true;
                rightSpikesCollider[i].enabled = false;
            }
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
