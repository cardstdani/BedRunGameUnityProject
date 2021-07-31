using UnityEngine;
using TMPro;

public class contador : MonoBehaviour
{
    public TextMeshProUGUI textoNumero;
    public float timer;
    public float rate;

    private void Start()
    {
        GetComponent<jugador>().enabled = false;
        textoNumero = GameObject.Find("TextoNumero").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        textoNumero.text = timer.ToString("f0");

        if (timer > rate) return;

        GetComponent<jugador>().enabled = true;
        textoNumero.text = "";
        Destroy(this);
    }
}
