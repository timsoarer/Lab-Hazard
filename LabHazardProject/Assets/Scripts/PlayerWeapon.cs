using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform gunPivot;
    [SerializeField]
    private Transform muzzlePoint;
    [SerializeField]
    private AudioClip shootSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 relativePos = cursorPos - (Vector2)gunPivot.position;
        float angle = Vector2.SignedAngle(Vector2.right, relativePos);
        gunPivot.rotation = Quaternion.Euler(0f, 0f, angle);

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, muzzlePoint.position, Quaternion.identity);
        Projectile projectileScript = projectileObject.GetComponent<Projectile>();
        if (projectileScript == null)
        {
            Debug.LogError(gameObject.name + " is trying to fire " + projectilePrefab.name + ", but it has no projectile script attached!");
        }
        projectileScript.SetTravelAngle(gunPivot.rotation.eulerAngles.z);
        projectileScript.ChangeProjectileSide(ProjectileSide.Player);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position + Vector3.forward*3);
    }
}
