using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[System.Serializable]
public class DictionaryObject
{
    public string term;
    public string definition; 
}

[System.Serializable]
public class JsonRoot
{
    public DictionaryObject[] dictionary;
}

public class TermController : MonoBehaviour
{

    TextAsset jsonFile;
    JsonRoot root;

    TMP_Text term;
    TMP_Text definition;

    Button b_Next;
    Button b_Back;

    Animator m_Animator;

    public int current_term_index = 0; 


    void Start()
    {
        term = GameObject.Find("Word").GetComponent<TextMeshProUGUI>();
        definition = GameObject.Find("Definition").GetComponent<TextMeshProUGUI>();

        jsonFile = Resources.Load<TextAsset>("dictionary");
        root = JsonUtility.FromJson<JsonRoot>(jsonFile.text);

        term.SetText(root.dictionary[current_term_index].term);
        definition.SetText(root.dictionary[current_term_index].definition);

        b_Next = GameObject.Find("Button_Next").GetComponent<Button>();
        b_Next.onClick.AddListener(NextTerm);
        
        b_Back = GameObject.Find("Button_Back").GetComponent<Button>();
        b_Back.onClick.AddListener(PreviousTerm);

        m_Animator = GameObject.Find("SampleCharacter").transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    void Update()
    {
    }

    void NextTerm() {
        current_term_index = (current_term_index + 1) % root.dictionary.Length;
        term.SetText(root.dictionary[current_term_index].term);
        definition.SetText(root.dictionary[current_term_index].definition);
        m_Animator.SetInteger("AnimationCounter", current_term_index);
    }

    void PreviousTerm() {
        current_term_index = current_term_index - 1;
        if (current_term_index < 0) {
            current_term_index = root.dictionary.Length - 1;
        }     
        term.SetText(root.dictionary[current_term_index].term);
        definition.SetText(root.dictionary[current_term_index].definition);
        m_Animator.SetInteger("AnimationCounter", current_term_index);
    }

}