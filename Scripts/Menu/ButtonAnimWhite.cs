using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimWhite : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IPointerExitHandler
{
  public GameObject obj;
  int c=0;
  public float y=8f;
  public void OnPointerEnter(PointerEventData eventData)
  {
    CancelInvoke("UnVisibility");
    InvokeRepeating("Visibility",0f, 0.01f);
  }

  void Visibility()
  {
    if (c<200) 
    {
      c+=3;
      obj.transform.localScale=new Vector3(c,y,0f);
    }
  }

  public void UnVisibility() 
  {
    if (c>0) 
    {
      c-=5;
      obj.transform.localScale=new Vector3(c,y,0f);
    } else {
      c=0;
      obj.transform.localScale=new Vector3(c,y,0f);
    }
    
  }
  public void OnSelect(BaseEventData eventData)
  {
      CancelInvoke("Visibility");
      CancelInvoke("UnVisibility");
      c=0;
      obj.transform.localScale=new Vector3(c,y,0f);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    CancelInvoke("Visibility");
    InvokeRepeating("UnVisibility",0f, 0.01f);
  }
}
