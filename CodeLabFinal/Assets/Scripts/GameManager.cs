using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int numberOfDrinks;
    public Dictionary<string, int> DrunkAmount = new Dictionary<string, int>();
    public string drunkKey;
    public int drunkValue;
    public HandMotion handScript;
    public List<DialogueHolder> DialogueHolders;
    public TextMeshPro dialogueText;
    public bool fullDrunk = false;
    public bool doubleDrunk = false;
    public GameObject dialogueBox;
    public AudioSource broken;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        numberOfDrinks = 0;
        DrunkAmount.Add("Alc Level", 0);
        ResetKeyAndValue();
        dialogueText.text = DialogueHolders[0].dialogues[0];
    }

    void ResetKeyAndValue()
    {
        drunkKey = null;
        drunkValue = 0;
    }

    public int NumberOfDrinks
    {
        get
        {
            return numberOfDrinks;
        }
        set
        {
            numberOfDrinks = value;
            Debug.Log("Drinks: " + numberOfDrinks);
            AddToDrunkAmount(drunkKey, drunkValue);
            ChangeText(numberOfDrinks, drunkKey);
        }
    }

    public void PlayAudio()
    {
        broken.Play(0);
    }

    public void AddToDrunkAmount(string typeOfAlc, int amount)
    {
        DrunkAmount["Alc Level"] += amount;
        
        if (DrunkAmount.ContainsKey(typeOfAlc))
        {
            DrunkAmount[typeOfAlc] += 1;
        }
        else
        {
            DrunkAmount.Add(typeOfAlc, 1);
        }
        
        handScript.UpdateNumber(DrunkAmount["Alc Level"]);
        
        Debug.Log("You are " + DrunkAmount["Alc Level"] + "% Drunk from the " + typeOfAlc + " you just drank");

        if (DrunkAmount["Alc Level"] >= 100 & numberOfDrinks > 6)
        {
            Debug.Log("You Should Stop!");
            fullDrunk = true;
            numberOfDrinks = 0;
        }
        

        if (DrunkAmount["Alc Level"] >= 160)
        {
            fullDrunk = false;
            Debug.Log("Friend goes silent");
            doubleDrunk = true;
            if (numberOfDrinks > 2 && DrunkAmount["Alc Level"] < 180)
            {
                numberOfDrinks = 0;
            }
        }
    }

    public void ChangeText(int numberOfDrinks, string drunkKey)
    {
        if (!fullDrunk && !doubleDrunk)
        {
                switch (numberOfDrinks)
            {
                case 1:
                    dialogueText.text = DialogueHolders[0].dialogues[1].Replace("<keycode>", drunkKey);
                    break;
                case 2:
                    dialogueText.text = DialogueHolders[0].dialogues[2];
                    break;
                case 3:
                    dialogueText.text = DialogueHolders[0].dialogues[3];
                    break;
                case 4: 
                    dialogueText.text = DialogueHolders[0].dialogues[4];
                    break;
                default:
                    switch (drunkKey)
                    {
                        case "Manhattan":
                            dialogueText.text = DialogueHolders[1].dialogues[Random.Range(0,DialogueHolders[1].dialogues.Count - 1)]; 
                            break;
                        case "Bellini":
                            dialogueText.text = DialogueHolders[2].dialogues[Random.Range(0,DialogueHolders[1].dialogues.Count - 1)]; 
                            break;
                        case "Margaritta":
                            dialogueText.text = DialogueHolders[3].dialogues[Random.Range(0,DialogueHolders[1].dialogues.Count - 1)];
                            break;
                        case "Tequila Sunrise":
                            dialogueText.text = DialogueHolders[4].dialogues[Random.Range(0,DialogueHolders[1].dialogues.Count - 1)];
                            break;
                    }
                    break;
            }
        }

        if (fullDrunk)
        {
            switch (numberOfDrinks)
            {
                case 0:
                    dialogueText.text = DialogueHolders[0].dialogues[numberOfDrinks + 5].Replace
                        ("<keyvalue>", DrunkAmount["Alc Level"].ToString());
                        //fullDrunk = false;
                    break;
                default:
                    dialogueText.text = DialogueHolders[0].dialogues[numberOfDrinks + 5];
                    break;
            }
        }

        if (doubleDrunk)
        {
            switch (numberOfDrinks)
            {
                case 0:
                    dialogueText.text = "......";
                    break;
                default:
                    dialogueText.text = "You know what? That's enough. I will take you outta here --- ";
                    dialogueBox.GetComponent<CapsuleCollider2D>().enabled = true;
                    dialogueBox.GetComponent<Animator>().SetTrigger("DialogueBoil");
                    break;  
            }
        }
        
        ResetKeyAndValue();
    }
}
