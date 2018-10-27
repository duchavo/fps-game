using UnityEngine;
using System.Collections;

/* Uses raycasting to calculate where a bullet fired from the center of the camera position
 * would stop when it hit an object.
 * SphereIndicator spawns a bullet hole at said position, which destroys itself after a few seconds.
 */

public class RayShooter : MonoBehaviour
{
    private Camera _camera;
    GameObject bullet_prefab;


    void Start()
    {
        _camera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.root.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null)
                {
                    target.ReactToHit();
                }
                
                else
                {
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = Instantiate(bullet_prefab, pos, Quaternion.identity);

        yield return new WaitForSeconds(2);

        Destroy(sphere);
    }

    public void setBulletHole(GameObject bulletHole)
    {
        bullet_prefab = bulletHole;
    }
}