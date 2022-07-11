using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [Header("시작 전 설정")]
    public int loopNum = 100;
    [Range(0f, 3f)] public float waitTime = 0.5f;
    public bool isView = true;

    [HideInInspector] public int i = 0;

    [HideInInspector] public int normalWinRate = 0;
    [HideInInspector] public int normalLoseRate = 0;

    [HideInInspector] public int changeWinRate = 0;
    [HideInInspector] public int changeLoseRate = 0;

    [HideInInspector] public double normalRate = 0;
    [HideInInspector] public double changeRate = 0;

    //public GameObject goatPrefab;
    //public GameObject carPrefab;

    int reward = 0;
    int answer = 0;
    int change = 0;

    private void Awake()
    {
    }

    private void Start()
    {
        StartCoroutine(MontyHallProblem(isView));
    }

    private IEnumerator MontyHallProblem(bool isView)
    {
        float startTime = Time.time;

        for (i = 0; i < loopNum; i++) // loopNum 까지 반복
        {
            reward = Random.Range(1, 4); // 정답
            answer = Random.Range(1, 4); // 3가지 중 하나 선택

            int open = Random.Range(1, 4);
            while (reward == open || answer == open) // 정답, 선택한 값 제외 오픈
            {
                open = Random.Range(1, 4);
            }

            if (reward == answer) // 바꾸지 않아 이긴 횟수 올림
            {
                normalWinRate++;
            }
            else                  // 바꾸지 않아 진 횟수 올림
            {
                normalLoseRate++;
            }


            change = Random.Range(1, 4); // 선택값을 바꿈
            while (change == open || change == answer) // 오픈 값, 기존에 선택한 값 제외 바꿈
            {
                change = Random.Range(1, 4);
            }

            if (reward == change) // 바꿔서 이긴 횟수 올림
            {
                changeWinRate++;
            }
            else                  // 바꿔서 진 횟수 올림
            {
                changeLoseRate++;
            }

            normalRate = (normalWinRate * 100.0f) / (normalWinRate + normalLoseRate); //확률 계산
            changeRate = (changeWinRate * 100.0f) / (changeWinRate + changeLoseRate); //확률 계산

            yield return new WaitForSeconds(waitTime); // 딜레이

        }
        float endTime = Time.time - startTime;
        Debug.Log($"종료 시간: {endTime}");
    }
}
