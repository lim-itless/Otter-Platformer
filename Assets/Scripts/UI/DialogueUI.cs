using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : UIBase
{
    [SerializeField] private GameObject Layout_CharacterName;
    [SerializeField] private Text Text_Character;
    [SerializeField] private Text Text_Description;
    [SerializeField] private UIButton Button_Next;

    private string _currentDialogueId;
    private Queue<string> _descriptionQueue = new Queue<string>();

    private Coroutine _typingCoroutine;

    private void OnEnable()
    {
        Button_Next.BindOnClickButtonEvent(OnClick_Next);
    }

    public void OnClick_Next()
    {
        bool isNextDescriptionExist = CheckAndSetDescription();

        if (isNextDescriptionExist)
        {
            return;
        }

        bool isNextDialogueExist = CheckAndStartNextDialogue();
        if(isNextDialogueExist == false)
        {
            UIManager.Instance.CloseContentUI(UIType.DialogueUI);
        }
    }

    private bool CheckAndStartNextDialogue()
    {
        var dialogueData = GameDataManager.Instance.GetDialogueData(_currentDialogueId);
        if (dialogueData == null)
        {
            Debug.LogWarning($"다이얼로그 데이터가 존재하지 않습니다 {dialogueData}");
            return false;
        }

        string nextDialogueId = dialogueData.NextDialogueId;
        if (string.IsNullOrEmpty(nextDialogueId) == false)
        {
            StartDialogue(nextDialogueId);
            return true;
        }

        return false;
    }

    public void StartDialogue(string dialogueId)
    {
        _descriptionQueue.Clear();

        var dialogueData = GameDataManager.Instance.GetDialogueData(dialogueId);
        if (dialogueData == null)
        {
            Debug.LogWarning($"다이얼로그 데이터가 존재하지 않습니다 {dialogueData}");
            return;
        }

        _currentDialogueId = dialogueId;


        if (string.IsNullOrEmpty(dialogueData.Description))
        {
            return;
        }

        if (dialogueData.Description.Contains("<np>"))
        {
            string[] dialogueDescriptionList = dialogueData.Description.Split("<np>");
            foreach(string desc in dialogueDescriptionList)
            {
                _descriptionQueue.Enqueue(desc);
            }
            CheckAndSetDescription();
        }
        else
        {
            SetCurrentDialogueDescription(dialogueData.Description);
        }

        SetCharacterName(dialogueData.CharacterDataId);
    }

    private bool CheckAndSetDescription()
    {
        bool isNextDescriptionExsist = (_descriptionQueue.Count > 0);
        if (isNextDescriptionExsist)
        {
            string desc = _descriptionQueue.Dequeue();
            SetCurrentDialogueDescription(desc);
        }

        return isNextDescriptionExsist;
    }

    private void SetCharacterName(string characterDataId)
    {
        bool isActive = (string.IsNullOrEmpty(characterDataId) == false);
        Layout_CharacterName.SetActive(isActive);

        if (isActive)
        {
            var characterData = GameDataManager.Instance.GetCharacterData(characterDataId);
            if (characterData != null)
            {
                Text_Character.text = characterData.Name;
            }
        }
    }

    private void SetCurrentDialogueDescription(string description)
    {
        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);
        }

        _typingCoroutine = StartCoroutine(CoTypingText(description));
    }

    private IEnumerator CoTypingText(string description)
    {
        Text_Description.text = "";

        foreach (char c in description)
        {
            Text_Description.text += c;

            yield return new WaitForSeconds(0.03f);
        }

        _typingCoroutine = null;
    }
}
