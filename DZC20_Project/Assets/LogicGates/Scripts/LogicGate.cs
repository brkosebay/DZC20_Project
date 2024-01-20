using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 
using System.Linq;


public class LogicGate : MonoBehaviour, IPointerClickHandler
{
    public enum GateType { Input, Output, AND, OR, NOT, XOR }
    public GateType type;
    public List<LogicGate> inputs; // The list of input gates connected to this gate
    public bool output; // The output of this gate after evaluation

    // References to the connection points for inputs and output
    public List<LogicGate> connectedGates;

    public bool Evaluate()
    {
        // Evaluate based on gate type
        switch (type) // Make sure to use 'type' instead of 'gateType'
        {
            case GateType.Input:
                // Directly use the assigned output value for input gates
                return output;
            case GateType.AND: // Use the correct enum value
                // Perform AND on all input gate outputs
                return inputs.All(input => input.Evaluate());
            case GateType.OR: // Use the correct enum value
                // Perform OR on all input gate outputs
                return inputs.Any(input => input.Evaluate());
            case GateType.NOT: // Use the correct enum value
                // NOT gate can only receive input from one gate
                return !inputs[0].Evaluate();
            case GateType.XOR: // Use the correct enum value
                // Perform XOR on the first two input gate outputs
                return inputs[0].Evaluate() ^ inputs[1].Evaluate();
            default:
                return false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Notify the GameManager that this gate or node has been clicked
        GameManager.Instance.GateClicked(this);
    }

    public void Connect(LogicGate targetGate)
    {
        // Your code to instantiate a wire and connect it between gates
        connectedGates.Add(targetGate);
    }
}

