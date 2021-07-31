using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Prototype.NetworkLobby;
using TMPro;

public class mpCanvas : NetworkBehaviour
{
    public GameObject cuadradito;
    public GameObject contenido;
    public GameObject normal, hanAcabado, botonObservador;
    public int posicionJugador;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void actualizarValoresMeta()
    {
        normal.SetActive(false);
        hanAcabado.SetActive(true);
        botonObservador.SetActive(false);
        GameObject.Find("TextoPosicion").GetComponent<TextMeshProUGUI>().text = "YOU HAVE BEEN IN " + " " +  posicionJugador.ToString() + " POSITION";
    }

    public void menu()
    {
        NetworkManager.singleton.client.Disconnect();
        GameObject.Find("LobbyManager").GetComponent<LobbyManager>().posicion = 0;
        SceneManager.LoadSceneAsync("Principal");
    }
}
