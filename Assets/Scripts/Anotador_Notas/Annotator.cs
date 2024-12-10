using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class Annotator : MonoBehaviour
{
    private TextMeshProUGUI annotatorText;
    [SerializeField]private TextMeshProUGUI sheetText;

    private Queue<string> inputsWriting = new Queue<string>();
    private List<string> inputs = new List<string>();
   
    [SerializeField] private RectTransform writePointePosition;
    private bool Y_wasPressed = false;
    private float delay = 0.2f;

    void Start()
    {
        annotatorText = GetComponentInChildren<TextMeshProUGUI>();
    }
    void Update()
    {
        StartWriting();
        WritingInputs();
    }

    void StartWriting()
    {
        if (!Y_wasPressed)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                StartCoroutine(EnableWriting());
                Debug.Log("you can start writing");
            }
        }
    }
    IEnumerator EnableWriting()
    {
        ClearQueue();
        yield return new WaitForSeconds(delay);
        Y_wasPressed = true;
    }
    void WritingInputs()
    {
        if (Y_wasPressed == true)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    string keyPressed = key.ToString();
                    // Filtrar solo letras, números y algunos caracteres útiles.
                    if (IsValidKey(keyPressed))
                    {
                        inputsWriting.Enqueue(keyPressed);
                        UpdateDisplayWriting();
                    }
                    break; // Salir del bucle tras detectar la primera tecla válida.
                }
            }
            //al precionar enter lo escrito es guardado en la hoja
            if (Input.GetKeyDown(KeyCode.Return))
            {
                sheetText.text += annotatorText.text;
                ClearQueue();
            }

            if (Input.GetKeyDown(KeyCode.Backspace) && inputs.Count > 0)
            {
                inputs.RemoveAt(inputs.Count - 1);
                ClearQueue();
            }
        }
    }

    private bool IsValidKey(string key)
    {
        // Filtrar teclas válidas (puedes expandir esta lista según sea necesario).
        return key.Length == 1;
    }

    private void UpdateDisplayWriting()
    {
        annotatorText.text = ""; //primero se limpia lo q esta mostrado en pantalla para imprimierolo de 0
        foreach (var item in inputsWriting)//recorrer la queue de inputs los agrega a una lista y a un stack. 
        {
            inputs.Add(item);
        }
        inputsWriting.Clear();//siempre se limpia la queue para evitar que se agren inputs viejos
        for (int i = 0; i < inputs.Count; i++)//muesta en pantalla la lista de los caracteres.
        {
            annotatorText.text += inputs[i];
        }
    }

    public void ClearQueue()
    {
        inputsWriting.Clear();
        UpdateDisplayWriting();
    }
}

