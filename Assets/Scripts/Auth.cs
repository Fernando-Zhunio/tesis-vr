using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Auth : Conexion
{
    public Text text_header;
    [Header("Inputs de login")] 
    public TextMeshProUGUI txt_email;
    public TMP_InputField txt_password;

    [Header("Inputs de registro")]
    public TextMeshProUGUI txt_name_r;
    public TextMeshProUGUI txt_email_r;
    public TextMeshProUGUI txt_password_r;
    public TextMeshProUGUI txt_password_confirmed_r;

    [Header("Gameobject de login y registro")]
    public GameObject Panel_login;
    public GameObject Panel_register;

    [Header("Gameobject de cargando")]
    public GameObject Loading;

    [Header("Input fecha")]
    public TMP_InputField txt_birthday;
    [Space]
    public Toggle toogle_isStudent;

    int prevLength;

    public void Start()
    {
        txt_birthday.onValueChanged.AddListener(OnValueChanged);

        Panel_login.SetActive(true);
        Panel_register.SetActive(false);
    }

    public void OnValueChanged(string str)
    {
        print("String:" + str);
        if (str.Length > 0)
        {
            txt_birthday.onValueChanged.RemoveAllListeners();
            if (!char.IsDigit(str[str.Length - 1]) && str[str.Length - 1] != '/' || txt_birthday.text.Length  > 10)
            { // Remove Letters
                txt_birthday.text = str.Remove(str.Length - 1);
                txt_birthday.caretPosition = txt_birthday.text.Length;
            }
            else if (str.Length == 2 || str.Length == 5)
            {
                if (str.Length < prevLength)
                { // Delete
                    txt_birthday.text = str.Remove(str.Length - 1);
                    txt_birthday.caretPosition = txt_birthday.text.Length;
                }
                else
                { // Add
                    txt_birthday.text = str;
                    txt_birthday.caretPosition = txt_birthday.text.Length +1;
                }
            }
            txt_birthday.onValueChanged.AddListener(OnValueChanged);
        }
        prevLength = txt_birthday.text.Length;
    }

    public override void getResponse<T>(T _data)
    {
        print("Desde el metodo request:" +_data);
        if (Panel_login.activeInHierarchy)
        {
        SessionModel data = JsonUtility.FromJson<SessionModel>(_data.ToString());
        print(" request:" +data.user.name);
            Global.SetSession(data);
        }
        else
        {
            Global.ShowAndroidToastMessage("Registro correcto", "Ahora puedes iniciar sesion!", NotificationType.success);
            goLogin();
        }
    }


    public void goRegister()
    {
        Panel_login.SetActive(false);
        Panel_register.SetActive(true);
        text_header.text = "Creando Usuario";
    }

    public void goLogin()
    {
        Panel_login.SetActive(true);
        Panel_register.SetActive(false);
        text_header.text = "Inicio de Sesion";

    }

    public void login()
    {
        //Loading.SetActive(true);
        Global.ShowAndroidToastMessage("Cargando", "Verificando credenciales", NotificationType.info);
        WWWForm form = new WWWForm();
        form.AddField("email", txt_email.text);
        form.AddField("password", txt_password.text);
        print(txt_email.text + "-" + txt_password.text);
        StartCoroutine(CallPostBackend(Routes.login, form));
    }

    public void register()
    {
        Loading.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("name", txt_name_r.text);
        form.AddField("email", txt_email_r.text);
        form.AddField("password", txt_password_r.text);
        form.AddField("birthday", txt_birthday.text);
        form.AddField("is_student", toogle_isStudent.isOn ? 1.ToString() : 0.ToString());
        form.AddField("password_confirmation", txt_password_confirmed_r.text);
        StartCoroutine(CallPostBackend(Routes.register, form));
    }


    protected override void HideLoading()
    {
        //Loading.SetActive(false);
        //Global._ShowAndroidToastMessage("Credeiciales", "Usuario autenticado con exito", NotificationType.success);
    }

}
