using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [Header("���� �� ����")]
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

        for (i = 0; i < loopNum; i++) // loopNum ���� �ݺ�
        {
            reward = Random.Range(1, 4); // ����
            answer = Random.Range(1, 4); // 3���� �� �ϳ� ����

            int open = Random.Range(1, 4);
            while (reward == open || answer == open) // ����, ������ �� ���� ����
            {
                open = Random.Range(1, 4);
            }

            if (reward == answer) // �ٲ��� �ʾ� �̱� Ƚ�� �ø�
            {
                normalWinRate++;
            }
            else                  // �ٲ��� �ʾ� �� Ƚ�� �ø�
            {
                normalLoseRate++;
            }


            change = Random.Range(1, 4); // ���ð��� �ٲ�
            while (change == open || change == answer) // ���� ��, ������ ������ �� ���� �ٲ�
            {
                change = Random.Range(1, 4);
            }

            if (reward == change) // �ٲ㼭 �̱� Ƚ�� �ø�
            {
                changeWinRate++;
            }
            else                  // �ٲ㼭 �� Ƚ�� �ø�
            {
                changeLoseRate++;
            }

            normalRate = (normalWinRate * 100.0f) / (normalWinRate + normalLoseRate); //Ȯ�� ���
            changeRate = (changeWinRate * 100.0f) / (changeWinRate + changeLoseRate); //Ȯ�� ���

            yield return new WaitForSeconds(waitTime); // ������

        }
        float endTime = Time.time - startTime;
        Debug.Log($"���� �ð�: {endTime}");
    }
}
