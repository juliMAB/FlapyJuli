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
    [SerializeField] 
    private enum DIFICULTY
    {
        NONE,
        EASY,
        MEDIUM,
        HARD,
        IMPOSIBLE,
        ALLSPIKES
    }
    [SerializeField] private GameObject[] rightSpikes;
    [SerializeField] private GameObject[] leftSpikes;
    [SerializeField] private Collider2D[] rightSpikesCollider;
    [SerializeField] private Collider2D[] leftSpikesCollider;
    [SerializeField] private GameObject rightParent;
    [SerializeField] private GameObject leftParent;
    [SerializeField] private SIDE side = SIDE.RIGHT;
    [SerializeField] private DIFICULTY dificulty = DIFICULTY.NONE;
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

            rightSpikesCollider[i] = rightSpikes[i].GetComponent<Collider2D>();
            leftSpikesCollider[i] =  leftSpikes[i].GetComponent<Collider2D>();

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
            leftSpikes[i].SetActive(false);
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
        ActiveRandomSpikesBaseDifficulty(side);   
    }
    void ActiveRandomSpikesOld()
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
    void DisabledAllSpikes(SIDE side)
    {
        switch (side)
        {
            case SIDE.RIGHT:
                for (int i = 0; i < rightSpikes.Length; i++)
                    rightSpikes[i].SetActive(false);
                break;
            case SIDE.LEFT:
                for (int i = 0; i < leftSpikes.Length; i++)
                    leftSpikes[i].SetActive(false);
                break;
        }
    }
    void ActiveRandomSpikesBaseDifficulty(SIDE side)
    {
        if (rightSpikes.Length == 0 || leftSpikes.Length == 0)
            return;
        switch (dificulty)
        {
            case DIFICULTY.NONE:
                BaseEnabledDisable(side, 0);
                break;
            case DIFICULTY.EASY:
                BaseEnabledDisable(side, 2);
                break;
            case DIFICULTY.MEDIUM:
                BaseEnabledDisable(side, 5);
                break;
            case DIFICULTY.HARD:
                BaseEnabledDisable(side, 8);
                break;
            case DIFICULTY.IMPOSIBLE:
                BaseEnabledDisable(side, leftSpikes.Length - 2);
                break;
            case DIFICULTY.ALLSPIKES:
                BaseEnabledDisable(side, leftSpikes.Length);
                break;
            default:
                break;
        }
    }
    void BaseEnabledDisable(SIDE side,int cuantity)
    {
        switch (side)
        {
            case SIDE.RIGHT:
                for (int i = 0; i < cuantity; i++)
                {
                    int a = Random.Range(0, rightSpikes.Length);
                    if (rightSpikes[a].activeSelf)
                        i--;
                    else
                        rightSpikes[a].SetActive(true);
                }
                break;
            case SIDE.LEFT:
                for (int i = 0; i < cuantity; i++)
                {
                    int a = Random.Range(0, leftSpikes.Length);
                    if (leftSpikes[a].activeSelf)
                        i--;
                    else
                        leftSpikes[a].SetActive(true);
                }
                break;
            default:
                Debug.LogError("ERROR CALL");
                break;
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
        if (activeAnim)
        {
            StartCoroutine(move(go, to, time));
        }
        while (activeAnim)
        {
            yield return null;
        }
        DisabledAllSpikes(side);
    }

}
