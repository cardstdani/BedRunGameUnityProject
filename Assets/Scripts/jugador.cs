using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.PostProcessing;

public class jugador : MonoBehaviour {

    public float sensivityX, sensivityY;
    public WheelCollider frontR, frontL, rearR, rearL;
    public float speed;
    public Transform cameraTransform;
    public float maxSteerAngle;
    public GameObject centerOfMass;
    GameObject menuPerder;
    GameObject menuMeta;
    
    private void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass.transform.localPosition;
        menuPerder = GameObject.Find("MenuPerder");
        menuMeta = GameObject.Find("MenuMeta");
        menuPerder.SetActive(false);
        menuMeta.SetActive(false);

        if (PlayerPrefs.GetInt("PostProcesado") == 0)
        {
            cameraTransform.gameObject.GetComponent<PostProcessingBehaviour>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Meta"))
        {          
            GameObject.Find("FadeMeta").GetComponent<Animator>().enabled = true;
            GameObject.Find("FadeMeta").GetComponent<Animator>().SetBool("Fade", true);
            StartCoroutine(activarMenu(menuMeta));
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

        Vector3 rot = cameraTransform.rotation.eulerAngles;

        rot.x += Input.GetAxis("Mouse Y") * sensivityY;
        rot.y -= Input.GetAxis("Mouse X") * sensivityX;

        cameraTransform.rotation = Quaternion.Euler(rot);

        if (transform.position.y < -30f)
        {
            GameObject.Find("Fade").GetComponent<Animator>().enabled = true;
            GameObject.Find("Fade").GetComponent<Animator>().SetBool("Fade", true);
            StartCoroutine(activarMenu(menuPerder));
        }
    }

    IEnumerator activarMenu(GameObject menu)
    {
        yield return new WaitForSeconds(4f);
        menu.SetActive(true);
        Destroy(this);
    }
}