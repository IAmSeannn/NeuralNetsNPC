using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour {

    public GameObject canvas;
    public GameObject MinigameFab;
    public GameManager gm;

    private Fixable fixable;

    private bool Focus = true;

	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.E))
        {
            PressedE();
        }

        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if(Focus)
            {
                GetComponent<FirstPersonController>().m_MouseLook.SetCursorTo(false);
            }
            else
            {
                GetComponent<FirstPersonController>().m_MouseLook.SetCursorTo(true);
            }

            Focus = !Focus;
        }
    }

    private void PressedE()
    {
        Debug.Log("Pressed E");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.5f))
        {
            if (hit.transform.tag == "Fixable")
            {
                if(hit.transform.GetComponent<Fixable>().Useable)
                {
                    fixable = hit.transform.GetComponent<Fixable>();
                    fixable.Deactivate();
                    CreateMinigame();
                }
            }
        }
    }

    private void CreateMinigame()
    {
        print("Hit something");
        GetComponent<FirstPersonController>().m_MouseLook.SetCursorTo(false);
        GameObject clone = Instantiate(MinigameFab) as GameObject;
        clone.transform.SetParent(canvas.transform);
        clone.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        clone.GetComponent<Minigame>().player = this;
    }

    public void ReturnControl()
    {
        GameManager.Score++;
        gm.ActivateRandomFixable();
        GetComponent<FirstPersonController>().m_MouseLook.SetCursorTo(true);
    }
}
