using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MontyHall : MonoBehaviour
{
    [Header("���� �� ����")]
    public int loopNum = 100;
    [Range(0f, 10f)] public float waitTime = 0.5f;
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
    [Header("���� ���̴� �͵�")]
    public GameObject answerPrefab;
    public GameObject changePrefab;

    public Transform[] answersPos = new Transform[3];

    public GameObject[] Doors = new GameObject[3];

    public GameObject[] prefabs = new GameObject[3];
    public Transform[] prefabsPos = new Transform[3];

    [Header("UI ���� ����")]
    public Color32 winColor;
    public Color32 loseColor;

    [HideInInspector] public Color32[] UIColors = new Color32[4];

    int reward = 0;
    int answer = 0;
    int change = 0;

    public void StartBtn()
    {
        StartCoroutine(MontyHallProblem());
    }

    private IEnumerator MontyHallProblem()
    {
        float startTime = Time.time;

        for (i = 0; i < loopNum; i++)
        {
            reward = Random.Range(1, 4); // ����
            answer = Random.Range(1, 4); // 3���� �� �ϳ� ����

            bool isActive = isView; // ȭ�鿡 ����, �ڵ����� ��� ����

            if (isActive) // reward �� ���缭 ���� �� �ڵ��� ��ġ �̵�
            {
                int goatPrafabCount = 0;
                for (int j = 0; j < 3; j++)
                {
                    prefabs[j].SetActive(true);

                    if (reward - 1 != j)
                    {
                        prefabs[goatPrafabCount].transform.position = prefabsPos[j].position;
                        goatPrafabCount++;

                    }
                    else
                    {
                        prefabs[2].transform.position = prefabsPos[j].position;
                    }
                }
            }

            if (isActive) // ������ �� ���̰� ��
            {
                answerPrefab.SetActive(true);
                answerPrefab.transform.position = answersPos[answer - 1].position;
            }

            int open = Random.Range(1, 4);
            while (reward == open || answer == open) // ����, ���� �� ���� ����
            {
                open = Random.Range(1, 4);
            }

            if (isActive) // �� ������
            {
                yield return new WaitForSeconds(waitTime * 0.25f);
                Doors[open - 1].SetActive(false);
            }

            if (reward == answer) // �ٲ��� �ʾ� �̱� Ƚ�� �ø�
            {
                normalWinRate++;
                UIColors[0] = loseColor;
            }
            else                  // �ٲ��� �ʾ� �� Ƚ�� �ø�
            {
                normalLoseRate++;
                UIColors[1] = loseColor;
            }
            

            change = Random.Range(1, 4);  // ���ð��� �ٲ�
            while (change == open || change == answer) // ���� ��, ������ ������ �� ���� �ٲ�
            {
                change = Random.Range(1, 4);
            }

            if (isActive) // �ٲ� �� ���̰� ��
            {
                yield return new WaitForSeconds(waitTime * 0.25f);
                changePrefab.SetActive(true);
                changePrefab.transform.position = answersPos[change - 1].position;
            }

            if (reward == change) // �ٲ㼭 �̱� Ƚ�� �ø�
            {
                changeWinRate++;
                UIColors[2] = winColor;
            }
            else                  // �ٲ㼭 �� Ƚ�� �ø�
            {
                changeLoseRate++;
                UIColors[3] = winColor;
            }

            normalRate = (normalWinRate * 100.0f) / (normalWinRate + normalLoseRate); //Ȯ�� ���
            changeRate = (changeWinRate * 100.0f) / (changeWinRate + changeLoseRate); //Ȯ�� ���

            yield return new WaitForSeconds(waitTime * 0.5f); // ������

            if (isActive) // ����, �ڵ��� ����, UI �� ����ġ
            {
                Doors[open - 1].SetActive(true);
                
                answerPrefab.SetActive(false);
                changePrefab.SetActive(false);

                for (int j = 0; j < 3; j++)
                {
                    prefabs[j].SetActive(false);
                }
            }

            for (int j = 0; j < 4; j++)
            {
                UIColors[j] = new Color32(255, 255, 255, 255);
            }
        }
        float endTime = Time.time - startTime;
        Debug.Log($"���� �ð�: {endTime}");
    }
}
