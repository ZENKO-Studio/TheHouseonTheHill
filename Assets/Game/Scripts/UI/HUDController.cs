using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] Slider healthBar;

    private HUDMenu hudMenu;

    // Start is called before the first frame update
    void OnEnable()
    {
<<<<<<< Updated upstream
        if (GameManager.Instance.playerRef != null)
            GameManager.Instance.playerRef.OnHealthChanged.AddListener(UpdateHealthbar);
=======
        hudMenu = GetComponent<HUDMenu>();

        if (GameHandler.Instance.playerRef != null)
            GameHandler.Instance.playerRef.OnHealthChanged.AddListener(UpdateHealthbar);
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHealthbar()
    {
<<<<<<< Updated upstream
        healthBar.value = GameManager.Instance.playerRef.GetHealth();
=======
        healthBar.value = GameHandler.Instance.playerRef.GetHealth();
        hudMenu.ShowHUD();
        Invoke(nameof(hudMenu.HideHUD), 5f);
>>>>>>> Stashed changes
    }

    void OnDisable()
    {
        if (GameManager.Instance.playerRef != null)
            GameManager.Instance.playerRef.OnHealthChanged.RemoveListener(UpdateHealthbar);
    }
}
