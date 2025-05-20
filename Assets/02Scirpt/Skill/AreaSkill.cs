using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AreaSkill", menuName = "Scriptable Objects/AreaSkill")]
public class AreaSkill : SkillBase
{
    public float radius = 3f;
    public LayerMask enemyLayer;

    private float lineDuration = 1f;

    private LineRenderer lineRenderer;

    [SerializeField] GameObject HitAreaPre;
    public void CastatPosition(GameObject caster, Vector3 position)
    {
        

        Collider[] hits = Physics.OverlapSphere(position, radius, enemyLayer);

        GameObject proj = Instantiate(HitAreaPre, position, Quaternion.identity);

        Destroy(proj,1f);

        foreach (var hit in hits)
        {
            Targetable t = hit.GetComponent<Targetable>();
            if(t != null)
            {
                Debug.Log("R 스킬 적중");
            }
        }

        
    }

    //private void DrawRange(Vector3 position)
    //{
    //    if(lineRenderer == null)
    //    {
    //        GameObject lineObject = new GameObject("LineRenderer");
    //        lineRenderer.transform.position = position;
    //        lineRenderer = lineObject.AddComponent<LineRenderer>(); 

    //    }
    //    lineRenderer.positionCount = 50;
    //    lineRenderer.startWidth = 0.1f;
    //    lineRenderer.endWidth = 0.1f;
    //    lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    //    lineRenderer.startColor = Color.red;
    //    lineRenderer.endColor = Color.red;
    //    lineRenderer.enabled = false;

    //    lineRenderer.enabled = true;

    //    for(int i = 0; i < lineRenderer.positionCount; i++)
    //    {
    //        float angle = i * Mathf.PI * 2 / lineRenderer.positionCount;
    //        Vector3 point = new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
    //        lineRenderer.SetPosition(i, position + point);
    //    }
    //}

    //public IEnumerator HideRange(GameObject caster, Vector3 position)
    //{
    //    yield return new WaitForSeconds(lineDuration);
    //    lineRenderer.enabled = false;

    //    if(lineRenderer != null)
    //    {
    //        GameObject.Destroy(lineRenderer.gameObject);
    //    }
    //}
    public override void Cast(GameObject caster, Targetable target)
    {
        
    }
}
