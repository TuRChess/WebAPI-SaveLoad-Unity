using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameAPI
{
    private readonly HttpClient httpClient;
    private const string BASE_URL = "https://68f974bdef8b2e621e7c1e71.mockapi.io";

    public GameAPI()
    {
        httpClient = new HttpClient();
    }

    #region Player Operations

    /// <summary>
    /// Busca todos os jogadores
    /// </summary>
    public async Task<Player[]> GetAllPlayers()
    {
        try
        {
            string url = $"{BASE_URL}/Player";
            Debug.Log($"GET: {url}");

            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            Debug.Log($"Resposta recebida: {json.Substring(0, Math.Min(200, json.Length))}...");

            // Como JsonUtility não suporta arrays diretamente, vamos usar um wrapper
            string wrappedJson = $"{{\"players\":{json}}}";
            PlayerArray playerArray = JsonUtility.FromJson<PlayerArray>(wrappedJson);

            return playerArray.players;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao buscar players: {ex.Message}");
            return new Player[0];
        }
    }

    /// <summary>
    /// Busca um jogador específico
    /// </summary>
    public async Task<Player> GetPlayer(string id)
    {
        try
        {
            string url = $"{BASE_URL}/Player/{id}";
            Debug.Log($"GET: {url}");

            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            Debug.Log($"Player recebido: {json}");

            Player player = JsonUtility.FromJson<Player>(json);
            return player;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao buscar player {id}: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Atualiza dados do jogador
    /// </summary>
    public async Task<Player> UpdatePlayer(string id, Player player)
    {
        try
        {
            string url = $"{BASE_URL}/Player/{id}";
            Debug.Log($"PUT: {url}");

            string json = JsonUtility.ToJson(player);
            Debug.Log($"JSON sendo enviado: {json}");

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();

            string responseJson = await response.Content.ReadAsStringAsync();
            Debug.Log($"Player atualizado: {responseJson}");

            return JsonUtility.FromJson<Player>(responseJson);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao atualizar player {id}: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Cria novo jogador
    /// </summary>
    public async Task<Player> CreatePlayer(Player player)
    {
        try
        {
            string url = $"{BASE_URL}/Player";
            Debug.Log($"POST: {url}");

            string json = JsonUtility.ToJson(player);
            Debug.Log($"JSON sendo enviado: {json}");

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            string responseJson = await response.Content.ReadAsStringAsync();
            Debug.Log($"Player criado: {responseJson}");

            return JsonUtility.FromJson<Player>(responseJson);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao criar player: {ex.Message}");
            return null;
        }
    }

    #endregion

    public void Dispose()
    {
        httpClient?.Dispose();
    }
}

// Classes auxiliares para deserialização de arrays
[System.Serializable]
public class PlayerArray
{
    public Player[] players;
}
