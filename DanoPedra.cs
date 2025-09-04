using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoPedra : MonoBehaviour
{
    public float dano;
    bool applydano;

    private void Start()
    {
        applydano = true;
    }

    private void Update()
    {
        Collider[] colisor2 = Physics.OverlapSphere(transform.position, 0.2f);
        foreach (Collider colisor2_ in colisor2)
        {
            if (colisor2_.tag == "Inimigo" && applydano)
            {
                SistemaHpMiniBoss p2 = colisor2_.GetComponent<SistemaHpMiniBoss>();
                if (p2 != null)
                {
                    p2.BarraVidaMini.value -= dano;
                    applydano = false;
                    Destroy(this.gameObject);
                }
            }

        }
    }

    IEnumerator TempDano()
    {
        yield return new WaitForSeconds(1);
        applydano = true;
    }
}
