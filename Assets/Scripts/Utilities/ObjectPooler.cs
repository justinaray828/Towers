using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem {
	[Header("Pooling Variables")]
	private List<GameObject> pooledObjects;
	[Tooltip("GameObject to pool")]
	public GameObject objectToPool;
	[Tooltip("How many GameObjects should be stored in memory")]
	public int amountToPool;
	[Tooltip("Check if object pool should grow when needed")]
	public bool shouldExpand;
}

public class ObjectPooler : MonoBehaviour {

	public static ObjectPooler SharedInstance;
	[Tooltip("Choose parent GameObject in the Hierarcy for all instantiated PooledObjects")]
	public Transform pooledObjectsParent;
	public List<ObjectPoolItem> itemsToPool;
	private List<GameObject> pooledObjects;

	void Awake() {
		SharedInstance = this;
	}

	void Start(){
		pooledObjects = new List<GameObject>();
		foreach (ObjectPoolItem item in itemsToPool) {
			for (int i = 0; i < item.amountToPool; i++) {
				InstantiatePooledObject(item);
			}
		}
	}

	public GameObject GetPooledObject(string tag) {
		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag) {
				return pooledObjects[i];
			}
		}
		foreach (ObjectPoolItem item in itemsToPool) {
			if (item.objectToPool.tag == tag) {
				if (item.shouldExpand) {
					return InstantiatePooledObject(item);
				}
			}
		}
		return null;
	}

	private GameObject InstantiatePooledObject(ObjectPoolItem item){
		GameObject obj = (GameObject)Instantiate(item.objectToPool);
		obj.SetActive(false);
		obj.transform.SetParent (pooledObjectsParent);
		pooledObjects.Add(obj);
		return obj;
	}
}
