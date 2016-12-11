using UnityEngine;
using System.Collections;

public class EndPlayer : MonoBehaviour {

    private Vector3 _destination;

    void Start()
    {
        _destination = MapController.GenerateRandomPointInsideMap();
    }

    void Update()
    {
        if(Vector3.Distance(_destination, transform.position) < 1f)
        {
            _destination = MapController.GenerateRandomPointInsideMap();
        }
        var dir = _destination - transform.position;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg));
        this.transform.Translate(transform.right  * 10f * Time.deltaTime);
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
