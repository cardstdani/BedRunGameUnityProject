using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using Prototype.NetworkLobby;

public class meta : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            StartCoroutine(terminar());
        }
    }

    IEnumerator terminar()
    {
        yield return new WaitForSeconds(3.8f);
        GameObject.Find("LobbyManager").GetComponent<LobbyManager>().updateJugadoresTerminado();
    }
}
