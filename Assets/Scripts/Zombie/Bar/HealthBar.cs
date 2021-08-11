using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image _bar;
    [SerializeField] private TextMeshProUGUI _text;

    private int _maxHealth;
    
    public void Initialize(int maxHealth)
    {
        gameObject.SetActive(true);
        _maxHealth = maxHealth;
        _bar.color = _gradient.Evaluate(1);
    }

    public void UpdateValue(int health)
    {
        _text.text = health.ToString();
        _bar.fillAmount = (float) health / _maxHealth;
        _bar.color = _gradient.Evaluate((float) health / _maxHealth);
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}
