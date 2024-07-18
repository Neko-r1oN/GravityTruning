using UnityEngine;
using System.Collections;
public class CollectObject : MonoBehaviour
{
	public GameObject planet;   // ���͂̔������鐯
	public float coefficient;   // ���L���͌W��

	void FixedUpdate()
	{
		// ���Ɍ����������̎擾
		var direction = planet.transform.position - transform.position;
		// ���܂ł̋����̂Q����擾
		var distance = direction.magnitude;
		distance *= distance;
		// ���L���͌v�Z
		var gravity = coefficient * planet.GetComponent<Rigidbody2D>().mass * GetComponent<Rigidbody2D>().mass / distance;

		// �͂�^����
		GetComponent<Rigidbody2D>().AddForce(gravity * direction.normalized, ForceMode2D.Force);
	}
}