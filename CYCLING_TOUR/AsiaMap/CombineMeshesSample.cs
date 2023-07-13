using UnityEngine;

public class CombineMeshesSample : MonoBehaviour {
	[SerializeField] MeshFilter[] meshFilters;
CombineInstance[] combine;
	void Start () {
			
		/*MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
			MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
			if (CheckSameMaterial (meshRenderers) == true) {
				CombineInstance[] combine = new CombineInstance[meshFilters.Length];
				int i = 0;
				while (i < meshFilters.Length) {
					combine[i].mesh = meshFilters [i].sharedMesh;
					combine[i].transform = meshFilters [i].transform.localToWorldMatrix;
					meshFilters[i].gameObject.SetActive (false);
					i++;
				}
				MeshFilter meshfilter 
					= gameObject.AddComponent<MeshFilter> () as MeshFilter;
				MeshRenderer meshrenderer 
					= gameObject.AddComponent<MeshRenderer> () as MeshRenderer;
				meshrenderer.sharedMaterial = meshRenderers [0].sharedMaterial;
				meshfilter.mesh = new Mesh ();
				meshfilter.mesh.CombineMeshes (combine);
				transform.gameObject.SetActive (true);	
			}*/
			meshFilters = GetComponentsInChildren<MeshFilter>();
			combine = new CombineInstance[meshFilters.Length];

			int i = 0;
			while (i < meshFilters.Length)
			{
				combine[i].mesh = meshFilters[i].sharedMesh;
				combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
				meshFilters[i].gameObject.SetActive(false);

				i++;
			}
			transform.GetComponent<MeshFilter>().mesh = new Mesh();
			transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
			transform.gameObject.SetActive(true);
	//	Combine();
	}
	/*public void Merge()
	{
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		meshFilter.mesh.Clear();
		MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>(true);
		transform.GetComponent<MeshRenderer>().material = meshFilters[0].renderer.sharedMaterial;
		CombineInstance[] combine = new CombineInstance[meshFilters.Length - 1];
		int i = 0;

		int ci = 0;

		while (i < meshFilters.Length)
		{

			if (meshFilter != meshFilters[i])

			{

				combine[ci].mesh = meshFilters[i].sharedMesh;

				combine[ci].transform = meshFilters[i].transform.localToWorldMatrix;

				++ci;

			}

			meshFilters[i].gameObject.active = false;//.renderer.enabled = false;

			i++;

		}

		meshFilter.mesh.CombineMeshes(combine);

		transform.gameObject.active = true;



		transform.gameObject.GetComponent<MeshCollider>().sharedMesh =

		transform.gameObject.GetComponent<MeshFilter>().mesh;

	}*/
	bool CheckSameMaterial(MeshRenderer[] meshRenderers) {
		Material mtrl = meshRenderers[0].sharedMaterial;
		int i = 0;
		for( i=1; i<meshRenderers.Length; i++) {
			if (mtrl != meshRenderers [i].sharedMaterial)
				return false;
		}
		return true;
	}
	#region --- helper ---
	private enum enumMouseButton
	{
		left = 0,
		right = 1,
		middle = 2,
	}
	#endregion

	//public GameObject prefabShape = null;
	//public Camera cam = null;

	private void Update()
	{
	/*	//mouse left button = add block
		if (Input.GetMouseButtonDown((int)enumMouseButton.left) == true)
		{
			// 1. ray(line) from camera into scene
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100f) == true)
			{
				// 2. ray hits mesh side (use normal, get position on side where hit)
				Vector3 newPos = hit.point + (hit.normal * 0.5f);

				// 3. round position to middle of 1 unit space
				newPos.x = (float)System.Math.Round(newPos.x, System.MidpointRounding.AwayFromZero);
				newPos.y = (float)System.Math.Round(newPos.y, System.MidpointRounding.AwayFromZero);
				newPos.z = (float)System.Math.Round(newPos.z, System.MidpointRounding.AwayFromZero);

				// 4. instantiate a new shape, make child
				GameObject shape = (GameObject)Instantiate(prefabShape, newPos, Quaternion.identity);
				shape.transform.parent = this.transform;

				// 5. combine mesh and children into one (to keep Batches count low)
				Combine();
			}
		}*/
	}

	private void Combine()
	{
		// 1. destroy existing meshcollider
		Destroy(this.gameObject.GetComponent<MeshCollider>());

		// 2. mesh and child meshes into array
		MeshFilter[] meshfilters = this.GetComponentsInChildren<MeshFilter>();
		CombineInstance[] combine = new CombineInstance[meshfilters.Length];
		int i = 0;
		while (i < meshfilters.Length)
		{
			combine[i].mesh = meshfilters[i].sharedMesh;
			combine[i].transform = meshfilters[i].transform.localToWorldMatrix;
			meshfilters[i].gameObject.SetActive(false);
			i++;
		}

		// 3. combine meshes in array, into one mesh
		MeshFilter meshfilter = this.GetComponent<MeshFilter>();
		meshfilter.mesh = new Mesh();
		meshfilter.mesh.CombineMeshes(combine, true);
		meshfilter.mesh.RecalculateBounds();
		meshfilter.mesh.RecalculateNormals();
		meshfilter.mesh.Optimize();

		// 4. remake the meshcollider
		this.gameObject.AddComponent<MeshCollider>();
		this.transform.gameObject.SetActive(true);

		
	}
}