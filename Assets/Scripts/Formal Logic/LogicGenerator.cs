using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicGenerator : MonoBehaviour
{
    public string[] WordList;
    string Subject, Middle, Predicate;

    string Figure_Minor;
    string Figure_Major;

    enum Syllogism { All, Some, No, Some_Not };

    Syllogism[] Logic = new Syllogism[3];

    public Text Text_Major, Text_Minor, Text_Conclusion;

    // Start is called before the first frame update
    void Start()
    {
        GenerateSyllogism();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GenerateSyllogism();
        }
    }

    void GenerateSyllogism()
    {
        AssignSMP();
        GenerateFigure();
        GenerateLogic();

        for (int i = 0; i < 3; i++)
        {
            string ObjectA, ObjectB, Output;

            switch (i)
            {
                case 0:
                    // Set object strings to Predicate, Subject, or Middle based on Figure strings
                    if (Figure_Major[0] == 'P')
                    {
                        ObjectA = Predicate;
                    }
                    else
                    {
                        ObjectA = Middle;
                    }

                    if (Figure_Major[1] == 'P')
                    {
                        ObjectB = Predicate;
                    }
                    else
                    {
                        ObjectB = Middle;
                    }

                    Output = Logic[i].ToString() + " " + ObjectA + " are " + ObjectB;
                    Text_Major.text = Output;
                    break;

                case 1:
                    if (Figure_Minor[0] == 'S')
                    {
                        ObjectA = Subject;
                    }
                    else
                    {
                        ObjectA = Middle;
                    }

                    if (Figure_Minor[1] == 'S')
                    {
                        ObjectB = Subject;
                    }
                    else
                    {
                        ObjectB = Middle;
                    }

                    Output = Logic[i].ToString() + " " + ObjectA + " are " + ObjectB;
                    Text_Minor.text = Output;
                    break;

                case 2:
                    // Conclusion is always Subject Predicate order
                    Output = "Therefore, " + Logic[i].ToString() + " " + Subject + " are " + Predicate;
                    Text_Conclusion.text = Output;
                    break;
            };
        }
    }

    void AssignSMP()
    {
        List<int> usedIndex = new List<int>();
        int wordIndex;

        for (int i = 0; i < 3; i ++)
        {
            // do while loop to make sure we don't repeat using a word for SMP
            bool bIndexRepeated = false;
            do {
                wordIndex = Random.Range(0, WordList.Length);

                for (int j = 0; j < usedIndex.Count; j++)
                {
                    if (wordIndex == usedIndex[j])
                    {
                        bIndexRepeated = true;
                    }
                    else
                    {
                        bIndexRepeated = false;
                    }
                }
            } while (bIndexRepeated);

            usedIndex.Add(wordIndex);

            switch (i)
            {
                case 0:
                    Subject = WordList[wordIndex];
                    break;

                case 1:
                    Middle = WordList[wordIndex];
                    break;

                case 2:
                    Predicate = WordList[wordIndex];
                    break;
            };
        }

        Debug.Log(Subject + Middle + Predicate);
    }

    void GenerateFigure()
    {
        int i = Random.Range(0, 4);
        switch (i)
        {
            case 0:
                Figure_Major = "MP";
                Figure_Minor = "SM";
                break;

            case 1:
                Figure_Major = "PM";
                Figure_Minor = "MS";
                break;

            case 2:
                Figure_Major = "PM";
                Figure_Minor = "SM";
                break;

            case 3:
                Figure_Major = "MP";
                Figure_Minor = "MS";
                break;
        };
    }

    void GenerateLogic()
    {
        // Generate order of Syllogism randomly ex. AAE
        for (int i = 0; i < Logic.Length; i++)
        {
            // TODO add logic to only generate valid Syllogisms
            if(i == 1 && (Logic[0] == Syllogism.No || Logic[0] == Syllogism.Some_Not))
            {
                // if the first premise was already negative make sure second premise is positive
                Logic[i] = (Syllogism)Random.Range(0, 2);
                continue;
            }
            else if(i == Logic.Length-1)
            {
                if((Logic[0] == Syllogism.No || Logic[0] == Syllogism.Some_Not) || (Logic[1] == Syllogism.No || Logic[1] == Syllogism.Some_Not))
                {
                    // make sure if any premise was negative that the conclusion is negative
                    Logic[i] = (Syllogism)Random.Range(2, 4);
                    continue;
                }
            }

            Logic[i] = (Syllogism)Random.Range(0, 4);
        }
    }
}