using UnityEngine;
using UnityEngine.UI;
using YG;
using System.Collections.Generic;

/// <summary>
/// TextTranslator sınıfı, farklı dillerde çevirileri destekler ve kullanıcı diline göre uygun metni gösterir.
/// </summary>
public class TextTranslator : MonoBehaviour
{
    [SerializeField] private string _defaultText;
    [SerializeField] private List<LanguageText> _translations;

    private Dictionary<string, string> _translationDictionary;

    void Start()
    {
        InitializeTranslations();
        UpdateText();
    }

    private void InitializeTranslations()
    {
        _translationDictionary = new Dictionary<string, string>();

        foreach (var translation in _translations)
        {
            _translationDictionary[translation.LanguageCode] = translation.Text;
        }
    }

    private void UpdateText()
    {
        var textComponent = GetComponent<Text>();
        textComponent.text = GetText();
    }

    public string GetText()
    {
        return _translationDictionary.TryGetValue(YandexGame.lang, out string translatedText)
            ? translatedText
            : _defaultText;
    }
}

[System.Serializable]
public class LanguageText
{
    public string LanguageCode;
    public string Text;
}
