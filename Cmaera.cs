using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmaera : MonoBehaviour
{

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private float _relativeSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(
                _camera.transform.position.x * _relativeSpeed,
                 _camera.transform.position.y * _relativeSpeed);
    }
}
