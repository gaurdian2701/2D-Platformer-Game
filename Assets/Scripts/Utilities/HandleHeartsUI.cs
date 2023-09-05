using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleHeartsUI : MonoBehaviour
{
    [SerializeField] private List<Image> HeartUI;

    private void Start()
    {
        PlayerHealth.PlayerDamaged += DecreaseHearts;
    }

    private void OnDestroy()
    {
        PlayerHealth.PlayerDamaged -= DecreaseHearts;
    }
    private void DecreaseHearts(int health)
    {
        Image heart = HeartUI[health];
        heart.fillAmount = 0f;
    }
}
