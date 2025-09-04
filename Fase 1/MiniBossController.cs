using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiniBossController : MonoBehaviour
{
    public float patrulhaTempo = 10;
    private WaitForSeconds tempo; //evitar acumulo de lixo
    public Transform[] waypoints; //variavel que ira receber os pontos onde serão feito as patrulhas
    private int index; //utilizar para passar de um ponto para outro
    private Animator anim;
    private NavMeshAgent agent;

    [SerializeField]
    private GameObject player;
    private bool pegaEle = false;
    [SerializeField]
    private float dist = 12;
    [SerializeField]
    private float distAtaque;

    public bool ataca = false;

    public int HpMiniBoss;



    // Start is called before the first frame update
    void Start()
    {
        
        tempo = new WaitForSeconds(patrulhaTempo);// tempo a ser usado na rotina    
        agent = GetComponent<NavMeshAgent>();
        index = Random.Range(0, waypoints.Length);
        anim = GetComponent<Animator>();
        StartCoroutine(ChamaPatrulha());

        distAtaque = agent.stoppingDistance;
    }

    private void Update()
    {
        anim.SetFloat("Move", agent.velocity.sqrMagnitude, 0.06f, Time.deltaTime);
        PegaHeroi();
        AtaqueVilao();

    }

    IEnumerator ChamaPatrulha()
    {
        while (true)
        {
            yield return tempo;
            Patrol();


        }
    }

    void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1;
        agent.destination = waypoints[index].position;

    }

    void PegaHeroi()
    {
        if (player != null && Vector3.Distance(transform.position, player.transform.position) < dist && !pegaEle)
        {
            pegaEle = true;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > dist)
        {
            pegaEle = false;
        }

        if (pegaEle)
        {
            agent.destination = player.transform.position;
        }

    }

    void AtaqueVilao()
    {

        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= distAtaque && pegaEle)
        {
            anim.SetBool("Ataque", true);
            ataca = true;

        }
        else if (player != null && Vector3.Distance(transform.position, player.transform.position) > distAtaque && pegaEle)
        {
            anim.SetBool("Ataque", false);
        }

        if (ataca)
        {
            agent.speed = 0;
            agent.isStopped = true;
        }
        else
        {
            agent.speed = 5;
            agent.isStopped = false;
        }
    }


    void AnulaAtaque()
    {
        ataca = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, dist); //determina vizibilidade de distância de perseguição azul.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distAtaque);//determina vizibilidade de distância de ataque vermelho.

    }
   
}