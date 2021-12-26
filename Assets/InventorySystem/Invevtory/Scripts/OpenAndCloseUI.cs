using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndCloseUI : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Open()
    {
        anim.SetBool("SetActive", true);
    }

    public void Close()
    {
        anim.SetBool("SetActive", false);
    }
}
