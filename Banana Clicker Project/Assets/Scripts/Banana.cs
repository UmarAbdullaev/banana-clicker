using TMPro;
using UnityEngine;
using DG.Tweening;

public class Banana : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI autoText;
    public ParticleSystem particle;
    public TextEffect textEffect;
    public Canvas canvas;
    public AudioSource audioSource;

    [Space]
    public AudioClip[] clickSounds;

    private int score = 0;
    private int custom = 4;
    private int auto = 2;
    private float textEffectRadius = .75f;

    private void Start()
    {
        Load();

        scoreText.text = score.ToString();

        InvokeRepeating("AutoClick", 1f, 1f);

        autoText.text = $"{FormatNumber(auto)} P/S";
    }

    private void Load()
    {
        score = PlayerPrefs.GetInt("score", 0);
    }

    private void Save()
    {
        PlayerPrefs.SetInt("score", score);
    }

    private void OnMouseDown()
    {
        Click(custom);

        transform.DOKill();
        transform.localScale = Vector3.one;
        transform.DOPunchScale(Vector3.one * -.1f, .25f);

        particle.Play();

        audioSource.PlayOneShot(clickSounds[Random.Range(0, clickSounds.Length)]);
    }

    private void AutoClick()
    {
        Click(auto);
    }

    private void CreateTextEffect(string text, Vector2 position)
    {
        Instantiate(textEffect, position, textEffect.transform.rotation, canvas.transform).Set(text);
    }

    private void Click(int value)
    {
        score += value;

        Save();

        scoreText.text = FormatNumber(score);

        scoreText.transform.DOKill();
        scoreText.transform.localScale = Vector3.one;
        scoreText.transform.DOPunchScale(Vector3.one * .05f, .25f);

        Vector3 randomPosition = new Vector2(Random.Range(-textEffectRadius, textEffectRadius), Random.Range(-textEffectRadius, textEffectRadius));
        CreateTextEffect(value.ToString(), Camera.main.WorldToScreenPoint(transform.position + randomPosition));
    }

    public static string FormatNumber(int number)
    {
        if (number >= 1000000)
            return (number / 1000000f).ToString("0.##") + "M";
        else if (number >= 1000)
            return (number / 1000f).ToString("0.##") + "K";
        else
            return number.ToString();
    }
}