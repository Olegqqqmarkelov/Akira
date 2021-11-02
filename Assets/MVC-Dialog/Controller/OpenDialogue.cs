using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDialogue : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Open()
    {
        anim.SetBool("DialogOpen", true);
    }

    public void Close()
    {
        anim.SetBool("DialogOpen", false);
    }


}
