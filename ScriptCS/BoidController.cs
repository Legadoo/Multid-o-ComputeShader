using UnityEngine;

public class BoidController : MonoBehaviour
{
    public ComputeShader boidShader;
    public int numAgents = 10000;
    public float separationRadius = 2.0f;
    public float alignmentRadius = 3.0f;
    public float cohesionRadius = 5.0f;
    public float curiosityFactor = 0.1f;

    private ComputeBuffer positionBuffer;
    private ComputeBuffer velocityBuffer;
    private ComputeBuffer newPositionBuffer;
    private ComputeBuffer newVelocityBuffer;
    private int kernelHandle;

    private Vector4[] positions;
    private Vector4[] velocities;

    void Start()
    {
        StartSimulation();
        // stopwatch = new Stopwatch();
        // stopwatch.Start();
        // Inicializar buffers
        // positionBuffer = new ComputeBuffer(numAgents, sizeof(float) * 4);
        // velocityBuffer = new ComputeBuffer(numAgents, sizeof(float) * 4);
        // newPositionBuffer = new ComputeBuffer(numAgents, sizeof(float) * 4);
        // newVelocityBuffer = new ComputeBuffer(numAgents, sizeof(float) * 4);

        // // Inicializar arrays para armazenamento
        // positions = new Vector4[numAgents];
        // velocities = new Vector4[numAgents];

        // // Encontrar o kernel correto
        // kernelHandle = boidShader.FindKernel("main"); // Verifique o nome do kernel no shader

        // if (kernelHandle < 0)
        // {
        //     Debug.LogError("Kernel 'main' not found. Please verify the ComputeShader.");
        //     return;
        // }

        // boidShader.SetBuffer(kernelHandle, "PositionBuffer", positionBuffer);
        // boidShader.SetBuffer(kernelHandle, "VelocityBuffer", velocityBuffer);
        // boidShader.SetBuffer(kernelHandle, "NewPositionBuffer", newPositionBuffer);
        // boidShader.SetBuffer(kernelHandle, "NewVelocityBuffer", newVelocityBuffer);
        // stopwatch.Stop();
        // UnityEngine.Debug.Log("Tempo de execução (paralela - GPU): " + stopwatch.ElapsedMilliseconds + "ms");
    }
    
    
    
    
    public void StartSimulation()
    {
        positionBuffer = new ComputeBuffer(numAgents, sizeof(float) * 4);
        velocityBuffer = new ComputeBuffer(numAgents, sizeof(float) * 4);
        newPositionBuffer = new ComputeBuffer(numAgents, sizeof(float) * 4);
        newVelocityBuffer = new ComputeBuffer(numAgents, sizeof(float) * 4);

        // Inicializar arrays para armazenamento
        positions = new Vector4[numAgents];
        velocities = new Vector4[numAgents];

        // Encontrar o kernel correto
        kernelHandle = boidShader.FindKernel("main"); // Verifique o nome do kernel no shader

        if (kernelHandle < 0)
        {
            Debug.LogError("Kernel 'main' not found. Please verify the ComputeShader.");
            return;
        }

        boidShader.SetBuffer(kernelHandle, "PositionBuffer", positionBuffer);
        boidShader.SetBuffer(kernelHandle, "VelocityBuffer", velocityBuffer);
        boidShader.SetBuffer(kernelHandle, "NewPositionBuffer", newPositionBuffer);
        boidShader.SetBuffer(kernelHandle, "NewVelocityBuffer", newVelocityBuffer);

    }

    

    void Update()
    {
        if (kernelHandle < 0) return; // Evitar exceções se o kernel não foi encontrado

        // Definir parâmetros e executar o shader
        boidShader.SetFloat("separationRadius", separationRadius);
        boidShader.SetFloat("alignmentRadius", alignmentRadius);
        boidShader.SetFloat("cohesionRadius", cohesionRadius);
        boidShader.SetFloat("curiosityFactor", curiosityFactor);
        boidShader.SetVector("pivotPosition", transform.position);

        int threadGroups = Mathf.CeilToInt(numAgents / 256.0f);
        boidShader.Dispatch(kernelHandle, threadGroups, 1, 1);

        // Atualizar posições e velocidades
        newPositionBuffer.GetData(positions);
        newVelocityBuffer.GetData(velocities);
    }

    void OnDestroy()
    {
        // Limpar buffers
        positionBuffer.Release();
        velocityBuffer.Release();
        newPositionBuffer.Release();
        newVelocityBuffer.Release();
    }

    // Métodos adicionais para obter posição e velocidade dos peixes
    public Vector3 GetPositionForFish(Transform fishTransform)
    {
        int index = Mathf.FloorToInt(fishTransform.GetInstanceID() % numAgents);
        return new Vector3(positions[index].x, positions[index].y, positions[index].z);
    }

    public Vector3 GetVelocityForFish(Transform fishTransform)
    {
        int index = Mathf.FloorToInt(fishTransform.GetInstanceID() % numAgents);
        return new Vector3(velocities[index].x, velocities[index].y, velocities[index].z);
    }
}
