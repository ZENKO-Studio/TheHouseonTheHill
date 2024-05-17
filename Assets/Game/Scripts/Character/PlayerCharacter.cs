using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : CharacterBase
{
    //#TODO Remove later, just for testing purpose
    public float movSpeed = 5f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * movSpeed, 0, Input.GetAxis("Vertical") * Time.deltaTime * movSpeed));
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        //#TODO: Remove later just for trial purpose
        if (health <= 0)
            SceneManager.LoadScene(0);
    }
}
