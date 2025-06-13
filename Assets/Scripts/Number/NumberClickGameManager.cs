using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class NumberClickGameManager : MonoBehaviour
{
    [Header("Botones y UI")]
    public List<Button> numberButtons;
    public TextMeshProUGUI timerText;

    [Header("Configuración del juego")]
    public float timeLimit = 10f;

    private float timeRemaining;
    private int currentExpected = 1;
    private bool gameActive = false;

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (!gameActive) return;

        timeRemaining -= Time.deltaTime;
        timerText.text = "Tiempo: " + Mathf.CeilToInt(timeRemaining).ToString();

        if (timeRemaining <= 0f)
        {
            GameOver(false);
        }
    }

    public void StartGame()
    {
        currentExpected = 1;
        timeRemaining = timeLimit;
        gameActive = true;

        // Validar cantidad de botones
        if (numberButtons.Count != 10)
        {
            Debug.LogError($"Se esperaban 10 botones pero hay {numberButtons.Count}. Verifica la lista.");
            gameActive = false;
            return;
        }

        // Generar números del 1 al 10 y mezclarlos
        List<int> numbers = new List<int>();
        for (int i = 1; i <= 10; i++) numbers.Add(i);
        Shuffle(numbers);

        for (int i = 0; i < numberButtons.Count; i++)
        {
            int num = numbers[i];

            if (numberButtons[i] == null)
            {
                Debug.LogError($"El botón en la posición {i} es null.");
                continue;
            }

            Debug.Log($"Asignando número {num} al botón index {i}: {numberButtons[i].name}");

            TextMeshProUGUI txt = numberButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (txt == null)
            {
                Debug.LogError($"El botón '{numberButtons[i].name}' no tiene un TextMeshProUGUI como hijo.");
                continue;
            }

            txt.text = num.ToString();

            // Resetear estado del botón
            numberButtons[i].interactable = true;
            numberButtons[i].onClick.RemoveAllListeners();

            int capturedNum = num;
            Button btn = numberButtons[i];
            numberButtons[i].onClick.AddListener(() => OnNumberClicked(capturedNum, btn));
        }

        Debug.Log("Juego iniciado correctamente con 10 botones.");
    }


    void OnNumberClicked(int clickedNumber, Button btn)
    {
        if (!gameActive) return;

        if (clickedNumber == currentExpected)
        {
            btn.interactable = false;
            currentExpected++;

            if (currentExpected > 10)
            {
                GameOver(true);
            }
        }
        else
        {
            // Opcional: retroalimentación de error
        }
    }

    void GameOver(bool won)
    {
        gameActive = false;
        timerText.text = won ? "¡Ganaste!" : "¡Perdiste!";
        foreach (var btn in numberButtons)
        {
            btn.interactable = false;
        }
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
