using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGameController : MonoBehaviour
{
    public GameObject[] trackObjects; // Ʈ���� ��Ÿ���� ���� ������Ʈ �迭
    public KeyCode[] inputKeys = { KeyCode.A, KeyCode.S, KeyCode.Semicolon, KeyCode.Quote }; // Ű �Է� ����
    public Transform[] judgmentLines; // �� Ʈ�� �Ʒ��ʿ� �������� ��ġ�� ��ġ

    private float perfectThreshold = 0.032f; // 32ms�� ��ȭ
    private float greatThreshold = 0.128f; // 128ms�� ��ȭ
    private float goodThreshold = 0.192f; // 192ms�� ��ȭ

    void Update()
    {
        // Ʈ���� Ű �Է� Ȯ��
        for (int i = 0; i < trackObjects.Length; i++)
        {
            if (Input.GetKeyDown(inputKeys[i]))
            {
                CheckNoteTiming(i); // �� Ʈ���� Ű �Է¿� ���� ���� ȣ��
            }
        }
    }

    void CheckNoteTiming(int trackIndex)
    {
        // Ʈ���� ���� ����� ��Ʈ�� ã��
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
            float timingDifference = closestDistance / 1; // �ð� ���� ��� (������ descentSpeed ���)

            // ���� ��� Ȯ�� �� ���
            if (timingDifference <= perfectThreshold)
            {
                Debug.Log("Perfect!");
                Destroy(closestNote.gameObject);
            }
            else if (timingDifference <= greatThreshold)
            {
                Debug.Log("Great!");
                Destroy(closestNote.gameObject);
            }
            else if (timingDifference <= goodThreshold)
            {
                Debug.Log("Good!");
                Destroy(closestNote.gameObject);
            }
            else
            {
                Debug.Log("Miss!");
            }
        }
    }
}