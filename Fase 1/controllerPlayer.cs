using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class controllerPlayer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Taxa por segundos mantendo pressionada a input (entrada)")]
    private float rotationRate = 360; //Valor para velocidade da rotação do player

    public float Sensibilidade;
    public float andar;
    public float correr;
    public float gravity;

    int ctrlCursor;
    CharacterController controll;
    Vector3 moveDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        ctrlCursor = 0;
        controll = GetComponent<CharacterController>();
    }

    // Update is called once per frame

    private void Awake()
    {
        transform.tag = "Player";
    }

    void Update()
    {
        contreller();
    }
    void contreller()
    {
        //Rotacionar o Player
        //float MouseX = CrossPlatformInputManager.GetAxis("Mouse X");
        //transform.Rotate(0, MouseX * Sensibilidade * Time.deltaTime, 0);
        //para desabilitar a seta do mouse
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ctrlCursor++;
            if (ctrlCursor > 1)
            {
                ctrlCursor = 0;
            }
        }
        if (ctrlCursor == 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        //Character Controller
        if (controll.isGrounded)
        {
            /*As movimentaçoes só serao realizadas caso 
            a variavel ataqueBasico seja igual a falso*/
            if (!PlayerAnimations.ataqueBasico)
            {
                //movimentação do player
                moveDirection = new Vector3(0, 0, CrossPlatformInputManager.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                //para andar
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    moveDirection = moveDirection * andar;
                }
                else
                {
                    //para correr
                    if (CrossPlatformInputManager.GetAxis("Vertical") > 0) {
                        moveDirection = moveDirection * correr;
                    }
                }
            }
        }
        //Simulator - Apply Gravity
        moveDirection.y = moveDirection.y - gravity;
        //aplicando fisica e movimentaçao ao character controller


            controll.Move(moveDirection * Time.deltaTime);
        
    }

    private void ApplyInput(float turnInput)
    {
        Turn(turnInput);
    }



    private void Turn(float input)
    {
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);//rotação do player por teclados
    }



}
