using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
  public void OnPointerEnter(PointerEventData eventData)
  {
    DDOnLoad.SwitchSoundData.Play();
  }

  public void OnSelect(BaseEventData eventData)
  {
    DDOnLoad.ClickSoundData.Play();
  }
}
