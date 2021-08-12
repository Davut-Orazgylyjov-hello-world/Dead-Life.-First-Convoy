using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheels_Speed_World : MonoBehaviour
{
    public Convoy_Controller Convoy_Controller_Script;
    public Animator Animator_this;


    private void Start()
    {
        //даем своианимации колес
        Convoy_Controller_Script.Animator_Wheels(Animator_this);
        //при старте сразу ставимскорость колес 
        Animator_this.speed = Convoy_Controller_Script.WorldSpeed;
    }
}
