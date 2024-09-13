using UnityEngine;

public class LODManager : MonoBehaviour
{
    public float lodDistance = 50.0f;

    void Update()
    {
        foreach (var fish in FindObjectsOfType<FishBehavior>())
        {
            float distance = Vector3.Distance(Camera.main.transform.position, fish.transform.position);
            fish.gameObject.SetActive(distance < lodDistance);
        }
    }
}
