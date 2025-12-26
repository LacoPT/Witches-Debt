using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
public class LocaleSelector : MonoBehaviour
{
    private bool isActive;
    private static int currentLocale;
    private static int localesCount;

    public void Awake()
    {
        localesCount = LocalizationSettings.AvailableLocales.Locales.Count;
        currentLocale = LocalizationSettings.SelectedLocale.GetEntityId();
    }
    
    public void SwitchLocale()
    {
        if (isActive)
            return;
        currentLocale += 1;
        if (currentLocale >= localesCount)
            currentLocale = 0;
        StartCoroutine(SetLocale(currentLocale));
    }
    
    public void ChangeLocale()
    {
        
    }
    IEnumerator SetLocale(int localeID)
    {
        isActive = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        isActive = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
