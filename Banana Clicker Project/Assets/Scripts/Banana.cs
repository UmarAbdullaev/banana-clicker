using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Banana : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [Space]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI autoText;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private TextEffect textEffect;
    [SerializeField] private Canvas canvas;
    [SerializeField] private AudioSource audioSource;

    [Space]
    [SerializeField] private AudioClip[] clickSounds;

    private int score = 0;
    private int clickScore = 4;
    private int auto = 2;
    private float textEffectRadius = .75f;

    private void Start()
    {
        Load();

        StartCoroutine(AutoClickCoroutine());
    }

    public void Load()
    {
        SaveSystem.SettingsData loadData = SaveSystem.Instance.Load();

        score = loadData.score;
        clickScore = loadData.clickScore;
        auto = loadData.auto;

        autoText.text = $"{FormatNumber(auto)} P/S";
        scoreText.text = FormatNumber(score);
    }

    public void Save()
    {
        SaveSystem.SettingsData currentData = new SaveSystem.SettingsData
        {
            score = this.score,
            clickScore = this.clickScore,
            auto = this.auto
        };

        SaveSystem.Instance.Save(currentData);
    }

    private void OnMouseDown()
    {
        Click(clickScore);

        transform.DOKill();
        transform.localScale = Vector3.one;
        transform.DOPunchScale(Vector3.one * -.1f, .25f);

        particle.Play();

        audioSource.PlayOneShot(clickSounds[Random.Range(0, clickSounds.Length)], 1f);
    }

    private IEnumerator AutoClickCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            Click(auto);
        }
    }

    private void Click(int value)
    {
        score += value;

        // Save();

        scoreText.text = FormatNumber(score);

        scoreText.transform.DOKill();
        scoreText.transform.localScale = Vector3.one;
        scoreText.transform.DOPunchScale(Vector3.one * .05f, .25f);

        Vector3 randomPosition = new Vector2(Random.Range(-textEffectRadius, textEffectRadius), Random.Range(-textEffectRadius, textEffectRadius));
        CreateTextEffect(value.ToString(), Camera.main.WorldToScreenPoint(transform.position + randomPosition));
    }

    private void CreateTextEffect(string text, Vector2 position)
    {
        Instantiate(textEffect, position, textEffect.transform.rotation, canvas.transform).Set(text);
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