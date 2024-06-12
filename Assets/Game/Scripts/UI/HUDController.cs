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

        hudMenu = GetComponent<HUDMenu>();

        if (GameHandler.Instance.playerRef != null)
            GameHandler.Instance.playerRef.OnHealthChanged.AddListener(UpdateHealthbar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHealthbar()
    {
        healthBar.value = GameHandler.Instance.playerRef.GetHealth();
        hudMenu.ShowHUD();
        Invoke(nameof(hudMenu.HideHUD), 5f);
    }

    void OnDisable()
    {
        if (GameHandler.Instance.playerRef != null)
            GameHandler.Instance.playerRef.OnHealthChanged.RemoveListener(UpdateHealthbar);
    }
}
