using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ValidationErrorsModel 
{
    public bool success;
    public string errors;
    public static ValidationErrorsModel CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<ValidationErrorsModel>(jsonString);
    }
}
