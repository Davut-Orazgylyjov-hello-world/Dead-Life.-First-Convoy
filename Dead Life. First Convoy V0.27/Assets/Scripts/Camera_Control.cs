using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{

    public Load_Convoy Load_Convoy_SCRIPT;

    [Header("Камера по x перемещениепо цсене")]
    public GameObject Position_Camera;

    [Header("Номер машину на которую смотрит камера")]
    public int TakedCar;

    [Header("Камера вращение")]
    public GameObject Rotation_Camera;
    public float SpeedCamRotation;

    // Update is called once per frame
    void Update()
    {
        //Смотреть на выбранную ммашину
        {
            if (Position_Camera.transform.position != new Vector3(0, 0, Load_Convoy_SCRIPT.Spawns[TakedCar].transform.position.z))
            {
                Position_Camera.transform.position = Vector3.Lerp(Position_Camera.transform.position, new Vector3(0, 0, Load_Convoy_SCRIPT.Spawns[TakedCar].transform.position.z), 0.025f);
            }
        }

        //вращать камеру
        {
            Rotation_Camera.transform.Rotate(0, SpeedCamRotation*Time.deltaTime, 0);
        }
    }
}
