using UnityEngine;

public class Boid : MonoBehaviour
{
    public float separationWeight = 1.5f;
    public float alignmentWeight = 1.0f;
    public float cohesionWeight = 1.0f;
    public float maxSpeed = 5.0f;
    public float maxForce = 0.1f;

    public Transform boidManagerTransform;
    public float speed = 5f;
    public float alignmentRadius = 5f;

    private Vector3 velocity;
    private Vector3 acceleration;
    private float neighborRadius = 5.0f;
    public float minDistance = 3f;  // Distância mínima do BoidManager

    public Vector3 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    private void Start()
    {
        // stopwatch = new Stopwatch();
        // stopwatch.Start();
        BoidManager boidManager = FindObjectOfType<BoidManager>();
        if (boidManager != null)
        {
            boidManagerTransform = boidManager.transform;
        }
        else
        {
            Debug.LogError("Não foi encontrado nenhum BoidManager na cena.");
        }

        velocity = Random.insideUnitSphere * maxSpeed;
    }

    private void Update()
    {
        
        if (boidManagerTransform != null)
        {
            // Calcular a distância do boid para o BoidManager
            float distanceToPivot = Vector3.Distance(transform.position, boidManagerTransform.position);

            if (distanceToPivot > minDistance)
            {
                // Se estiver longe demais, mova-se em direção ao BoidManager
                Vector3 direction = (boidManagerTransform.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
            else
            {
                // Se estiver próximo demais, orbite em volta do BoidManager
                OrbitAroundPivot();
            }
        }
        else
        {
            Debug.LogWarning("BoidManagerTransform não está atribuído.");
        }

        // Cálculo de separação, alinhamento e coesão
        Vector3 separation = Vector3.zero;
        Vector3 alignment = Vector3.zero;
        Vector3 cohesion = Vector3.zero;

        Collider[] neighbors = Physics.OverlapSphere(transform.position, neighborRadius);
        int count = 0;

        foreach (Collider neighbor in neighbors)
        {
            if (neighbor.gameObject != gameObject)
            {
                Boid neighborBoid = neighbor.gameObject.GetComponent<Boid>();
                if (neighborBoid)
                {
                    Vector3 direction = transform.position - neighbor.transform.position;
                    separation += direction.normalized / direction.magnitude;

                    alignment += neighborBoid.velocity;
                    cohesion += neighbor.transform.position;
                    count++;
                }
            }
        }

        if (count > 0)
        {
            separation /= count;
            alignment /= count;
            cohesion /= count;

            cohesion = (cohesion - transform.position).normalized;
            alignment = alignment.normalized;

            separation *= separationWeight;
            alignment *= alignmentWeight;
            cohesion *= cohesionWeight;

            acceleration = separation + alignment + cohesion;
            acceleration = Vector3.ClampMagnitude(acceleration, maxForce);
        }

        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        transform.position += velocity * Time.deltaTime;
        transform.forward = velocity.normalized;

        // stopwatch.Stop();
        // UnityEngine.Debug.Log("Tempo de execução (serial - CPU) para cada Boid: " + stopwatch.ElapsedMilliseconds + "ms");
    }

    // Função para orbitar ao redor do BoidManager quando estiver próximo demais
    private void OrbitAroundPivot()
    {
        // Rotaciona o boid ao redor do BoidManager
        transform.RotateAround(boidManagerTransform.position, Vector3.up, speed * Time.deltaTime);
    }

