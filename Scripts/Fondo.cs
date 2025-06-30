using UnityEngine;

public class Fondo : MonoBehaviour
{
public float scrollSpeed = 2f;

void Update() {
    transform.Translate(Vector2.down * scrollSpeed * Time.deltaTime);

    if (transform.position.y < -10) {
        transform.position += new Vector3(0, 20, 0); // según el tamaño del fondo
    }
}

}
