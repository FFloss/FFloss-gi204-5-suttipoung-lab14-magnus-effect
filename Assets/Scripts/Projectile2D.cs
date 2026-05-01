using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.InputSystem;

public class Projectile2D : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject target;
    [SerializeField] Rigidbody2D bulletPrefab;
    void Update()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();

        if (Mouse.current.leftButton.wasPressedThisFrame)
            {
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            Debug.DrawRay(start: ray.origin, dir: ray.direction * 5f, Color.red, duration: 5f);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, distance: Mathf.Infinity);
            if (hit.collider != null)
            {
                target.transform.position = new Vector2(hit.point.x, hit.point.y);
                Debug.Log($"Hit{hit.collider.gameObject.name}");

                Vector2 projectileVelocity = CalculateProjectileVelocity(shootPoint.position, hit.point, 1f);
                Rigidbody2D shootBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                shootBullet.linearVelocity = projectileVelocity;
            }
            Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
            {
                Vector2 distance = target - origin;

                float velocityX = distance.x / time;
                float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

                Vector2 projectileVelocity = new Vector2(velocityX, velocityY);

                return projectileVelocity;
            }
        }
    }
}
