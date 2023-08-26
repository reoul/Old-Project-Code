using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleAttach : Singleton<BattleAttach>
{
    [SerializeField] private TMP_Text _attachText;

    public readonly int[] AttachArray = {1, 2, 4};
    public int CurAttachIndex { get; private set; }

    public void ChangeAttach()
    {
        CurAttachIndex = ++CurAttachIndex % AttachArray.Length;
        Time.timeScale = AttachArray[CurAttachIndex];
        _attachText.text = $"x{AttachArray[CurAttachIndex].ToString()}";
    }
}
