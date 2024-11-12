using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TDAManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dynamicTextBox;
    [SerializeField] private TextMeshProUGUI staticTextBox;

    [SerializeField] private TMP_InputField dynamicAddField;
    [SerializeField] private TMP_InputField dynamicRemoveField;
    [SerializeField] private TMP_InputField dynamicContainsField;

    [SerializeField] private TMP_InputField staticAddField;
    [SerializeField] private TMP_InputField staticRemoveField;
    [SerializeField] private TMP_InputField staticContainsField;

    [SerializeField] private List<int> startingDynamicValues = new List<int>();
    [SerializeField] private List<int> startingStaticValues = new List<int>();

    private StaticSet<int> staticSet;
    private DynamicSet<int> dynamicSet;

    private void Awake()
    {
        dynamicSet = new DynamicSet<int>();
        staticSet = new StaticSet<int>(startingStaticValues.Count);

        InitializeValuesInSets(dynamicSet, startingDynamicValues);
        InitializeValuesInSets(staticSet, startingStaticValues);
    }

    private void Start()
    {
        dynamicAddField.onSubmit.AddListener(AddValuesDynamic);
        dynamicRemoveField.onSubmit.AddListener(RemoveValuesDynamic);
        dynamicContainsField.onSubmit.AddListener(ContainsValuesDynamic);

        staticAddField.onSubmit.AddListener(AddValuesStatic);
        staticRemoveField.onSubmit.AddListener(RemoveValuesStatic);
        staticContainsField.onSubmit.AddListener(ContainsValuesStatic);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ShowValues();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ShowCardinality();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckIfListEmpty();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            DynamicUnion();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            DynamicIntersection();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            DynamicDifference();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            StaticUnion();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            StaticIntersection();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            StaticDifference();
        }
    }

    private void InitializeValuesInSets(TDAGroup<int> set, List<int> list)
    {
        foreach (var value in list)
        {
            set.Add(value);
        }
    }

    public void ClearField(TMP_InputField field)
    {
        field.text = "";
    }

    public void AddValuesDynamic(string input)
    {
        int value = int.Parse(input);
        ClearField(dynamicAddField);

        if (dynamicSet.Add(value))
        {
            Debug.Log("Value added to dynamic set.");
        }
        else
        {
            Debug.Log("Couldn´t add value to dynamic set.");
        }

        ShowValues();
    }

    public void AddValuesStatic(string input)
    {
        int value = int.Parse(input);
        ClearField(staticAddField);

        if (staticSet.Add(value))
        {
            Debug.Log("Value added to static set.");
        }
        else
        {
            Debug.Log("Couldn´t add value to static set.");
        }

        ShowValues();
    }

    public void RemoveValuesDynamic(string input)
    {
        int value = int.Parse(dynamicRemoveField.text);
        ClearField(dynamicRemoveField);

        if (dynamicSet.Remove(value))
        {
            Debug.Log("Value removed from dynamic set.");
        }
        else
        {
            Debug.Log("Couldn´t remove value from dynamic set.");
        }

        ShowValues();
    }

    public void RemoveValuesStatic(string input)
    {
        int value = int.Parse(staticRemoveField.text);
        ClearField(staticRemoveField);

        if (staticSet.Remove(value))
        {
            Debug.Log("Value removed from static set.");
        }
        else
        {
            Debug.Log("Couldn´t remove value from static set.");
        }

        ShowValues();
    }

    public void ContainsValuesDynamic(string input)
    {
        int value = int.Parse(dynamicContainsField.text);
        ClearField(dynamicContainsField);

        dynamicTextBox.text = $"Does Dynamic set contain {value}: {dynamicSet.Contains(value)}";
    }

    public void ContainsValuesStatic(string input)
    {
        int value = int.Parse(staticContainsField.text);
        ClearField(staticContainsField);

        staticTextBox.text = $"Does Static set contain {value}: {staticSet.Contains(value)}";
    }

    public void ShowValues()
    {
        staticTextBox.text = staticSet.Show();
        dynamicTextBox.text = dynamicSet.Show();
    }

    public void ShowCardinality()
    {
        dynamicTextBox.text = "Dynamic Cardinality: " + dynamicSet.Cardinality();
        staticTextBox.text = "Static Cardinality: " + staticSet.Cardinality();
    }

    public void CheckIfListEmpty()
    {
        dynamicTextBox.text = "Is Dynamic set Empty: " + dynamicSet.IsEmpty();
        staticTextBox.text = "Is Static set Empty: " + staticSet.IsEmpty();
    }

    public void DynamicUnion()
    {
        dynamicSet = dynamicSet.Union(staticSet) as DynamicSet<int>;
    }

    public void DynamicIntersection()
    {
        dynamicSet = dynamicSet.Intersection(staticSet) as DynamicSet<int>;
    }

    public void DynamicDifference()
    {
        dynamicSet = dynamicSet.Difference(staticSet) as DynamicSet<int>;
    }

    public void StaticUnion()
    {
        staticSet = staticSet.Union(dynamicSet) as StaticSet<int>;
    }

    public void StaticIntersection()
    {
        staticSet = staticSet.Intersection(dynamicSet) as StaticSet<int>;
    }

    public void StaticDifference()
    {
        staticSet = staticSet.Difference(dynamicSet) as StaticSet<int>;
    }
}
