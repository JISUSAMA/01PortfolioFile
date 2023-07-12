using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageSelectorManager : MonoBehaviour
{
    public static LanguageSelectorManager instance { get; private set; }

    bool active = false;
    int languageID;

    public event Action<string> OnCompleteGetString;
    public event Action<Sprite> OnCompleteGetSprite;
    
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        DontDestroyOnLoad(this.gameObject);
    }


    void Start()
    {
        if (PlayerPrefs.GetString("Busan_Player_ID").Equals(""))
        {
            if (Application.systemLanguage.Equals(SystemLanguage.Korean))
                PlayerPrefs.SetString("Busan_Language", "KO");
            else if (Application.systemLanguage.Equals(SystemLanguage.English))
                PlayerPrefs.SetString("Busan_Language", "EN");
        }

        if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
            languageID = 1;
        else if (PlayerPrefs.GetString("Busan_Language").Equals("EN")) 
            languageID = 0;

        ChanageLanguage(languageID).Forget();
    }

    public async UniTask ChanageLanguage(int _languageID)
    {
        if (active.Equals(true))
            return;

        //StartCoroutine(SetLanguage(_languageID));
        await SetLanguage(_languageID);
    }

    async UniTask SetLanguage(int _languageID) {
        active = true;

        //yield return LocalizationSettings.InitializationOperation.WaitForCompletion();
        await LocalizationSettings.InitializationOperation;

        //LocalizationSettings.InitializeSynchronously = true;

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_languageID];

        if (_languageID.Equals(1))
            PlayerPrefs.SetString("Busan_Language", "KO");
        else if (_languageID.Equals(0))
            PlayerPrefs.SetString("Busan_Language", "EN");

        //Debug.Log("1");

        //LocalizationSettings.SelectedLocaleChanged += (Locale locale) => Debug.Log($"locale is {locale}");

        //Debug.Log("2");

        active = false;

        await UniTask.NextFrame();
    }

    //IEnumerator SetLanguage(int _languageID)
    //{
    //    active = true;

    //    yield return LocalizationSettings.InitializationOperation.WaitForCompletion();
    //    //LocalizationSettings.InitializationOperation.WaitForCompletion();

    //    //LocalizationSettings.InitializeSynchronously = true;

    //    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_languageID];

    //    if (_languageID.Equals(1))
    //        PlayerPrefs.SetString("Busan_Language", "KO");
    //    else if (_languageID.Equals(0))
    //        PlayerPrefs.SetString("Busan_Language", "EN");

    //    //Debug.Log("1");

    //    //LocalizationSettings.SelectedLocaleChanged += (Locale locale) => Debug.Log($"locale is {locale}");

    //    //Debug.Log("2");

    //    active = false;
    //}

    public async UniTaskVoid GetStringLocale(LocalizedString StringEntryRef) {

        Locale currentLocale = LocalizationSettings.SelectedLocale;

        var op = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(StringEntryRef.TableReference, StringEntryRef.TableEntryReference, currentLocale);

        await op;

        OnCompleteGetString?.Invoke(op.Result);

        //op.Completed += GetStringCompleted;
        //if (op.IsDone) {
        //    OnCompleteGetString?.Invoke(op.Result);
        //}
    }

    //private void GetStringCompleted(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<string> obj) {

    //    OnCompleteGetString?.Invoke(obj.Result);
    //    obj.Completed -= GetStringCompleted;
    //}

    public async UniTask<string> ImmediateGetStringLocale(LocalizedString StringEntryRef) {

        Locale currentLocale = LocalizationSettings.SelectedLocale;

        var op = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(StringEntryRef.TableReference, StringEntryRef.TableEntryReference, currentLocale);

        await op;

        return op.Result;

        //if (op.IsDone) {
        //    result = op.Result;
        //}
    }

    public async UniTaskVoid GetSpriteLocale(LocalizedSprite SpriteEntryRef) {

        Locale currentLocale = LocalizationSettings.SelectedLocale;

        var op = LocalizationSettings.AssetDatabase.GetLocalizedAssetAsync<Sprite>(SpriteEntryRef.TableReference, SpriteEntryRef.TableEntryReference, currentLocale);

        await op;
        //op.Completed += GetSprite_Completed;
        //if (op.IsDone) {
        //    Debug.Log(op.Result);
        //    OnCompleteGetSprite?.Invoke(op.Result);
        //}

        OnCompleteGetSprite?.Invoke(op.Result);
    }

    //private void GetSprite_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Sprite> obj) {
    //    OnCompleteGetSprite?.Invoke(obj.Result);
    //    obj.Completed -= GetSprite_Completed;
    //}

    public async UniTask<Sprite> ImmediateGetSpriteLocale(LocalizedSprite SpriteEntryRef) {

        Locale currentLocale = LocalizationSettings.SelectedLocale;

        var op = LocalizationSettings.AssetDatabase.GetLocalizedAssetAsync<Sprite>(SpriteEntryRef.TableReference, SpriteEntryRef.TableEntryReference, currentLocale);

        await op;

        return op.Result;
    }

    public int GetCurrentSelectedLanguage() {

        Locale currentLocale = LocalizationSettings.SelectedLocale;

        // 선택하기
        if (currentLocale.Identifier.Code == "en") {
            Debug.Log("Code EN");
            return (int)LanguageType.English;
        } else if (currentLocale.Identifier.Code == "ko") {
            Debug.Log("Code KO");
            return (int)LanguageType.Korean;
        } else {
            if (PlayerPrefs.GetString("Busan_Language") == "EN") {
                Debug.Log("EN");
                return (int)LanguageType.English;
            } else if (PlayerPrefs.GetString("Busan_Language") == "KO") {
                Debug.Log("KO");
                return (int)LanguageType.Korean;
            } else {
                Debug.Log("No language selected.");
                return -1;
            }
        }
    }

    public int GetAvailableLocalesCount() {
        return LocalizationSettings.AvailableLocales.Locales.Count;
    }

    public void SetLocalVariablesString(LocalizedString localizedString, Dictionary<string, string> localVariables) {
        localizedString.Clear();
        localizedString.Arguments = new object[]
        {
            localVariables
        };

        localizedString.RefreshString();
    }

    public void EndPanelClose()
    {
        Application.Quit();
    }
}
