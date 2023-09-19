using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
[RequireComponent(typeof(Slider))]
public class SaveSlider : MonoBehaviour
{
    private Slider ss;
    const string PrefName = "optionvalueS";
    void Awake()
    {
        ss = GetComponent<Slider>();
        ss.onValueChanged.AddListener(new UnityAction<float>(index =>
        {
            PlayerPrefs.SetFloat(PrefName, ss.value);
            PlayerPrefs.Save();
        }));
        ss.value = PlayerPrefs.GetFloat(PrefName, 0);
    }

}
