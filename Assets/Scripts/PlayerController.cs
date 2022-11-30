using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public GameObject Car= null;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Camera;

    public static PlayerController Instance { get; private set; } // static singleton
    void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(Player.transform.position, Car.transform.position));
        if((Vector3.Distance(Player.transform.position, Car.transform.position) < 3) && (Input.GetKeyDown(KeyCode.E)))
        {
            Player.transform.SetParent(Car.transform);
            Player.SetActive(false);
            Car.SetActive(true);
            Camera.GetComponent<CinemachineVirtualCamera>().LookAt = Car.transform;
            Camera.GetComponent<CinemachineVirtualCamera>().Follow = Car.transform;
            Car.GetComponent<PrometeoCarController>().enabled = true;
        }
        else if(Input.GetKeyDown(KeyCode.R)){
            Player.transform.SetParent(null);
            Player.SetActive(true);
            Camera.GetComponent<CinemachineVirtualCamera>().LookAt = Player.transform;
            Camera.GetComponent<CinemachineVirtualCamera>().Follow = Player.transform;
            Car.GetComponent<PrometeoCarController>().enabled = false;
        }
    }
}
