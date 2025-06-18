using UnityEngine;

public class CursorChange : MonoBehaviour
{
    [SerializeField] Texture2D cursor;
    [SerializeField] Texture2D enemyCusor;
    private Vector3 hotSpot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;

    Camera cam;
    private void Start()
    {
        cam = Camera.main;
        Cursor.SetCursor(cursor,hotSpot,cursorMode);
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.GetComponent<Enemy>() != null || hit.collider.GetComponent<BossAI>())
            {
                Cursor.SetCursor(enemyCusor,hotSpot,cursorMode);
                return;
            }
        }

        Cursor.SetCursor(cursor, hotSpot, cursorMode);
    }
}
