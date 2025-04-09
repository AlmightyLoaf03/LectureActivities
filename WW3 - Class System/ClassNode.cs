using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ClassNode : MonoBehaviour
{
    public string className;

    public List<ClassNode> nextClasses; // List of possible classes the BASECLASS can evolve into

    public Button button; // Represents the class in the screen

    public LineRenderer[] connectionLines; // Visually connect the BASECLASS to it's evolutions

    public Canvas linesCanvas; // Reference to the world-space canvas for lines

    public virtual void Activate() //Enables the class button
    {
        button.interactable = true;
        button.image.color = Color.white; // Make the button appear normal (Not yet grayed out)
    }

    public virtual void Deactivate() //Disables the class button
    {
        button.interactable = false;
        button.image.color = Color.gray; // Make the button grayed out when pressed
    }

    public virtual void OnSelected()
    {
        HighligthConnections(true); //Lights up the connections with the evolutions
        foreach (var next in nextClasses) // Go through all connected next classes
        {
            next.Activate();
        }
    }

    public void HighligthConnections(bool active)
    {
        foreach (var line in connectionLines)   // Go through each line connected to this class
        {
            if (line != null)
                line.startColor = line.endColor = active ? Color.yellow : Color.gray; // Highlights the lines to yellow if active, else grayed out
            
        }
    }

    public void DrawConnectionLineTo(ClassNode target, Material lineMaterial)
    {
        // Create a new GameObject to hold the LineRenderer
        GameObject lineObj = new GameObject($"{className}_To_{target.className}");
        lineObj.transform.SetParent(linesCanvas.transform);  // Parent it under this node (optional)

        // Add LineRenderer to the new GameObject
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.material = lineMaterial;   // Use the provided material
        lr.widthMultiplier = 7f;       // Set the line width (larger is easier to see)
        lr.positionCount = 2;          // We have 2 positions (start and end)
        lr.useWorldSpace = true;       // Use world space for accurate placement

        // Set Sorting Layer and Order in Layer to ensure lines are drawn behind
        Renderer lineRenderer = lr.GetComponent<Renderer>();
        lineRenderer.sortingLayerName = "Background";  // Set the sorting layer to 'Background'
        lineRenderer.sortingOrder = -1;                // Set the order in layer to be behind the UI buttons

        // Get the world positions for both the start and end points of the line
        Vector3 start = transform.position;       // Position of this node
        Vector3 end = target.transform.position;  // Position of the target node

        // Debug: Log the start and end positions to verify
        Debug.Log($"Start: {start}, End: {end}");

        // Set the start and end positions for the LineRenderer
        lr.SetPosition(0, start);  // Set the start position of the line
        lr.SetPosition(1, end);    // Set the end position of the line
    }

}





