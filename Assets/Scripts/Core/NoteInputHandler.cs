using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NoteInputHandler : MonoBehaviour
{
    public KeyCode keyToPress; // ej: LeftArrow
    public string noteType;    // "Left", "Right", etc.
    public float perfectThreshold = 20f;
    public float goodThreshold = 50f;

    public RectTransform impactZone;

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            // Buscar todas las notas activas
            NoteSymbol[] allNotes = FindObjectsOfType<NoteSymbol>();

            foreach (NoteSymbol note in allNotes)
            {
                if (note == null || note.noteType != noteType) continue;

                RectTransform noteRect = note.GetComponent<RectTransform>();
                if (noteRect == null) continue; // seguridad adicional

                float distance = Mathf.Abs(noteRect.anchoredPosition.y - impactZone.anchoredPosition.y);

                if (distance <= perfectThreshold)
                {
                    Debug.Log("Perfect!");
                    RitualGameManager.Instance.RegisterHit("Perfect");
                    Destroy(note.gameObject);
                    return;
                }
                else if (distance <= goodThreshold)
                {
                    Debug.Log("Good!");
                    RitualGameManager.Instance.RegisterHit("Good");
                    Destroy(note.gameObject);
                    return;
                }
            }

            Debug.Log("Fail!");
            RitualGameManager.Instance.RegisterHit("Fail");
        }

    }
}
