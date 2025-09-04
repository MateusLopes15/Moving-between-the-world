using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpRotacao : MonoBehaviour
{
    //public Transform camMain;
    public Transform camFirst;

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.LookAt(transform.position + camMain.forward);// aponta o objeto em direção ao alvo, no caso hp inimigo sempre visivel
        transform.LookAt(transform.position + camFirst.forward);// aponta o objeto em direção ao alvo, no caso hp inimigo sempre visivel

    }
}
