using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    // Start is called before the first frame update
    
    /*private void GravaJson(string fileNameLocation, Cenario _argCenario)
    {
        string jsonString = JsonUtility.ToJson(_argCenario);

        if (File.Exists(fileNameLocation))
        {
            File.Delete(fileNameLocation);
        }
        File.WriteAllText(fileNameLocation, jsonString);

    }

    private Cenario LoadGameData(string fileNameLocation)
    {
        Cenario _cenario;
        if (File.Exists(fileNameLocation))
        {
            string dataAsJson = File.ReadAllText(fileNameLocation);
            _cenario = JsonUtility.FromJson<Cenario>(dataAsJson);
        }
        else
        {
            _cenario = new Cenario();
        }
        return _cenario;
    }*/
}

public class TextData
{
    public string[] DialogueText;
    public string[] TalkingEntity;
    public int[] whenItsTalking;
}
public class DialogEntry
{
    public List<TextData> loopDialog = new List<TextData>();
}
