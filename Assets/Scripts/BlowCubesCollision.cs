using UnityEngine;

public class BlowCubesCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject restartButton;
    public GameObject rotator;
    public GameObject camera;
    public Animator camShake;
    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag=="Cubes")
        {
            for(int i=collision.transform.childCount-1;i>=0;i--)
            {
                Transform child = collision.transform.GetChild(i);
                child.gameObject.AddComponent<Rigidbody>();
                child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(100f, Vector3.up, 15f);
                child.SetParent(null);
            }
            restartButton.SetActive(true);
            Camera.main.fieldOfView = 90;
            camShake.SetTrigger("Shake");
            Destroy(collision.gameObject);
        }
        
    }
}
