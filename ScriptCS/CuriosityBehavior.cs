using UnityEngine;

public class CuriosityBehavior : MonoBehaviour
{
    public float curiosityRange = 10.0f;

    void Update()
    {
        Vector3 direction = (transform.position - Camera.main.transform.position).normalized;
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        if (distance < curiosityRange)
        {
            transform.position += direction * Time.deltaTime * 2.0f;
        }
    }
}
