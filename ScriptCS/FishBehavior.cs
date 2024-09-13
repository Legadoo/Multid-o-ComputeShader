using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    public BoidController boidController;

    void Start()
    {
        // Certifique-se de que o BoidController está atribuído
        if (boidController == null)
        {
            boidController = FindObjectOfType<BoidController>();
        }
    }

    void Update()
    {
        if (boidController != null)
        {
            Vector3 targetPosition = boidController.GetPositionForFish(transform);
            Vector3 targetVelocity = boidController.GetVelocityForFish(transform);

            // Lógica de movimento do peixe
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
            transform.forward = Vector3.Lerp(transform.forward, targetVelocity, Time.deltaTime);
        }
        else
        {
            Debug.LogError("BoidController não encontrado!");
        }
    }
}
