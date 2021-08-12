using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fregegergerge : MonoBehaviour
{
    public GameObject CreaTSpavns;

    public float Currentdfwfd;

   

    // Update is called once per frame
    void Start()
    {
        for(int i = 0; i< 51; i++)
        {
            int b = i + 1;
            GameObject LoadCar = Instantiate<GameObject>(CreaTSpavns);
            LoadCar.transform.position = new Vector3(-25.2f, 0, Currentdfwfd);
            LoadCar.name = ("Spawn_ID_" + b);
            Currentdfwfd -= 17.5f;
        }
    }
}
