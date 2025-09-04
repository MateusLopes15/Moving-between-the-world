using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;///<summary> Gerencia o controle de locomoção do player.
using Cinemachine;
using UnityEngine.UI;



[RequireComponent(typeof(Animator))] //Obriga o componente Animator
public class ControlePlayer : MonoBehaviour
{
    private const string AxisName = "Vertical";
   
    //--------------------------
    //novo sistema de animação
    CharacterController controll;
    Vector3 moveDirection;
    [Header("Ajuste Sistema de andar")]
    public float Sensibilidade;
    public float andarr;
    public float correrr;
    public float gravity;

    [Header("Ajuste Sistema de Vida")]
    public Slider vidaPlayer;
    public float vidaMaxima = 100;
    public static bool isDeath;

    public static bool andar;
    public static bool correr;
    public static bool andarPTras;
    public static bool ataqueBasico;
    public static bool porreteDanoDelay;
    public static bool arremesso;
    public static bool coletarObj;
    public static bool objLeve;
    public static bool objPesado;
    public static bool isPaused;

    public static bool pegandoItem;

    //--------------------------
    [Header("Painel e Menu")]
    public GameObject pausePainel;
    public GameObject pausedButton;
    public string cena;
    public GameObject gameOverPainel;
    public GameObject playerGameOver;



    //--------------------------
    [Header("Camera Configuração")]
    public CinemachineVirtualCamera vcam;
    public float[] posCam;
    public static int id;
    public CinemachineFramingTransposer composer; //Body do cinemachine

    //--------------------------
    [SerializeField]
    [Tooltip("Taxa por segundos mantendo pressionada a input (entrada)")]
    private float rotationRate = 360; //Valor para velocidade da rotação do player

    private string turnInputAxis = "Horizontal"; // "a" e "d" ou setas "esquerda" e "direita"

    private Animator anim; //Declarando o animator como uma variável

    [SerializeField]

    public Rigidbody rb;
    public float JumpForce = 100;

    public static int ctrlCursor; //variavel para travar cursor dou mouse


    //public LayerMask Layermask;
    public bool IsGrounded;
    public float GroundCheckSize;
    public Vector3 GroundCheckPosition;

    public float vida = 100;

    public Animator gameOverCanvas;

    void Start()
    {

        //--------------------------
        controll = GetComponent<CharacterController>();
        correr = false;
        andar = false;
        ataqueBasico = false;
        arremesso = false;
        coletarObj = false;
        objLeve = false;
        objPesado = false;
        pegandoItem = false;
        isPaused = false;
        isDeath = false;
        //-------------------------- camera
        composer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        composer.m_CameraDistance = posCam[id];
        //--------------------------
        vidaPlayer.minValue = 0;
        vidaPlayer.maxValue = vidaMaxima;
        vidaPlayer.value = vidaMaxima;
        isDeath = false;
        ctrlCursor = 0;
    }

    private void Awake()
    {

        anim = GetComponent<Animator>(); //Atribuindo o componente a variável antes da inicialização
        transform.tag = "Player"; // Da tag de Player 
        RotacaoCamera.areadefala = false;

    }

    private void Update()
    {
        if(vidaPlayer.value <= 0)
        {
            isDeath = true;
        }
        //if (isDeath)
        //{
        //    gameOverPainel.SetActive(true);
        //    playerGameOver.SetActive(false);
        //    //RotacaoCamera.areadefala = true;
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //    Time.timeScale = 0;
        //}
        
        if (!isPaused && !ataqueBasico)
        {
            if (!coletarObj && !pegandoItem && !arremesso)
            {
                Movement();
                contreller();
            }
            else
            {
                correr = false;
                andar = false;
                andarPTras = false;
            }
            Animacao();
        }

        if (Input.GetKeyDown(KeyCode.P)){
            PauseScreen();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }
        if (Input.GetButtonDown("CameraAjust"))
        {
            AjusteCamera();
            composer.m_CameraDistance = posCam[id];
        }

    }   
 


    //Controle de Pause
    //-------------------------------------------
    void PauseScreen()
    {
        if (isPaused)
        {
            isPaused = false;
            pausePainel.SetActive(false);
            pausedButton.SetActive(true);
            //RotacaoCamera.areadefala = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
        else //Pausando o jogo
        {
            isPaused = true;
            pausePainel.SetActive(true);
            pausedButton.SetActive(false);
            //RotacaoCamera.areadefala = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Reccomeçando");
        isDeath = false;
        //gameOverPainel.SetActive(false);
        //playerGameOver.SetActive(true);

    }
    public void PlayGame()
    {
        isPaused = false;
        pausePainel.SetActive(false);
        pausedButton.SetActive(true);
        //RotacaoCamera.areadefala = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        isPaused = true;
        pausePainel.SetActive(true);
        pausedButton.SetActive(false);
        //RotacaoCamera.areadefala = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }
    public void GameOver()
    {

    }
    public void MenuGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void Animacao()
    {
        //-----------------------------------------------
        //aplicando valores para os parametros do AC
        anim.SetBool("Correr", correr);
        anim.SetBool("Andar", andar);
        anim.SetBool("AndarPTras", andarPTras);
        anim.SetBool("AtaqueBasico", ataqueBasico);
        anim.SetBool("Arremesso", arremesso);
        anim.SetBool("ColetarObj", coletarObj);
        anim.SetBool("ObjLeve", objLeve);
        anim.SetBool("ObjPesado", objPesado);

        //---------------------------------------------------------
    }
    //---------------------------------------------
    void Movement()
    {
            /*As animaçoes só serao realizadas caso 
            a variavel ataqueBasico seja igual a falso*/
            if (!ataqueBasico)
            {
                //sistema de animar (correr/andar)
                if (Input.GetKey(KeyCode.LeftShift) && CrossPlatformInputManager.GetAxis("Vertical") > 0)
                {
                    correr = true;
                    andar = false;
                }
                else
                {
                    if (!Input.GetKey(KeyCode.LeftShift) && CrossPlatformInputManager.GetAxis("Vertical") > 0)
                    {
                        correr = false;
                        andar = true;
                    }
                    else
                    {
                        correr = false;
                        andar = false;
                    }
                }
                //andando para tras
                if (CrossPlatformInputManager.GetAxis("Vertical") < 0)
                {
                    andarPTras = true;
                }
                else
                {
                    andarPTras = false;
                }
            }

            //Ataque com porrete selecionado atraves do script WeaponsPlayer
            if (Input.GetButtonDown("Fire1") && WeaponsPlayer.WeaponSelect == 1)
            {
                ataqueBasico = true; //PlayerAnimations da controle
                porreteDanoDelay = true;
                StartCoroutine("DelayAtaquePorrete");
            }

            //Arremessar pedra
            if (Input.GetButtonDown("Fire1") && WeaponsPlayer.WeaponSelect == 2)
            {
                arremesso = true; //PlayerAnimations da controle
            }


            //---------------------------------------------------
            //selecionar camadas de animaçoes do AC player
            //dar peso a camada de animação do porrete
            if (WeaponsPlayer.WeaponSelect == 1)
            {
                anim.SetLayerWeight(1, 1);
            }
            else
            {
                anim.SetLayerWeight(1, 0);
            }
            //dar peso a camada de animação de arremesso
            if (WeaponsPlayer.WeaponSelect == 2)
            {
                anim.SetLayerWeight(2, 1);
            }
            else
            {
                anim.SetLayerWeight(2, 0);
            }

            //---------------------------------------------------------


    
        // parte do pulo
        //private void ondrawgizmos()
        //{
        //    gizmos.color = color.red;
        //    gizmos.drawwiresphere(transform.position + groundcheckposition, groundchecksize);
        //}
        //----------------------------------------------------------------------------
        if (!ataqueBasico)
        {
            float turnAxis = Input.GetAxis(turnInputAxis);

            ApplyInput(turnAxis);

            //anim.SetFloat("Horizontal", Input.GetAxis("Horizontal")); //Atribui o valor da variável inputX para o parâmetro Horizontal do controle de animação
            //anim.SetFloat("Vertical", Input.GetAxis("Vertical")); //Atribui o valor da variável inputY para o parâmetro Vertical do controle de animação

            ////Correndo ou não
            //if (Input.GetKey(KeyCode.LeftShift))
            //    anim.SetBool("Run", true); //Segurando o shift esquerdo a bool Run do controle de animação recebe true
            //else
            //    anim.SetBool("Run", false); //Soltando o shift esquerdo a bool Run do controle de animação recebe false

            //.........................................................................................................................................

            //Pulando ou não

            //var groundcheck = physics.overlapsphere(transform.position + groundcheckposition, groundchecksize, layermask);
            //if (groundcheck.length != 0)
            //{
            //    isgrounded = true;

            //}
            //else
            //{
            //    isgrounded = false;

            //}
            //anim.setbool("jump", !isgrounded); //fuciona so que animação fica bugada


            //if (isgrounded == true && input.getbuttondown("jump"))
            //{
            //    rb.addforce(transform.up * jumpforce, forcemode.impulse);

            //}


        }
    }

    IEnumerator DelayAtaquePorrete()
    {

        yield return new WaitForSeconds(1.6f);
        ataqueBasico = false;
        porreteDanoDelay = false;
    }


    private void ApplyInput(float turnInput)
    {
        Turn(turnInput);
    }



    private void Turn(float input)
    {
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);//rotação do player por teclados
    }

    void contreller()
    {
        //para desabilitar a seta do mouse
        if (Input.GetKeyDown(KeyCode.Escape))
            ctrlCursor++;//sempre adiciona +1 a  variavel
            if(ctrlCursor > 1)
            {
            ctrlCursor = 0;
            }
        if (ctrlCursor == 0 && RotacaoCamera.areadefala == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (controll.isGrounded)
        {
            /*As movimentaçoes só serao realizadas caso 
            a variavel ataqueBasico seja igual a falso*/
            if (!ataqueBasico)
            {
                //movimentação do player
                moveDirection = new Vector3(0, 0, CrossPlatformInputManager.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                //para andar
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    moveDirection = moveDirection * andarr;
                }
                else
                {
                    //para correr
                    if (CrossPlatformInputManager.GetAxis("Vertical") > 0)
                    {
                        moveDirection = moveDirection * correrr;
                    }
                }
            }
        }
        //Simulator - Apply Gravity
        moveDirection.y = moveDirection.y - gravity;
        //aplicando fisica e movimentaçao ao character controller


        controll.Move(moveDirection * Time.deltaTime);

        ////Rotação do player por mouse
        //float MouseX = CrossPlatformInputManager.GetAxis("Mouse X");
        //transform.Rotate(0, MouseX * Sensibilidade * Time.deltaTime, 0);




    }
 
    void AjusteCamera()
    {
        if (Input.GetButtonDown("CameraAjust") && id < 2)
        {
            id++;
        }
        else if (Input.GetButtonDown("CameraAjust") && id > 1)
        {
            id = 0;
        }
    }

}  