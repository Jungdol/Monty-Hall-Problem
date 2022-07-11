using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MontyHall : MonoBehaviour
{
    [Header("시작 전 설정")]
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
    [Header("여러 보이는 것들")]
    public GameObject answerPrefab;
    public GameObject changePrefab;

    public Transform[] answersPos = new Transform[3];

    public GameObject[] Doors = new GameObject[3];

    public GameObject[] prefabs = new GameObject[3];
    public Transform[] prefabsPos = new Transform[3];

    [Header("UI 색상 설정")]
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
            reward = Random.Range(1, 4); // 정답
            answer = Random.Range(1, 4); // 3가지 중 하나 선택

            bool isActive = isView; // 화면에 염소, 자동차를 띄울 건지

            if (isActive) // reward 에 맞춰서 염소 및 자동차 위치 이동
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

            if (isActive) // 선택한 값 보이게 함
            {
                answerPrefab.SetActive(true);
                answerPrefab.transform.position = answersPos[answer - 1].position;
            }

            int open = Random.Range(1, 4);
            while (reward == open || answer == open) // 정답, 선택 값 제외 오픈
            {
                open = Random.Range(1, 4);
            }

            if (isActive) // 문 오픈함
            {
                yield return new WaitForSeconds(waitTime * 0.25f);
                Doors[open - 1].SetActive(false);
            }

            if (reward == answer) // 바꾸지 않아 이긴 횟수 올림
            {
                normalWinRate++;
                UIColors[0] = loseColor;
            }
            else                  // 바꾸지 않아 진 횟수 올림
            {
                normalLoseRate++;
                UIColors[1] = loseColor;
            }
            

            change = Random.Range(1, 4);  // 선택값을 바꿈
            while (change == open || change == answer) // 오픈 값, 기존에 선택한 값 제외 바꿈
            {
                change = Random.Range(1, 4);
            }

            if (isActive) // 바꾼 값 보이게 함
            {
                yield return new WaitForSeconds(waitTime * 0.25f);
                changePrefab.SetActive(true);
                changePrefab.transform.position = answersPos[change - 1].position;
            }

            if (reward == change) // 바꿔서 이긴 횟수 올림
            {
                changeWinRate++;
                UIColors[2] = winColor;
            }
            else                  // 바꿔서 진 횟수 올림
            {
                changeLoseRate++;
                UIColors[3] = winColor;
            }

            normalRate = (normalWinRate * 100.0f) / (normalWinRate + normalLoseRate); //확률 계산
            changeRate = (changeWinRate * 100.0f) / (changeWinRate + changeLoseRate); //확률 계산

            yield return new WaitForSeconds(waitTime * 0.5f); // 딜레이

            if (isActive) // 염소, 자동차 숨김, UI 색 원위치
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
        Debug.Log($"종료 시간: {endTime}");
    }
}
