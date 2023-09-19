using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnim : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IPointerExitHandler
{
  GameObject obj;
  int c=0;

  void Start() 
  {
    obj=gameObject.transform.Find("BLACK").gameObject;
  }

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
      obj.transform.localScale=new Vector3(c,10f,0f);
    } else {
      CancelInvoke("Visibility");
    }
  }

  public void UnVisibility() 
  {
    if (c>0) 
    {
      c-=5;
      obj.transform.localScale=new Vector3(c,10f,0f);
    } else {
      c=0;
      obj.transform.localScale=new Vector3(c,10f,0f);
      CancelInvoke("UnVisibility");
    }
    
  }
  public void OnSelect(BaseEventData eventData)
  {
      CancelInvoke("Visibility");
      CancelInvoke("UnVisibility");
      c=0;
      obj.transform.localScale=new Vector3(c,10f,0f);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    CancelInvoke("Visibility");
    InvokeRepeating("UnVisibility",0f, 0.01f);
  }
}
