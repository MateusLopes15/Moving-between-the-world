 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSegue : MonoBehaviour
{
    public Transform cabeca;
    public Transform[] pos;
    public int id;
    public Vector3 vel = Vector3.zero;
    private RaycastHit hit;
    private float rotVel, rotação;


    void Start()
    {
        rotVel = 100;
        id = 0;
    }

    private void Update()
    {
        AjusteCamera();
        RotacaoCam();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(cabeca);
        if (!Physics.Linecast(cabeca.position, pos[id].position))
        {
            transform.position = Vector3.SmoothDamp(transform.position,pos[id].position, ref vel, 0.4f);
            Debug.DrawLine(cabeca.position, pos[id].position);
        }else if(Physics.Linecast(cabeca.position, pos[id].position, out hit))
        {
            transform.position = Vector3.SmoothDamp(transform.position, hit.point, ref vel, 0, 4f);
        }




    }

    void AjusteCamera()
    {
        //if (Input.GetButtonDown("CameraAjuste") && id < 2)
        //{
        //    id++;
        //}
        //else if (Input.GetButtonDown("CameraAjuste") && id > 1)
        //{
        //    id = 0;
        //}
    }

    void RotacaoCam()
    {
        rotação = Input.GetAxis("CameraRot") * rotVel;
        rotação *= Time.deltaTime;
        cabeca.Rotate(0, rotação, 0);
    }
}
