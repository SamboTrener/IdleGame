using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class BgMover : MonoBehaviour
{
    public static BgMover Instance {  get; private set; }

    public Action OnLevelCompleted;

    [SerializeField] float maxTimeMoving;
    [SerializeField] float moveSpeed;
    [SerializeField] float minXPosBeforeReposition;
    float timeMoving;

    private void Awake()
    {
        Instance = this;
    }

    float defaultXPosition;

    void Start()
    {

        defaultXPosition = transform.position.x;
        OnLevelCompleted += MoveBg;
    }

    void MoveBg()
    {
        if(transform.position.x <= minXPosBeforeReposition)
        {
            Debug.Log(transform.position.x);
            transform.position = new Vector3(defaultXPosition, transform.position.y, transform.position.z);
        }
        StartCoroutine(nameof(MoveSlow));
    }

    IEnumerator MoveSlow()
    {
        while (maxTimeMoving > timeMoving)
        {
            timeMoving += Time.deltaTime;
            transform.Translate(-Time.deltaTime * moveSpeed, 0, 0);
            yield return null;
        }
        timeMoving = 0;
    }
}
