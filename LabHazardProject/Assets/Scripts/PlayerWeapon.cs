using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(RandomSoundPlayer))]
public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private Transform gunPivot; // The parent of the gun object with which the gun rotates
    [SerializeField]
    private Transform muzzlePoint; // The point where projectiles spawn
    [SerializeField]
    private AudioClip[] shootSounds;
    private RandomSoundPlayer randSoundPlayer;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        randSoundPlayer = GetComponent<RandomSoundPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotates the weapon's pivot toward the mouse cursor.
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 relativePos = cursorPos - (Vector2)gunPivot.position;
        float angle = Vector2.SignedAngle(Vector2.right, relativePos);
        gunPivot.rotation = Quaternion.Euler(0f, 0f, angle);

        if (Input.GetMouseButton(0) && timer >= fireRate)
        {
            Shoot();
            timer = 0f;
        }
        timer += Time.deltaTime;
    }

    // Creates a projectline "projectilePrefab", sets it to player's side, and fires it in the direction of the weapon
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
        randSoundPlayer.PlayRandomSound(shootSounds);
    }
}
