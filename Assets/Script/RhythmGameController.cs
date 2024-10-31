using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RhythmGameController : MonoBehaviour
{
    public GameObject[] trackObjects;
    public KeyCode[] inputKeys = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F };
    public Transform[] judgmentLines;

    public Text judgmentText; // 판정 문구를 표시할 UI 텍스트 컴포넌트
    public Text scoreText;    // 점수를 표시할 UI 텍스트 컴포넌트

    private float perfectThreshold = 0.032f;
    private float greatThreshold = 0.128f;
    private float goodThreshold = 0.192f;

    private int score = 0;

    void Start()
    {
        UpdateScoreText(); // 시작할 때 초기 점수를 화면에 표시
    }

    void Update()
    {
        for (int i = 0; i < trackObjects.Length; i++)
        {
            if (Input.GetKeyDown(inputKeys[i]))
            {
                CheckNoteTiming(i);
            }
        }
    }

    void CheckNoteTiming(int trackIndex)
    {
        Transform closestNote = null;
        float closestDistance = float.MaxValue;

        foreach (Transform note in trackObjects[trackIndex].transform)
        {
            float distance = Mathf.Abs(note.position.y - judgmentLines[trackIndex].position.y);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNote = note;
            }
        }

        if (closestNote != null)
        {
            float timingDifference = closestDistance;

            if (timingDifference <= perfectThreshold)
            {
                ShowJudgment("Perfect!");
                score += 300;
                Destroy(closestNote.gameObject);
            }
            else if (timingDifference <= greatThreshold)
            {
                ShowJudgment("Great!");
                score += 200;
                Destroy(closestNote.gameObject);
            }
            else if (timingDifference <= goodThreshold)
            {
                ShowJudgment("Good!");
                score += 100;
                Destroy(closestNote.gameObject);
            }
            else
            {
                ShowJudgment("Miss!");
            }

            UpdateScoreText(); // 점수를 업데이트하여 화면에 표시
        }
    }

    // 판정 문구를 보여주는 메서드
    void ShowJudgment(string judgment)
    {
        judgmentText.text = judgment; // 판정 문구 설정
        judgmentText.gameObject.SetActive(true); // 판정 문구 표시
        StartCoroutine(HideJudgmentAfterDelay()); // 일정 시간 후 사라지게 함
    }

    // 일정 시간 후 판정 문구를 숨김
    IEnumerator HideJudgmentAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // 0.5초 동안 문구 표시
        judgmentText.gameObject.SetActive(false); // 판정 문구 숨기기
    }

    // 점수 UI 텍스트를 업데이트하는 메서드
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}