using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionSpawner : MonoBehaviour
{
    [SerializeField] private king king;
    [SerializeField] private Scroll scroll;
    [SerializeField] private GameObject instructionPrefab;
    [SerializeField] private int difficultyCap = 10;
    private int difficulty = 1;

    private void Awake() {
        SpawnInstruction();
    }

    private void Start() {
        king.GetEvent().AddListener(SpawnInstruction);
    }

    private void SpawnInstruction() {
        float paperDifficulty = (float)difficulty / difficultyCap;

        Instructions instantiatedInstruction = Instantiate(instructionPrefab, transform).GetComponent<Instructions>();
        instantiatedInstruction.SetPaper(king, paperDifficulty, scroll);

        difficulty++;
    }
}
