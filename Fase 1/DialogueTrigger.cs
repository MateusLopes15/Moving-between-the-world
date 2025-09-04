using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool falaFase2;

	public Dialogue dialogue;
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    private void OnTriggerEnter(Collider falar)
    {
        if (falar.gameObject.tag == "Player" && AtivarAmuleto.anelF1 && DhaliaController.chamarDh && !PegarItem.Pegueipedra)
        {
            TriggerDialogue();
            RotacaoCamera.areadefala = true;
        }

        if (falar.gameObject.tag == "Player" && falaFase2)
        {
            TriggerDialogue();
            RotacaoCamera.areadefala = true;
            WeaponsPlayer.usaPorrete = true;
        }
    }
    private void OnTriggerExit(Collider falar)
    {
        if (falar.gameObject.tag == "Player" && AtivarAmuleto.anelF1 && DhaliaController.chamarDh)
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
        if (falar.gameObject.tag == "Player" && falaFase2)
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }


}
