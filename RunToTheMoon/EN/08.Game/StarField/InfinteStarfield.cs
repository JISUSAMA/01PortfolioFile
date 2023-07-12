using UnityEngine;
using System.Collections;

public class InfinteStarfield : MonoBehaviour {

	private Transform tx;
	private ParticleSystem.Particle[] points;
	private ParticleSystem m_System;

	public int starsMax = 100;
	public float starSize = 1;
	public float starDistance = 10;
	public float starClipDistance = 1;
	private float starDistanceSqr;
	private float starClipDistanceSqr;
	[SerializeField] [Range(0f, 0.5f)] private float star_minSize;
	[SerializeField] [Range(0.5f, 1f)] private float star_maxSize;


	// Use this for initialization
	void Start () {

		if (m_System == null) m_System = GetComponent<ParticleSystem>();

		tx = transform;
		starDistanceSqr = starDistance * starDistance;
		starClipDistanceSqr = starClipDistance * starClipDistance;
	}


	private void CreateStars() {
		points = new ParticleSystem.Particle[starsMax];

		starSize = Random.Range(star_minSize, star_maxSize);

		for (int i = 0; i < starsMax; i++) {
			points[i].position = Random.insideUnitSphere * starDistance + tx.position;
			points[i].color = new Color(1,1,1, 1);
			points[i].size = starSize;
		}
	}


	// Update is called once per frame
	void Update () {
		if ( points == null ) CreateStars();

		for (int i = 0; i < starsMax; i++) {

			if ((points[i].position - tx.position).sqrMagnitude > starDistanceSqr) {
				points[i].position = Random.insideUnitSphere.normalized * starDistance + tx.position;
			}

			if ((points[i].position - tx.position).sqrMagnitude <= starClipDistanceSqr) {
				float percent = (points[i].position - tx.position).sqrMagnitude / starClipDistanceSqr;
				points[i].color = new Color(1,1,1, percent);
				points[i].size = percent * starSize;
			}

		}

		// older versions of Unity
		// particleSystem.SetParticles ( points, points.Length );
		// Unity 5.4+ and probably some sooner versions
		m_System.SetParticles(points, points.Length);
	}
}