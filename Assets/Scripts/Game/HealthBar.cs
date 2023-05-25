using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used for the bar attached to the enemies.
/// </summary>
public class HealthBar : MonoBehaviour
{
    /// <summary>
    /// The slider component representing the health bar.
    /// </summary>
    public Slider slider;

    /// <summary>
    /// The gradient used to determine the fill color of the health bar.
    /// </summary>
    public Gradient gradient;

    /// <summary>
    /// The image component representing the fill of the health bar.
    /// </summary>
    public Image fill;

    /// <summary>
    /// Sets the maximum health value and initializes the health bar.
    /// </summary>
    /// <param name="health">The maximum health value.</param>
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    /// <summary>
    /// Sets the current health value and updates the health bar.
    /// </summary>
    /// <param name="health">The current health value.</param>
    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
