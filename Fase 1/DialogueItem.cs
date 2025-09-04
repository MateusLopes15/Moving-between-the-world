using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueItem : MonoBehaviour
{
    public Dialogue dialogue;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }
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
