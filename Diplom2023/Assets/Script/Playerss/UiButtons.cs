using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButtons : MonoBehaviour
{
    public static bool sprintInput = false;
    public static Animator anim;
    public GameObject player;

    public void Start()
    {
        player = GameObject.Find(PlayerManager.YouPerson + "(Clone)");
        anim = player.GetComponent<Animator>();
    }
    public void Update()
    {
        if(player == null || anim == null)
        {
            player = GameObject.Find(PlayerManager.YouPerson + "(Clone)");
            anim = player.GetComponent<Animator>();
        }
    }
    public void ShiftPressed()
    {
        sprintInput = true;
        
    }
    public void ShiftOnPressed()
    {
        sprintInput = false;
    }

    public void Attack()
    {
        anim.Play("Attack_Sword");
    }
}
