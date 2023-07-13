using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using static LocaleManager;

public class LocaleManager : MonoBehaviour {

    #region Singleton    
    public static LocaleManager Instance { get; private set; }
    private void Awake() {
        if (Instance == null) { 
            Instance = this; 
        } else { 
            Destroy(gameObject); 
        }
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    public enum LanguageType {
        English,
        Korean,
    }

    private bool active = false;
    
    public event Action<Sprite> OnCompleteGetSprite;
    public event Action<string> OnCompleteGetString;

    private void Start() {        
        // if playerprefs has a key
        if (PlayerPrefs.HasKey("LocaleKey")) {
            int localeKey = PlayerPrefs.GetInt("LocaleKey");
            ChangeLocale(localeKey);
        } 
        else {
            if (Application.systemLanguage == SystemLanguage.English) {    
                ChangeLocale((int)LanguageType.English);
            } else if (Application.systemLanguage == SystemLanguage.Korean) {                
                ChangeLocale((int)LanguageType.Korean);
            }
        }
    }
    public void ChangeLocale(int localeID) {

        if (active) { return; }
        StopAllCoroutines();
        StartCoroutine(SetLocale(localeID));
    }
    // localID : 0 - en , 1 - ko
    IEnumerator SetLocale(int localeID) {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        PlayerPrefs.SetInt("LocaleKey", localeID);
        active = false;
    }

    public void GetStringLocale(LocalizedString StringEntryRef) {

        Locale currentLocale = LocalizationSettings.SelectedLocale;

        var op = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(StringEntryRef.TableReference, StringEntryRef.TableEntryReference, currentLocale);

        if (op.IsDone) {
            Debug.Log(op.Result);
            OnCompleteGetString?.Invoke(op.Result);
        }
    }



    public void GetSpriteLocale(LocalizedSprite SpriteEntryRef) {

        Locale currentLocale = LocalizationSettings.SelectedLocale;

        var op = LocalizationSettings.AssetDatabase.GetLocalizedAssetAsync<Sprite>(SpriteEntryRef.TableReference, SpriteEntryRef.TableEntryReference, currentLocale);

        if (op.IsDone)
            Debug.Log(op.Result);
        else
            op.Completed += (op) =>
            {
                Debug.Log(op.Result);
                OnCompleteGetSprite?.Invoke(op.Result);
            };
    }

    public Locale GetSelectedLocale() {
        return LocalizationSettings.SelectedLocale;
    }
}