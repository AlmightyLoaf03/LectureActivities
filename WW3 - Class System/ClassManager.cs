using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //Allows the use of LINQ ( .toList() )

public class ClassManager : MonoBehaviour
{
    public ClassNode rootClass;
    private List<ClassNode> allClasses = new(); // List to hold all the class nodes

    public Material lineMaterial;

    private void Start()
    {
        allClasses = FindObjectsOfType<ClassNode>(true).ToList(); // automatically finds and stores all ClassNode objects

        foreach (var node in allClasses)
        {
            node.Deactivate();
            node.button.onClick.AddListener(() => OnClassSelected(node));
        }

        rootClass.Activate();

        DrawClassConnections();
    }

    void DrawClassConnections()
    {
        foreach (var node in allClasses) // Loop through each class and draw lines to its next classes
        {
            foreach (var nextClass in node.nextClasses)
            {
                if (nextClass != null)  // Check for null before calling the method
                {
                    node.DrawConnectionLineTo(nextClass, lineMaterial);
                }
                else
                {
                    Debug.LogError("nextClass is null for node: " + node.className);
                }
            }
        }
    }

    void OnClassSelected(ClassNode selected) // Called when a class button is pressed
    {
        foreach (var node in allClasses)
        {
            node.Deactivate();
            node.HighligthConnections(false);
        }

        selected.OnSelected();
    }
}
