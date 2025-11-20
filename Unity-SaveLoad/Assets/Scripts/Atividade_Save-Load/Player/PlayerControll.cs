using System.Collections;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    [SerializeField] private float moveSpeed, rotationSpeed;

    [Header("Referências")]
    [SerializeField] private Transform cameraTransform;

    private Rigidbody rb;
    private Vector3 moveDirection;

    private Player player;
    [SerializeField] private GameObject saveText;

    async void Start()
    { 
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Evita que ele tombe fisicamente

        saveText = GameObject.Find("SaveText");
        saveText.SetActive(false);

        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        player = await UIManager.instance.Api.GetPlayer("1");

        if (player == null)
        {
            Debug.LogError("Erro: API retornou player nulo");
            return;
        }

        // Proteção importante
        if (player.Vida <= 0)
        {
            Debug.LogWarning("Player veio com Vida <= 0, corrigindo para 100");
            player.Vida = 100;
        }
    }

    public Player Player { get { return player; } }

    void Update()
    {
        // Pega entrada do teclado
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Direção no plano XZ (sem interferir no Y)
        Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;

        if (inputDir.magnitude >= 0.1f)
        {
            // Calcula o ângulo relativo a câmera
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float smoothedAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, Time.deltaTime * rotationSpeed);

            // Rotaciona o player suavemente
            transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);

            // Define direção baseada na câmera
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        // Aplica movimento com velocidade constante
        Vector3 velocity = moveDirection.normalized * moveSpeed;
        velocity.y = rb.linearVelocity.y; // Mantém gravidade do Rigidbody
        rb.linearVelocity = velocity;
    }

    public void SavePositions()
    {
        player.PosicaoX = (int)transform.position.x;
        player.PosicaoY = (int)transform.position.y;
        player.PosicaoZ = (int)transform.position.z;
    }

    public void AnularPosition()
    {
        player.PosicaoX = 0;
        player.PosicaoY = 0;
        player.PosicaoZ = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            saveText.SetActive(true);
            StartCoroutine(WaitSave());
        }
    }

    private IEnumerator WaitSave()
    {
        yield return new WaitForSeconds(2f);
        saveText.SetActive(false);
    }
}
