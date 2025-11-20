using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject telaInfo;
    [SerializeField] private TextMeshProUGUI textLife, textItens, textPositions;

    private GameAPI api;
    PlayerControll controlePlayer;

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
        api = new GameAPI();
    }

    public GameAPI Api { get { return api; } }

    private void Start()
    {
        controlePlayer = FindFirstObjectByType<PlayerControll>();

        telaInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!telaInfo.activeSelf)
            {
                UpdateInfoPlayer();
                telaInfo.SetActive(true);
            }
            else
            {
                telaInfo.SetActive(false);
            }
            
        }
    }

    async void UpdateInfoPlayer()
    {
        controlePlayer.SavePositions();
        Player player = controlePlayer.Player;

        if (player.Vida < 0)
            player.Vida = 100;

        player = await api.UpdatePlayer("1", player);
        textLife.text = "Vida: " + player.Vida;
        textItens.text = "Itens: " + player.QuantidadeItens;
        textPositions.text = $"X: {player.PosicaoX} Y: {player.PosicaoY} Z: {player.PosicaoZ}";
    }

    public void ButtonRefresh()
    {
        Refresh();
    }

    async void Refresh()
    {
        Player player = controlePlayer.Player;
        player.Vida = 100;
        player.QuantidadeItens = 0;

        controlePlayer.AnularPosition();
        player = await api.UpdatePlayer("1", player);

        SceneManager.LoadScene(0);
    }

    void OnDestroy()
    {
        api?.Dispose();
    }
}
