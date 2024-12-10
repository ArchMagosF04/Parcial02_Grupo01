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
    private Stack<string> inputs = new Stack<string>();
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
                Debug.Log("you can start writing.");
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
                    if (key == KeyCode.Space)
                    {
                        inputs.Push(" ");
                        UpdateDisplay();
                        break;
                    }

                    string keyPressed = key.ToString();
                    // Filtrar solo letras, n�meros y algunos caracteres �tiles.
                    if (IsValidKey(keyPressed))
                    {
                        inputs.Push(keyPressed);
                        UpdateDisplay();

                    }
                    break; // Salir del bucle tras detectar la primera tecla v�lida.
                }
            }
            //al precionar enter lo escrito es guardado en la hoja
            if (Input.GetKeyDown(KeyCode.Return))
            {
                sheetText.text += annotatorText.text + "\n";
                ClearQueue();
                Y_wasPressed = false;
                Debug.Log("Press Y to write.");
            }

            if (Input.GetKeyDown(KeyCode.Backspace) && inputs.Count > 0)
            {
                inputs.Pop();
                annotatorText.text = annotatorText.text.Remove(annotatorText.text.Length - 1);
            }
        }
    }

    private bool IsValidKey(string key)
    {
        // Filtrar teclas v�lidas (puedes expandir esta lista seg�n sea necesario).
        return key.Length == 1;
    }

    private void UpdateDisplay()
    {
        // Mostrar el contenido del stack en el texto.
        string temp = inputs.Peek();
        annotatorText.text += temp;
    }

    public void ClearQueue()
    {
        // Limpiar el stack y actualizar la visualizaci�n.
        inputs.Clear();
        annotatorText.text = "";
    }
}

