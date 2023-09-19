using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
[RequireComponent(typeof(Slider))]
public class SaveSlider2 : MonoBehaviour
{
    private Slider ss2;
    const string PrefName = "optionvalueS2";
    void Awake()
    {
        ss2 = GetComponent<Slider>();
        ss2.onValueChanged.AddListener(new UnityAction<float>(index =>
        {
            PlayerPrefs.SetFloat(PrefName, ss2.value);
            PlayerPrefs.Save();
        }));
        ss2.value = PlayerPrefs.GetFloat(PrefName, 0);
    }

}
