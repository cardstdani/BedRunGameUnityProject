using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.PostProcessing;
using Prototype.NetworkLobby;

public class mpJugador : NetworkBehaviour
{
    public float sensivityX, sensivityY;
    public WheelCollider frontR, frontL, rearR, rearL;
    public float speed;
    public Transform camaraTransform;
    public float maxSteerAngle;
    public GameObject centerOfMass;
    public GameObject menuPerder;
    public GameObject menuMeta;
    public GameObject camaraFinal;
    public Behaviour[]componentesDesactivados;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            componentesDesactivados[0].enabled = false;
            Destroy(this);
        }

        camaraTransform = componentesDesactivados[0].gameObject.transform;

        if (PlayerPrefs.GetInt("PostProcesado") == 0)
        {
            camaraTransform.gameObject.GetComponent<PostProcessingBehaviour>().enabled = false;
        }

        GetComponent<Rigidbody>().centerOfMass = centerOfMass.transform.localPosition;
        StartCoroutine(desactivarMenu());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Meta"))
        {
            meta();
        }
    }

    void Update()
    {
        frontL.motorTorque = Input.GetAxis("Vertical") * speed;
        rearR.motorTorque = Input.GetAxis("Vertical") * speed;
        frontR.motorTorque = Input.GetAxis("Vertical") * speed;
        rearL.motorTorque = Input.GetAxis("Vertical") * speed;

        frontL.steerAngle = Input.GetAxis("Horizontal") * maxSteerAngle;
        frontR.steerAngle = Input.GetAxis("Horizontal") * maxSteerAngle;

        Vector3 rot = camaraTransform.rotation.eulerAngles;

        rot.x += Input.GetAxis("Mouse Y") * sensivityY;
        rot.y -= Input.GetAxis("Mouse X") * sensivityX;

        camaraTransform.rotation = Quaternion.Euler(rot);

        if (transform.position.y < -30f)
        {
            fin();
        }
    }

    public void fin()
    {
        GameObject.Find("Fade").GetComponent<Animator>().enabled = true;
        GameObject.Find("Fade").GetComponent<Animator>().SetBool("Fade", true);
        StartCoroutine(activarMenu());
    }

    public void meta()
    {
        GameObject.Find("Fade Meta").GetComponent<Animator>().enabled = true;
        GameObject.Find("Fade Meta").GetComponent<Animator>().SetBool("Fade", true);
        StartCoroutine(activarMenuMeta());
    }

    IEnumerator activarMenu()
    {
        yield return new WaitForSeconds(4f);
        menuPerder.SetActive(true);
    }

    IEnumerator activarMenuMeta()
    {
        yield return new WaitForSeconds(4f);
        menuMeta.SetActive(true);
        Instantiate(camaraFinal, GameObject.Find("mpMeta(Clone)").gameObject.transform.position, GameObject.Find("mpMeta(Clone)").gameObject.transform.rotation);
        Destroy(componentesDesactivados[0].gameObject);
        Destroy(GameObject.Find("Fade Meta").gameObject);
        Destroy(GameObject.Find("Fade").gameObject);
        GameObject.Find("Canvas").GetComponent<mpCanvas>().posicionJugador = GameObject.Find("LobbyManager").GetComponent<LobbyManager>().posicion;
        Destroy(this.gameObject);
    }

    IEnumerator desactivarMenu()
    {
        yield return new WaitForSeconds(1f);
        menuPerder = GameObject.Find("Menu Perder");
        menuMeta = GameObject.Find("Menu Meta");
        GameObject adorno = GameObject.Find("Adorno");
        menuPerder.SetActive(false);
        menuMeta.SetActive(false);
        DestroyImmediate(adorno);
    }
}