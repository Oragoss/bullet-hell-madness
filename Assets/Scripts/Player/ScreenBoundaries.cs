using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class ScreenBoundaries : MonoBehaviour
    {
        [Tooltip("How far from the side edges of the screen you want the player to be.")]
        [SerializeField, Range(-5f, 5f)]
        float horizontalBoundary = 1;

        [Tooltip("How far from the top or bottom edge of the screen you want the player to be.")]
        [SerializeField, Range(-10f, 10f)]
        float verticalBoundary = 0.65f;

        float objectWidth;
        float objectHeight;
        Vector2 screenBounds;

        private void Start()
        {
            screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
            objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        }

        private void LateUpdate()
        {
            Vector3 viewPosition = transform.position;

            viewPosition.x = Mathf.Clamp(viewPosition.x, screenBounds.x * -1 - objectWidth + horizontalBoundary, screenBounds.x + objectWidth - horizontalBoundary);
            viewPosition.y = Mathf.Clamp(viewPosition.y, screenBounds.y * -1 - objectHeight + verticalBoundary, screenBounds.y + objectHeight - verticalBoundary);

            transform.position = viewPosition;
        }
    }
}