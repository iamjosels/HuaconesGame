using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualGameManager : MonoBehaviour
{
    public static RitualGameManager Instance;

    public float suspicionLevel = 0f;
    public float suspicionMax = 100f;
    public int consecutiveFails = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterHit(string accuracy)
    {
        Debug.Log("Hit: " + accuracy);

        if (accuracy == "Perfect")
        {
            suspicionLevel = Mathf.Max(0, suspicionLevel - 10f);
            consecutiveFails = 0;
        }
        else if (accuracy == "Good")
        {
            consecutiveFails = 0;
        }
        else if (accuracy == "Fail")
        {
            suspicionLevel += 15f;
            consecutiveFails++;
        }

        if (suspicionLevel >= suspicionMax || consecutiveFails >= 5)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("¡Ritual fallido! Los Huacones te descubrieron.");
        // Aquí puedes añadir animación o transición
    }
}
