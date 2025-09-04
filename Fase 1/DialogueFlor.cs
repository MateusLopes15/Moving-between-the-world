using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueFlor : MonoBehaviour
{
    public Dialogue dialogue;
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    private void OnTriggerEnter(Collider falar)
    {
        if (falar.gameObject.tag == "Player")
        {
            TriggerDialogue();
            RotacaoCamera.areadefala = true;
        }
    }
    private void OnTriggerExit(Collider falar)
    {
        if (falar.gameObject.tag == "Player")
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }


}
