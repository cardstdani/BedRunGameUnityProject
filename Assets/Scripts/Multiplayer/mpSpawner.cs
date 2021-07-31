using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class mpSpawner : NetworkBehaviour
{
    public GameObject meta;
    public GameObject camas;
    public float espacio;
    public int puedeGirar = 7;

    public void spawn()
    {
        StartCoroutine(spawn2());
    }

    IEnumerator spawn2()
    {
        yield return new WaitForSeconds(.3f);
        for (int i = 0; i < PlayerPrefs.GetInt("Longitud"); i++)
        {
            if (puedeGirar <= 0 && Random.Range(1, 6) == 3)
            {
                if (Random.Range(1, 3) == 1)
                {
                    transform.Translate(Vector3.forward * 9.29f);
                    transform.Rotate(new Vector3(0f, 90f, 0f));
                    transform.Translate(Vector3.forward * espacio);
                }
                else
                {
                    transform.Translate(Vector3.forward * 9.29f);
                    transform.Rotate(new Vector3(0f, -90f, 0f));
                    transform.Translate(Vector3.forward * espacio * 3.5f);
                }
                puedeGirar = Random.Range(10, 20);
            }
            else
            {
                transform.Translate(Vector3.forward * 9.29f);
            }

            NetworkServer.Spawn(Instantiate(camas, transform.position, transform.rotation));
            puedeGirar--;
        }

        transform.Translate(Vector3.forward * 9.29f);
        NetworkServer.Spawn(Instantiate(meta, transform.position, transform.rotation));
    }
}
