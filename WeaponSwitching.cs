using UnityEngine;
using System.Collections;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public float WeaponSwitchSpeed = 5f;
    public Vector3 position1;
    public Vector3 position2;
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;

    public GameObject weapon4;
    public bool Selected1;
    public bool Selected2;
    public bool Selected3;
    public bool Selected4;
    void Start()
    {
        StartCoroutine(SelectWeapon());
    }

    void Update()
    {
        int i = 0;

        int previousSelectedWeapon = selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedWeapon = 3;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            StartCoroutine(SelectWeapon());
            SelectWeapon();
        }
 
        #region Selection bools
        if (selectedWeapon == 0)
        {
            Selected1 = true;
        }
        else
        {
            Selected1 = false;
        }

        if (selectedWeapon == 1)
        {
            Selected2 = true;
        }
        else
        {
            Selected2 = false;
        }

        if (selectedWeapon == 2)
        {
            Selected3 = true;
        }
        else
        {
            Selected3 = false;
        }
        if (selectedWeapon == 3)
        {
            Selected4 = true;
        }
        else
        {
            Selected4 = false;
        }
        #endregion
        #region transitions
        if(Selected1 == true && weapon1.activeSelf == true)
        {
            weapon1.transform.localPosition = Vector3.Lerp(weapon1.transform.localPosition, position1, WeaponSwitchSpeed * Time.deltaTime);
        }
        else
        {
            weapon1.transform.localPosition = Vector3.Lerp(weapon1.transform.localPosition, position2, WeaponSwitchSpeed * Time.deltaTime);
        }
        if (Selected2 == true && weapon2.activeSelf == true)
        {
            weapon2.transform.localPosition = Vector3.Lerp(weapon2.transform.localPosition, position1, WeaponSwitchSpeed * Time.deltaTime);
        }
        else
        {
            weapon2.transform.localPosition = Vector3.Lerp(weapon2.transform.localPosition, position2, WeaponSwitchSpeed * Time.deltaTime);
        }
        if (Selected3 == true && weapon3.activeSelf == true)
        {
            weapon3.transform.localPosition = Vector3.Lerp(weapon3.transform.localPosition, position1, WeaponSwitchSpeed * Time.deltaTime);
        }
        else
        {
            weapon3.transform.localPosition = Vector3.Lerp(weapon3.transform.localPosition, position2, WeaponSwitchSpeed * Time.deltaTime);
        }
        if (Selected4 == true && weapon4.activeSelf == true)
        {
            weapon3.transform.localPosition = Vector3.Lerp(weapon3.transform.localPosition, position1, WeaponSwitchSpeed * Time.deltaTime);
        }
        else
        {
            weapon3.transform.localPosition = Vector3.Lerp(weapon3.transform.localPosition, position2, WeaponSwitchSpeed * Time.deltaTime);
        }
        #endregion

    }

    IEnumerator SelectWeapon()
    {
        yield return new WaitForSeconds(0f);
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
                
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}