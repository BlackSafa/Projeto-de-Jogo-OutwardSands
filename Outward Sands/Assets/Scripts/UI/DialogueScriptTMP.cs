using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class DialogueScriptTMP : MonoBehaviour
{
    private TMP_Text _textComponent;
    public string[] DialogueStrings;
    public float SecondsBetweenCharacters = 0.15f;
    public float CharacterRateMultiplier = 0.5f;
    public KeyCode DialogueInput = KeyCode.Return, JumpDialogueInput = KeyCode.End;
    private bool _isStringBeingRevealed = false;
    public bool _isDialoguePlaying {get; private set;} = false;
    private bool _isEndOfDialogue = false;
    private bool _isJumpingText = false;
    public bool autoStart = false;
    public GameObject ContinueIcon;
    public GameObject StopIcon;
    public delegate void OnDialogueEvent();
    public static event OnDialogueEvent endOfDialogue;
	
    void Start ()
	{
	    _textComponent = GetComponent<TMP_Text>();
	    _textComponent.text = "";
        _textComponent.lineSpacing = 10;
        _textComponent.lineSpacingAdjustment = 10;
        //DialogueStrings = RequestText()
        HideIcons();
	}
	
    void Update () 
	{
	    if (Input.GetKeyDown(KeyCode.Return)|| autoStart)
	    {
	        if (!_isDialoguePlaying)
	        {
                autoStart = false;
                _isDialoguePlaying = true;
                StartCoroutine(StartDialogue());
            }
	    }
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJumpingText = true;
            SecondsBetweenCharacters *= 0.1f;
        }*/
	}

    public void CallDialog(int indiceDoRoteiro)
    {}

    private IEnumerator StartDialogue()
    {
        int dialogueLength = DialogueStrings.Length;
        int currentDialogueIndex = 0;

        while (currentDialogueIndex < dialogueLength || !_isStringBeingRevealed)
        {
            if (!_isStringBeingRevealed)
            {
                _isStringBeingRevealed = true;

                StartCoroutine(DisplayString(DialogueStrings[currentDialogueIndex++]));

                
            }

            if (currentDialogueIndex >= dialogueLength)
                {
                    _isEndOfDialogue = true;
                }

            yield return 0;
        }

        while (true)
        {
            if (Input.GetKeyDown(DialogueInput))
            {
                break;
            }

            yield return 0;
        }

        HideIcons();
        _isEndOfDialogue = false;
        _isDialoguePlaying = false;
        SecondsBetweenCharacters = 0.15f;
    }

    private IEnumerator DisplayString(string stringToDisplay)
    {
        WaitForSecondsRT wait = new WaitForSecondsRT(1);
        int stringLength = stringToDisplay.Length;
        int currentCharacterIndex = 0;

        HideIcons();

        _textComponent.text = "";

        while (currentCharacterIndex < stringLength)
        {
            _textComponent.text += stringToDisplay[currentCharacterIndex];
            currentCharacterIndex++;

            if (currentCharacterIndex < stringLength)
            {
                if (Input.GetKey(DialogueInput))
                {
                    yield return wait.NewTime(SecondsBetweenCharacters*CharacterRateMultiplier);
                }
                else
                {
                    yield return wait.NewTime(SecondsBetweenCharacters);
                }
            }
            else
            {
                break;
            }
        }

        ShowIcon();

        while (true)
        {
            if (Input.GetKeyDown(DialogueInput) || _isJumpingText)
            {
                break;
            }

            yield return 0;
        }

        HideIcons();

        _isStringBeingRevealed = false;
        _textComponent.text = "";
    }

    private void HideIcons()
    {
        ContinueIcon.SetActive(false);
        StopIcon.SetActive(false);
    }

    private void ShowIcon()
    {
        if (_isEndOfDialogue)
        {
            StopIcon.SetActive(true);
            _isJumpingText = false;
            if (endOfDialogue != null)
            {
                endOfDialogue();
            }
            return;
        }

        ContinueIcon.SetActive(true);
    }
}

/* Texto de teste da caixa de dialogo
	Corpo: Testando o corpo do texto vamos ver como esse texto consegue segurar com a questao de distancia ou qualquer outra coisa assim, ja que quero ver esteticamente como tudo pode se posicionar em uma caixa apropriada para a disposiçao no jogo, ja que tudo pode ser ume tanto inadequado quando nao testado de forma apropriada, como tamanho, espaçamento, paragrafaçao, recuo, etc, etc. entao eu preciso garantir a organiaçao disso no projeto. Entretanto ainda preciso descobrir como fazer um texto manter seu tamanho em disversas formas do Aspect Ratio, ja que isso vai dar problema futuramente conforme formos dispor nosso jogo
*/