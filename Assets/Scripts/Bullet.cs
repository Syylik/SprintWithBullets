using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Vector3 moveDirection;
    [Range(0.1f, 50f)] public float timeToDestroy = 2f;
    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
    private void Update()
    {
        transform.Translate(moveSpeed * moveDirection * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent<Bot>(out Bot bot))
        {
            GameManager.SlowMoOff();
            bot.Die();
            DestroyBullet();
        }
        else if(collision.collider.TryGetComponent<FinishBlock>(out FinishBlock finishBlock))
        {
            GameManager.SlowMoOff();
            GameManager.instance.Win(finishBlock.multiplyNum);
            DestroyBullet();
        }
        else if(collision.collider.CompareTag("Box")) DestroyBullet(); //  :(
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}

[CustomEditor(typeof(Bullet))]
[CanEditMultipleObjects]
public class BulletEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var moveSpeed = serializedObject.FindProperty("moveSpeed");
        EditorGUILayout.PropertyField(moveSpeed, new GUIContent("Скорость"));
        var moveDir = serializedObject.FindProperty("moveDirection");
        EditorGUILayout.PropertyField(moveDir, new GUIContent("Направление"));
        EditorGUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        var destroyTime = serializedObject.FindProperty("timeToDestroy");
        EditorGUILayout.PropertyField(destroyTime, new GUIContent("время до уничтожения"));
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
}