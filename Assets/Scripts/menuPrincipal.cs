using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using Prototype.NetworkLobby;

public class menuPrincipal : MonoBehaviour
{
    public TextMeshProUGUI textoLongitud, textoLongitudMp;
    int longitud;
    int vehiculo;
    public List<Sprite> imagenCoche;
    public List<GameObject> vehiculoObject;

    public Slider vehiculoSlider, vehiculoSliderMP;
    public TMP_Dropdown graficosDrop, resolucionesDrop;
    public Toggle postProcesadoToggle, fullScreenToggle;
    public LobbyManager lobbyManager;
    Resolution[] resoluciones;
    int vecesAbiertas;

    private void Start()
    {
        vecesAbiertas = PlayerPrefs.GetInt("VecesAbiertas");
        vecesAbiertas++;
        PlayerPrefs.SetInt("VecesAbiertas", vecesAbiertas);

        if (vecesAbiertas <= 1)
        {
            PlayerPrefs.SetInt("PostProcesado", 1);
        }

        longitud = PlayerPrefs.GetInt("Longitud");
        if (longitud < 50) { longitud = 50; PlayerPrefs.SetInt("Longitud", 50); }
        textoLongitud.text = longitud.ToString(); textoLongitudMp.text = longitud.ToString();

        vehiculo = PlayerPrefs.GetInt("Vehiculo");
        vehiculoSlider.value = PlayerPrefs.GetInt("Vehiculo");
        vehiculoSliderMP.value = PlayerPrefs.GetInt("Vehiculo");
        lobbyManager.gamePlayerPrefab = vehiculoObject[PlayerPrefs.GetInt("Vehiculo")];
        GameObject.Find("Panel").GetComponent<Image>().sprite = imagenCoche[vehiculo];
        graficosDrop.value = QualitySettings.GetQualityLevel();
        GameObject.Find("LobbyManager").GetComponent<LobbyManager>().posicion = 0;
        GameObject.Find("LobbyManager").GetComponent<LobbyManager>().todosHanAcabado = false;

        if (PlayerPrefs.GetInt("PostProcesado") == 0) { postProcesadoToggle.isOn = false; } else { postProcesadoToggle.isOn = true; }

        resoluciones = Screen.resolutions;
        resolucionesDrop.ClearOptions();
        List<string> opciones = new List<string>();
        int indiceActual = 0;
        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + "x" + resoluciones[i].height;
            opciones.Add(opcion);

            if (resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height)
            {
                indiceActual = i;
            }
        }
        resolucionesDrop.AddOptions(opciones);
        resolucionesDrop.value = indiceActual;
        resolucionesDrop.RefreshShownValue();
        Screen.SetResolution(800, 600, Screen.fullScreenMode = FullScreenMode.Windowed);
    }

    public void Resolucion(int indiceResolucion)
    {
        Resolution resolucion = resoluciones[indiceResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

    public void fullscreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void graficos(int numeroGraficos)
    {
        QualitySettings.SetQualityLevel(numeroGraficos);
    }

    public void togglePostProcesado(bool activado)
    {
        if (activado == false) { PlayerPrefs.SetInt("PostProcesado", 0); } else { PlayerPrefs.SetInt("PostProcesado", 1); }
    }

    public void salir()
    {
        Application.Quit();
    }

    public void jugar()
    {
        Destroy(GameObject.Find("LobbyManager").gameObject);
        SceneManager.LoadSceneAsync("Juego");
    }

    public void multijugador()
    {
        SceneManager.LoadScene("Multijugador");
    }

    public void aumentar()
    {
        longitud += 2;
        PlayerPrefs.SetInt("Longitud", longitud);
        textoLongitud.text = longitud.ToString(); textoLongitudMp.text = longitud.ToString();
    }

    public void disminuir()
    {
        if (longitud > 50)
        {
            longitud -= 2;
            PlayerPrefs.SetInt("Longitud", longitud);
            textoLongitud.text = longitud.ToString(); textoLongitudMp.text = longitud.ToString();
        }
    }

    public void SetVehiculo(float vehiculo)
    {
        int vehiculoVariable = Mathf.RoundToInt(vehiculo);
        PlayerPrefs.SetInt("Vehiculo", vehiculoVariable);
        GameObject.Find("Panel").GetComponent<Image>().sprite = imagenCoche[vehiculoVariable];
        lobbyManager.gamePlayerPrefab = vehiculoObject[PlayerPrefs.GetInt("Vehiculo")];
    }
}
