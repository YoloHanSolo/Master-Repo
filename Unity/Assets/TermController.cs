using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    string[][] dictionary;

    public int current_term_index = 0; 


    void Start()
    {
        term = GameObject.Find("Word").GetComponent<TextMeshProUGUI>();
        definition = GameObject.Find("Definition").GetComponent<TextMeshProUGUI>();

        jsonFile = Resources.Load<TextAsset>("dictionary");
        root = JsonUtility.FromJson<JsonRoot>(jsonFile.text);

        term.SetText(root.dictionary[current_term_index].term);
        definition.SetText(root.dictionary[current_term_index].definition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}