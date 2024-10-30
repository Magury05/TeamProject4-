using UnityEngine;

public class TrackController : MonoBehaviour
{
    public AudioSource audioSource; // 유니티 에디터에서 할당
    public GameObject[] trackObjects; // 트랙을 나타내는 게임 오브젝트 배열
    public float descentSpeed = 1f; // 트랙의 하강 속도
    
    void Start()
    {
        // 오디오가 할당되었는지 확인
        if (audioSource.clip != null)
        {
           
        }
    }
    void Update()
    {
        // 트랙을 내려가게 함
        MoveTracksDown();
    }

    void MoveTracksDown()
    {
        // 각 트랙을 아래로 이동
        foreach (GameObject track in trackObjects)
        {
            Vector3 newPosition = track.transform.localPosition;
            newPosition.y -= descentSpeed * Time.deltaTime; // 속도에 따라 하강
            track.transform.localPosition = newPosition;
        }
    }
}