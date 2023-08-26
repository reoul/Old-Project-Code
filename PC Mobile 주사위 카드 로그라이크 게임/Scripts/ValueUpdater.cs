using UnityEngine;
using TMPro;

public class ValueUpdater : MonoBehaviour
{
    [SerializeField]
    private TMP_Text powText, piercingText, defText;

    struct ValStruct {
        public TMP_Text text;
        public float val;
        public float targetVal;

        public ValStruct(TMP_Text tmpText)
        {
            text = tmpText;
            val = 0;
            targetVal = val;
        }
    }

    ValStruct powVal, piercingVal, defVal;

    public enum valType{ pow, piercing, def }

    public void Init()
    {
        powVal = new ValStruct(powText);
        piercingVal = new ValStruct(piercingText);
        defVal = new ValStruct(defText);
    }

    private void Update()
    {
        valUpdate(ref powVal);
        valUpdate(ref piercingVal);
        valUpdate(ref defVal);
    }

    private void valUpdate(ref ValStruct valStruct)
    {
        valStruct.val = Mathf.Lerp(valStruct.val, valStruct.targetVal, 3 * Time.deltaTime);

        valStruct.text.text = Mathf.Round(valStruct.val).ToString();

        
        if (System.Convert.ToInt32(valStruct.text.text) > valStruct.targetVal)
        {
            valStruct.text.color = Color.red;
        }
        else if(System.Convert.ToInt32(valStruct.text.text) < valStruct.targetVal)
        {
            valStruct.text.color = Color.green;
        }
        else
        {
            valStruct.text.color = Color.white;
        }
    }


    public void AddVal(int val, valType type, bool isSoundOn = true)
    {
        switch (type)
        {
            case valType.pow:
                powVal.targetVal += val;
                break;
            case valType.piercing:
                piercingVal.targetVal += val;
                break;
            case valType.def:
                defVal.targetVal += val;
                break;
        }

        if (!isSoundOn)
        {
            return;
        }
        
        if(val > 0)
        {
            SoundManager.Instance.PlaySound("StatU");
        }
        else
        {
            SoundManager.Instance.PlaySound("StatD2");
        }
    }
}
