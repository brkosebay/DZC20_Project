using UnityEngine;

public class GameControllerHanoiTower : MonoBehaviour
{
    private int moveCount = 0;
    private int minMovesToWin;
    public RodController targetRod; // Assign this in the Inspector

    void Start()
    {
        // Initialize minMovesToWin based on the number of disks
        minMovesToWin = CalculateMinMoves(3);
    }

    public void IncrementMoveCount()
    {
        moveCount++;
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (moveCount >= minMovesToWin && targetRod.disksOnRod.Count == 3)
        {
            Debug.Log("Win!");
            // Implement win logic
        }
    }

    private int CalculateMinMoves(int numberOfDisks)
    {
        return (int)Mathf.Pow(2, numberOfDisks) - 1;
    }
}
