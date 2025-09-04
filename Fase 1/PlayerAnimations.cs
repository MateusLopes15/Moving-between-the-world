using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAnimations : MonoBehaviour
{
    public Animator anim;
    bool andar;
    bool andarPTras;
    public static bool ataqueBasico;
    public static bool correr;
    // Start is called before the first frame update
    void Start()
    {
        andar = false;
        correr = false;
    }

    // Update is called once per frame
    void Update()
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
        //Ataque com porrete
        if (Input.GetButtonDown("Fire1") && WeaponsPlayer.WeaponSelect == 1)
        {
            ataqueBasico = true;
            StartCoroutine("DelayAtaquePorrete");
        }

        //selecionar camadas de animaçoes do AC player
        if (WeaponsPlayer.WeaponSelect == 1)
        {
            anim.SetLayerWeight(1,1);
        }
        else
        {
            anim.SetLayerWeight(1, 0);
        }


        //aplicando valores para os parametros do AC
        anim.SetBool("Correr", correr);
        anim.SetBool("Andar", andar);
        anim.SetBool("AndarPTras", andarPTras);
        anim.SetBool("AtaqueBasico", ataqueBasico);
    }
    IEnumerator DelayAtaquePorrete()
    {
        yield return new WaitForSeconds(0.7f);
        ataqueBasico = false;
    }
}
