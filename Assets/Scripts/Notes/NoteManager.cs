using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [Header("Prefab y referencias")]
    public GameObject notePrefab;
    public RectTransform spawnZone;
    public Transform noteParent;

    [Header("Targets visuales de generación")]
    public List<NoteSpawnTarget> spawnTargets;

    //[Header("Generación de notas")]
    //public float spawnInterval = 1.5f;
    //private float spawnTimer = 0f;

    //void Update()
    //{
    //    spawnTimer += Time.deltaTime;
    //    if (spawnTimer >= spawnInterval)
    //    {
    //        string[] directions = { "Left", "Down", "Up", "Right" };
    //        string randomDirection = directions[Random.Range(0, directions.Length)];

    //        SpawnNote(randomDirection);
    //        spawnTimer = 0f;
    //    }
    //}

    public void SpawnNote(string direction)
    {
        RectTransform target = GetTargetForDirection(direction);
        if (target == null)
        {
            Debug.LogWarning("No se encontró posición para: " + direction);
            return;
        }

        GameObject note = Instantiate(notePrefab, noteParent);
        var noteScript = note.GetComponent<NoteSymbol>();
        noteScript.noteType = direction;

        RectTransform rt = note.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(target.anchoredPosition.x, spawnZone.anchoredPosition.y);
    }

    private RectTransform GetTargetForDirection(string direction)
    {
        foreach (var target in spawnTargets)
        {
            if (target.noteType.Trim().ToLower() == direction.Trim().ToLower())
                return target.targetTransform;
        }
        return null;
    }
}
