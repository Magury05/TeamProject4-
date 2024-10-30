using UnityEngine;

public class TrackController : MonoBehaviour
{
    public AudioSource audioSource; // ����Ƽ �����Ϳ��� �Ҵ�
    public GameObject[] trackObjects; // Ʈ���� ��Ÿ���� ���� ������Ʈ �迭
    public float descentSpeed = 1f; // Ʈ���� �ϰ� �ӵ�
    
    void Start()
    {
        // ������� �Ҵ�Ǿ����� Ȯ��
        if (audioSource.clip != null)
        {
           
        }
    }
    void Update()
    {
        // Ʈ���� �������� ��
        MoveTracksDown();
    }

    void MoveTracksDown()
    {
        // �� Ʈ���� �Ʒ��� �̵�
        foreach (GameObject track in trackObjects)
        {
            Vector3 newPosition = track.transform.localPosition;
            newPosition.y -= descentSpeed * Time.deltaTime; // �ӵ��� ���� �ϰ�
            track.transform.localPosition = newPosition;
        }
    }
}