using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RhythmGameController : MonoBehaviour
{
    public GameObject[] trackObjects;
    public KeyCode[] inputKeys = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F };
    public Transform[] judgmentLines;

    public Text judgmentText; // ���� ������ ǥ���� UI �ؽ�Ʈ ������Ʈ
    public Text scoreText;    // ������ ǥ���� UI �ؽ�Ʈ ������Ʈ

    private float perfectThreshold = 0.032f;
    private float greatThreshold = 0.128f;
    private float goodThreshold = 0.192f;

    private int score = 0;

    void Start()
    {
        UpdateScoreText(); // ������ �� �ʱ� ������ ȭ�鿡 ǥ��
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

            UpdateScoreText(); // ������ ������Ʈ�Ͽ� ȭ�鿡 ǥ��
        }
    }

    // ���� ������ �����ִ� �޼���
    void ShowJudgment(string judgment)
    {
        judgmentText.text = judgment; // ���� ���� ����
        judgmentText.gameObject.SetActive(true); // ���� ���� ǥ��
        StartCoroutine(HideJudgmentAfterDelay()); // ���� �ð� �� ������� ��
    }

    // ���� �ð� �� ���� ������ ����
    IEnumerator HideJudgmentAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // 0.5�� ���� ���� ǥ��
        judgmentText.gameObject.SetActive(false); // ���� ���� �����
    }

    // ���� UI �ؽ�Ʈ�� ������Ʈ�ϴ� �޼���
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}