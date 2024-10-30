using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGameController : MonoBehaviour
{
    public GameObject[] trackObjects; // 트랙을 나타내는 게임 오브젝트 배열
    public KeyCode[] inputKeys = { KeyCode.A, KeyCode.S, KeyCode.Semicolon, KeyCode.Quote }; // 키 입력 정의
    public Transform[] judgmentLines; // 각 트랙 아래쪽에 판정선을 배치할 위치

    private float perfectThreshold = 0.032f; // 32ms로 완화
    private float greatThreshold = 0.128f; // 128ms로 완화
    private float goodThreshold = 0.192f; // 192ms로 완화

    void Update()
    {
        // 트랙별 키 입력 확인
        for (int i = 0; i < trackObjects.Length; i++)
        {
            if (Input.GetKeyDown(inputKeys[i]))
            {
                CheckNoteTiming(i); // 각 트랙의 키 입력에 대한 판정 호출
            }
        }
    }

    void CheckNoteTiming(int trackIndex)
    {
        // 트랙에 가장 가까운 노트를 찾음
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
            float timingDifference = closestDistance / 1; // 시간 차이 계산 (임의의 descentSpeed 사용)

            // 판정 결과 확인 및 출력
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