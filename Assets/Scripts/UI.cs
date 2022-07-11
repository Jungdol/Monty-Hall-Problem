using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI loopNum;

    public TextMeshProUGUI normalWinRate;
    public TextMeshProUGUI normalLoseRate;

    public TextMeshProUGUI changeWinRate;
    public TextMeshProUGUI changeLoseRate;

    public TextMeshProUGUI normalRate;
    public TextMeshProUGUI changeRate;

    public Image[] winOrLose;

    public Slider waitTimeSlider;
    public TMP_InputField waitTimeInputField;

    public int reward;
    public int answer;
    public int change;

    MontyHall montyHall;

    private void Start()
    {
        montyHall = GetComponent<MontyHall>();

        waitTimeSlider.value = montyHall.waitTime;
        waitTimeInputField.text = montyHall.waitTime.ToString("F3");
    }

    public void WaitTimeOnchanged(bool isSlider)
    {
        if (isSlider)
        {
            montyHall.waitTime = waitTimeSlider.value;
            waitTimeInputField.text = montyHall.waitTime.ToString("F3");
        }
        else
        {
            waitTimeSlider.value = montyHall.waitTime;
            montyHall.waitTime = float.Parse(waitTimeInputField.text);
        }
    }

    public void ActiveOnchanged(bool value)
    {
        montyHall.isView = value;
    }

    public void LoopNumOnchanged(string value)
    {
        montyHall.loopNum = int.Parse(value);
    }

    public void ResetBtn()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        normalWinRate.text = montyHall.normalWinRate.ToString();
        normalLoseRate.text = montyHall.normalLoseRate.ToString();

        changeWinRate.text = montyHall.changeWinRate.ToString();
        changeLoseRate.text = montyHall.changeLoseRate.ToString();

        normalRate.text = montyHall.normalRate.ToString("F1") + "%";
        changeRate.text = montyHall.changeRate.ToString("F1") + "%";
        loopNum.text = montyHall.i + " / " + montyHall.loopNum;

        for (int i = 0; i < winOrLose.Length; i++)
        {
            winOrLose[i].color = montyHall.UIColors[i];
        }
    }
}
