using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // to access images (for "filling" in the cd overlay)
using System.Linq;

public class SpecialGrenadeSelector : MonoBehaviour
{
    public GameObject gun;
    public List<GameObject> grenadeIcons;

    private GunController gunController;
    private List<int> grenadesAvailable; // grenadesAvailable[i] = 4 means that the 5th grenade type is in position i (and will be selected by pressing the #(i+1) key)
    private List<Image> cdImage;

    private void Start()
    {
        gunController = gun.GetComponent<GunController>();
        grenadesAvailable = new List<int>(grenadeIcons.Count + 1);
        cdImage = new List<Image>(grenadeIcons.Count + 1);
        //grenadesAvailable.Add(0); // ignore the first element of this list (so that it is indexed starting at 1, like the rest of this stuff)
        AddGrenade(0); // the rest are 0 (unavailable) at the start of the game for now
        AddGrenade(1);
        AddGrenade(2);
        AddGrenade(3);
        AddGrenade(4);
    }

    private void Update()
    {
        for (int i = 0; i < grenadesAvailable.Count; i++)
        {
            if (Input.GetButtonDown((i+1).ToString()))
            {
                Select(i);
                break; // prefer the smaller number if two buttons are pressed at the same time
            }

            // set the cooldown overlay image to be filled the right amount based off the remaining cooldown
            //grenadeIcons[grenadesAvailable[i]].transform.GetChild(2)
            cdImage[i].fillAmount = gunController.GetCD(grenadesAvailable[i]);
        }
    }

    public void AddGrenade(int grenadeType)
    {
        grenadesAvailable.Add(grenadeType);
        GameObject icon = grenadeIcons[grenadesAvailable[grenadesAvailable.Count - 1]];
        icon.SetActive(true);
        icon.transform.localPosition = new Vector3(200f*(grenadesAvailable.Count - 1), 100f, 0f);
        cdImage.Add(icon.transform.GetChild(2).gameObject.GetComponent<Image>());
        icon.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = grenadesAvailable.Count.ToString();
        if (grenadesAvailable.Count == 1)
        {
            Select(0);
        }

    }

    private void Select(int selection)
    {
        for (int i = 0; i < grenadesAvailable.Count; i++)
        {
            GameObject selectedIcon = grenadeIcons[grenadesAvailable[i]].transform.GetChild(0).gameObject;
            GameObject unselectedIcon = grenadeIcons[grenadesAvailable[i]].transform.GetChild(1).gameObject;

            if (i == selection)
            {
                selectedIcon.SetActive(true);
                unselectedIcon.SetActive(false);
                gunController.selected = grenadesAvailable[i];
            }
            else
            {
                selectedIcon.SetActive(false);
                unselectedIcon.SetActive(true);
            }

        }
    }
}
