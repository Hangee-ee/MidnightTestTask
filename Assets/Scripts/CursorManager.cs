using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private bool isCursorLocked = true;

    public PlayerHealth playerHealth;
    public WinManager winManager;

    void Start()
    {
        LockUnlockCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.I))
        {
            isCursorLocked = !isCursorLocked;
            LockUnlockCursor();
        }

        if (playerHealth.IsDead)
        {
            isCursorLocked = false;
            LockUnlockCursor();
        }

        if (winManager.GameWon)
        {
            isCursorLocked = false;
            LockUnlockCursor();
        }
    }

    public void LockUnlockCursor()
    {
        Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isCursorLocked;
    }
}
