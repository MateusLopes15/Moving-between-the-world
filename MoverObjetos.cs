using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoverObjetos : MonoBehaviour
{

    public string tagObjects = "Respawn";
    public KeyCode teclaRotacionar = KeyCode.R;
    public bool ocultarOMouse = false;
    public bool setarLayerNoPlayer = true;
    public CursorLockMode _cursorLockMode = CursorLockMode.None;

    [SerializeField]
    private MiniBossController script;

    [Space(15)]
    [Range(1.0f, 5.0f)]
    public float distanciaMinima = 2.5f;
    [Range(5.0f, 9.0f)]
    public float distanciaMaxima = 6;
    [Range(1.0f, 10.0f)]
    public float velocidadeDeMovimento = 5;
    [Range(10.0f, 100.0f)]
    public float velocidadeDeRotacao = 50;
    [Range(5.0f, 15.0f)]
    public float velocidadeRoletaMouse = 10;
    [Space(10)]
    public float forcaParaArremessar = 200;
    public float forcaParaMover = 200;
    [Space(15)]
    public Texture texturaMaoFechada;
    public Texture texturaMaoAberta;

    bool canMove;
    bool blockMovement;
    bool isMoving;
    float distance;
    float rotXTemp;
    float rotYTemp;
    float tempDistance;
    RaycastHit tempHit;
    Rigidbody rbTemp;
    Vector3 rayEndPoint;
    Vector3 tempDirection;
    Vector3 tempSpeed;
    Vector3 direcAddForceMode;
    GameObject tempObject;
    public static bool rotatingObject;
    Camera mainCamera;

    void Awake()
    {
        distance = (distanciaMinima + distanciaMaxima) / 2;
        mainCamera = Camera.main;
        if (!mainCamera)
        {
            Debug.LogError("O código não achou nenhuma camera com a tag 'MaiCamera'");
        }
        if (ocultarOMouse)
        {
            Cursor.visible = false;
        }
        Cursor.lockState = _cursorLockMode;
        if (setarLayerNoPlayer)
        {
            GameObject refTemp = transform.root.gameObject;
            refTemp.layer = 2;
            foreach (Transform trans in refTemp.GetComponentsInChildren<Transform>(true))
            {
                trans.gameObject.layer = 2;
            }
        }
    }

    void Update()
    {
        //raycast vector3.down
        if (tempObject)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out tempHit, 5))
            {
                if (tempHit.transform.tag == tagObjects && tempObject.transform.gameObject == tempHit.transform.gameObject)
                {
                    blockMovement = true;

                }
                else
                {
                    blockMovement = false;

                }
            }
            else
            {
                blockMovement = false;
            }
        }
        else
        {
            blockMovement = false;
        }

        //raycast camera forward
        rayEndPoint = transform.position + transform.forward * distance;
        if (Physics.Raycast(transform.position, transform.forward, out tempHit, (distanciaMaxima + 1)))
        {
            if (Vector3.Distance(transform.position, tempHit.point) <= distanciaMaxima && tempHit.transform.tag == tagObjects)
            {
                canMove = true;
            }
            else
            {
                canMove = false;
            }
            //
            if (Input.GetKeyDown(KeyCode.Mouse0) && canMove)
            {
                if (tempHit.rigidbody)
                {
                    tempHit.rigidbody.useGravity = true;
                    distance = Vector3.Distance(transform.position, tempHit.point);
                    tempObject = tempHit.transform.gameObject;
                    isMoving = true;

                }
                else
                {
                    Debug.LogWarning("O objeto que você está tentando arrastar não possui o componente Rigidbody");
                }
            }
        }
        else
        {
            canMove = false;
        }
        distance += Input.GetAxis("Mouse ScrollWheel") * velocidadeRoletaMouse;
        distance = Mathf.Clamp(distance, distanciaMinima, distanciaMaxima);
        if (tempObject)
        {
            rbTemp = tempObject.GetComponent<Rigidbody>();
        }

        if (blockMovement && tempObject)
        {
            rbTemp.useGravity = true;
            tempObject = null;
            rbTemp = null;
            isMoving = false;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && tempObject)
        {
            rbTemp.useGravity = true;
            tempObject = null;
            rbTemp = null;
            isMoving = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && tempObject)
        {


            tempDirection = rayEndPoint - transform.position;
            tempDirection.Normalize();
            rbTemp.useGravity = true;
            rbTemp.AddForce(tempDirection * forcaParaArremessar * 4);
            tempObject = null;
            rbTemp = null;
            isMoving = false;

        
        }
        if (tempObject)
        {
            if (Vector3.Distance(transform.position, tempObject.transform.position) > distanciaMaxima)
            {
                rbTemp.useGravity = true;
                tempObject = null;
                rbTemp = null;
                isMoving = false;
            }
        }

        if (tempObject && mainCamera)
        {
            if (Input.GetKey(teclaRotacionar))
            {
                rotatingObject = true;
                rotXTemp = Input.GetAxis("Mouse X") * velocidadeDeRotacao / 10;
                rotYTemp = Input.GetAxis("Mouse Y") * velocidadeDeRotacao / 10;
                tempObject.transform.Rotate(mainCamera.transform.up, -rotXTemp, Space.World);
                tempObject.transform.Rotate(mainCamera.transform.right, rotYTemp, Space.World);
            }
            if (Input.GetKeyUp(teclaRotacionar))
            {
                rotatingObject = false;
            }
        }
        else
        {
            rotatingObject = false;
        }


    }

    void FixedUpdate()
    {
        if (tempObject)
        {
            rbTemp = tempObject.GetComponent<Rigidbody>();
            rbTemp.angularVelocity = new Vector3(0, 0, 0);
            tempSpeed = (rayEndPoint - rbTemp.transform.position);
            tempSpeed.Normalize();
            tempDistance = Vector3.Distance(rayEndPoint, rbTemp.transform.position);
            tempDistance = Mathf.Clamp(tempDistance, 0, 1);
            direcAddForceMode = tempSpeed * velocidadeDeMovimento * forcaParaMover * tempDistance;
            rbTemp.velocity = Vector3.zero;
            rbTemp.AddForce(direcAddForceMode, ForceMode.Force);
        }
    }

    void OnGUI()
    {
        if (canMove && !isMoving && texturaMaoAberta)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - texturaMaoAberta.width / 2, Screen.height / 2 - texturaMaoAberta.height / 2, texturaMaoAberta.width, texturaMaoAberta.height), texturaMaoAberta);
            


        }
        if (isMoving && texturaMaoFechada)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - texturaMaoFechada.width / 2, Screen.height / 2 - texturaMaoFechada.height / 2, texturaMaoFechada.width, texturaMaoFechada.height), texturaMaoFechada);
        }
    }
} 