using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class CritElement
{
    public Element attackerElement;
    public Element attackedElement;
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "CritSprites", menuName = "CrushDatas/CritSprites")]
public class CritSprites : SerializedScriptableObject
{
    public List<CritElement> critElements;

    public Sprite GetCritSprite(Element attackerElement, Element attackedElement)
    {
       foreach(var critElement in critElements)
       {
           if(critElement.attackedElement == attackedElement && critElement.attackerElement == attackerElement)
           {
               return critElement.sprite;
           }
       }
       return null;
    }
}
