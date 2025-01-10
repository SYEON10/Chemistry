using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogManager : Singleton<DialogManager>
{
    public DialogueRunner dialogueRunner;

    void Start()
    {
        StartDialogue("Start");
    }

    public void StartDialogue(string filename)
    {
        dialogueRunner.StartDialogue(filename);
    }
}
