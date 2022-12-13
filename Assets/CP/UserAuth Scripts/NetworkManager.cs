using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager _instance;

    [SerializeField] private GameObject NickNameCanvas;

    //ResponseObjects
    private LogInReceivedDataClass logInObject;
    private RegisterReceivedDataClass regObject;
    private EmailVerificationReceivedDataClass emailVerificationObject;
    private ForgotPasswordReceivedDataClass forgotPasswordObject;
    private KeyVerificationReceivedDataClass keyVarificationObject;
    private PasswordVerificationReceivedDataClass passwordVerificationObject;

    //API's
    private string logInAPI = "http://arqosh.qodequay.com:4000/users/login";
    private string createAccountAPI = "http://arqosh.qodequay.com:4000/users/create/consumer";
    private string verifyAccountAPI = "http://arqosh.qodequay.com:4000/users/reset-otp";
    private string resendOtp = "http://arqosh.qodequay.com:4000/users/resendotp";
    private string forgotPasswordAPI = "http://arqosh.qodequay.com:4000/users/forgot-password";
    private string verifyKeyAPI = "http://arqosh.qodequay.com:4000/users/check/otp";
    private string newPasswordAPI = "http://arqosh.qodequay.com:4000/users/reset-password";

    [Header("LogIn canvas References")]
    [SerializeField] TMP_InputField emailInputText;
    [SerializeField] TMP_InputField passwordInputText;
    [SerializeField] GameObject logInSuccessfull;
    [SerializeField] GameObject invalidCredentials;
    private string emailString;
    private string passwordString;

    [Header("SignUp canvas References")]
    [SerializeField] TMP_InputField nameInputText;
    [SerializeField] TMP_InputField newEmailInputText;
    [SerializeField] TMP_InputField newPasswordInputText;
    [SerializeField] TMP_InputField confirmPasswordInputText;
    [SerializeField] TMP_InputField otpInputText;
    [SerializeField] GameObject emailVerificationCanvas;
    [SerializeField] GameObject registerCanvas;
    [SerializeField] GameObject logInCanvas;
    [SerializeField] GameObject registrationSuccessfull;
    [SerializeField] GameObject invalidOtp;
    [SerializeField] GameObject passwordUnmatch;
    [SerializeField] private bool passwordMatchTrack = false;
    [SerializeField] private bool resendOTPTrack = false;
    private int resendClickCount = 0;
    private string newEmailString;
    private string newPasswordString;
    private string confirmPasswordString;
    private string otpString;

    private readonly string[] role = new string[] { "consumer" };
    private readonly string org = "Qodequay";

    [Header("Forgot passwords canvas References")]
    [SerializeField] TMP_InputField forgotPasswordEmailInputText;
    [SerializeField] TMP_InputField keyInputText;
    [SerializeField] TMP_InputField resetNewPasswordInputText;
    [SerializeField] TMP_InputField confirmNewPasswordInputText;
    [SerializeField] GameObject invalidKeyGameObject;
    [SerializeField] GameObject forgotPasswordCanvas;
    [SerializeField] GameObject keyCanvas;
    [SerializeField] GameObject newPasswordCanvas;
    [SerializeField] GameObject passwordUpdateSuccessfullObject;
    private bool isMatch;
    private string forgotPasswordEmailString;
    private string keyString;
    private string resetNewPasswordString;
    private string confirmNewPasswordString;
    

    void Awake()
    {
        _instance = this;

        //GameObject.Find("SignIn_Button").GetComponent<Button>().onClick.AddListener(OnSignInButtonSelect);
        //GameObject.Find("CreateAccount_Button").GetComponent<Button>().onClick.AddListener(OnCreateAccountButtonSelect);
        //GameObject.Find("VerifyAccount_Button").GetComponent<Button>().onClick.AddListener(OnVerifyEmailSelect);
        //GameObject.Find("SendKey_Button").GetComponent<Button>().onClick.AddListener(OnSendKeySelect);
        //GameObject.Find("VerifyKey_Button").GetComponent<Button>().onClick.AddListener(OnVerifyKeySelect);
        //GameObject.Find("ResetPassword_Button").GetComponent<Button>().onClick.AddListener(OnUpdatePassworddSelect);

        //emailInputText = GameObject.Find("LogIn_InputField").GetComponent<TMP_InputField>();
        //passwordInputText = GameObject.Find("Password_InputField").GetComponent<TMP_InputField>();

        //nameInputText = GameObject.Find("Name_InputField").GetComponent<TMP_InputField>();
        //newEmailInputText = GameObject.Find("Email_InputField").GetComponent<TMP_InputField>();
        //newPasswordInputText = GameObject.Find("Password_InputField").GetComponent<TMP_InputField>();

        //forgotPasswordEmailInputText = GameObject.Find("ForgotEmail_InputField").GetComponent<TMP_InputField>();
    }

    public void OnSignInButtonSelect()
    {
        bool isEmptyFields = false;
        emailString = emailInputText.text;
        passwordString = passwordInputText.text;

        if (emailString.Length <= 1 || passwordString.Length <= 1)
        {
            isEmptyFields = true;
        }

        if (isEmptyFields)
        {
            Debug.Log("Fields cannot be empty!!");
        }
        else
        {
            StartCoroutine(LogIn(emailString, passwordString));
        }

    }

    public void OnCreateAccountButtonSelect()
    {
        StartCoroutine(WaitSeconds());
        //newPasswordString = newPasswordInputText.text;

        //Debug.Log(nameInputText.text.Length);

        //if (newPasswordString == confirmPasswordString)
        //{
        //    passwordMatchTrack = true;
        //}

        //else
        //{
        //    return;
        //}

        RegisterBodyClass tempRegClass = new RegisterBodyClass()
        {
            name = nameInputText.text,
            email = newEmailInputText.text,
            password = newPasswordInputText.text,
            roles = role,
            organization_name = org
        };

        emailVerificationCanvas.SetActive(true);
        registerCanvas.SetActive(false);
        StartCoroutine(SignUp(tempRegClass));
    }

    public void OnResendOTPButtonSelect()
    {
        RegisterBodyClass tempRegClass = new RegisterBodyClass()
        {
            name = nameInputText.text,
            email = newEmailInputText.text,
            password = newPasswordInputText.text,
            roles = role,
            organization_name = org
        };

        StartCoroutine(ResendOtp(tempRegClass));
    }

    public void OnVerifyEmailSelect()
    {
        newEmailString = newEmailInputText.text;
        otpString = otpInputText.text;
        StartCoroutine(EmailVerification(newEmailString, otpString));
    }

    public void OnSendKeySelect()
    {
        forgotPasswordEmailString = forgotPasswordEmailInputText.text;
        StartCoroutine(ForgotPassword(forgotPasswordEmailString));
    }

    public void OnVerifyKeySelect()
    {
        keyString = keyInputText.text;
        StartCoroutine(KeyVarification(forgotPasswordEmailString, keyString));
    }

    public void OnUpdatePassworddSelect()
    {
        resetNewPasswordString = resetNewPasswordInputText.text;
        confirmNewPasswordString = confirmNewPasswordInputText.text;

        if (resetNewPasswordString == confirmNewPasswordString)
        {
            isMatch = true;
        }
        else
        {
            return;
        }

        if (isMatch)
        {
            StartCoroutine(PasswordVerification(forgotPasswordEmailString, keyString, resetNewPasswordString));
        }

        else
        {
            invalidKeyGameObject.SetActive(true);
        }
    }

    public IEnumerator LogIn(string emailParamater, string passwordParamater)
    {
        LogInBodyClass LogInClass = new LogInBodyClass(emailParamater, passwordParamater);
        string json = JsonConvert.SerializeObject(LogInClass);
        Debug.Log("==========================>" + json);

        var req = new UnityWebRequest(logInAPI, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.isNetworkError)
        {
            Debug.Log("Error While Sending: " + req.error);
        }

        else
        {
            Debug.Log("Received: " + req.downloadHandler.text);
            string temp = req.downloadHandler.text;
            LogInMethod(temp);
        }
    }

    public IEnumerator SignUp(RegisterBodyClass regObj)
    {
        string json = JsonConvert.SerializeObject(regObj);
        Debug.Log("==========================>" + json);
        var req = new UnityWebRequest(createAccountAPI, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.isNetworkError)
        {
            Debug.Log("Error While Sending: " + req.error);
        }

        else
        {
            Debug.Log("Received: " + req.downloadHandler.text);
            string temp = req.downloadHandler.text;
            SignUpMethod(temp);
        }
    }

    public IEnumerator ResendOtp(RegisterBodyClass regObj)
    {
        string json = JsonConvert.SerializeObject(regObj);
        Debug.Log("==========================>" + json);
        var req = new UnityWebRequest(resendOtp, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.isNetworkError)
        {
            Debug.Log("Error While Sending: " + req.error);
        }

        else
        {
            Debug.Log("Received: " + req.downloadHandler.text);
            string temp = req.downloadHandler.text;
            regObject = JsonConvert.DeserializeObject<RegisterReceivedDataClass>(temp);
            resendOTPTrack = true;
            resendClickCount++;
        }
    }

    public IEnumerator EmailVerification(string emailParameter, string otpParameter)
    {
        EmailVerificationBodyClass EmailVerificationClass = new EmailVerificationBodyClass(emailParameter, otpParameter);
        string json = JsonConvert.SerializeObject(EmailVerificationClass);
        Debug.Log("==========================>" + json);

        var req = new UnityWebRequest(verifyAccountAPI, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.isNetworkError)
        {
            Debug.Log("Error While Sending: " + req.error);
        }

        else
        {
            Debug.Log("Received: " + req.downloadHandler.text);
            string temp = req.downloadHandler.text;
            EmailVerificationMethod(temp);
        }
    }

    public IEnumerator ForgotPassword(string emailParamater)
    {
        ForgotPasswordBodyClass ForgotPasswordClass = new ForgotPasswordBodyClass(emailParamater);
        string json = JsonConvert.SerializeObject(ForgotPasswordClass);
        Debug.Log("==========================>" + json);

        var req = new UnityWebRequest(forgotPasswordAPI, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.isNetworkError)
        {
            Debug.Log("Error While Sending: " + req.error);
        }

        else
        {
            Debug.Log("Received: " + req.downloadHandler.text);
            string temp = req.downloadHandler.text;
            ForgotPasswordMethod(temp);
        }
    }

    public IEnumerator KeyVarification(string emailParamater, string keyParameter)
    {
        KeyVerificationBodyClass KeyVerificationClass = new KeyVerificationBodyClass(emailParamater, keyParameter);
        string json = JsonConvert.SerializeObject(KeyVerificationClass);
        Debug.Log("==========================>" + json);

        var req = new UnityWebRequest(verifyKeyAPI, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.isNetworkError)
        {
            Debug.Log("Error While Sending: " + req.error);
        }

        else
        {
            Debug.Log("Received: " + req.downloadHandler.text);
            string temp = req.downloadHandler.text;
            KeyVerificationMethod(temp);
        }
    }

    public IEnumerator PasswordVerification(string emailParameter, string otpParameter, string passwordParameter)
    {
        PasswordVerificationBodyClass PasswordVerificationClass = new PasswordVerificationBodyClass(emailParameter, otpParameter, passwordParameter);
        string json = JsonConvert.SerializeObject(PasswordVerificationClass);
        Debug.Log("==========================>" + json);

        var req = new UnityWebRequest(newPasswordAPI, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.isNetworkError)
        {
            Debug.Log("Error While Sending: " + req.error);
        }

        else
        {
            Debug.Log("Received: " + req.downloadHandler.text);
            string temp = req.downloadHandler.text;
            PasswordVerificationMethod(temp);
        }
    }

    public IEnumerator SceneTransition()
    {
        //sceneTransitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1.5f);
        //SceneManager.LoadScene(sceneToLoad);
    }

    public IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(5f);
    }

    public void LogInMethod(string logInResponse)
    {
        logInObject = JsonConvert.DeserializeObject<LogInReceivedDataClass>(logInResponse);

        if (logInObject.Message == "Email & Password do not match." || logInObject.Message == "Data not found.")
        {
            invalidCredentials.SetActive(true);
        }

        else if (logInObject.Data.IsVerified == true)
        {
            logInSuccessfull.SetActive(true);
            StartCoroutine(WaitSeconds());
            logInSuccessfull.SetActive(false);
            StartCoroutine(WaitSeconds());
            NickNameCanvas.SetActive(true);
            StartCoroutine(WaitSeconds());
            logInCanvas.SetActive(false);
        }

        else
        {
            return;
        }
    }
    public void SignUpMethod(string signUpResponse)
    {
        regObject = JsonConvert.DeserializeObject<RegisterReceivedDataClass>(signUpResponse);

        if (regObject.Data.IsVerified)
        {
            emailVerificationCanvas.SetActive(true);
            registerCanvas.SetActive(false);
        }
    }

    public void EmailVerificationMethod(string emailVerificationResponse)
    {
        emailVerificationObject = JsonConvert.DeserializeObject<EmailVerificationReceivedDataClass>(emailVerificationResponse);

        if (emailVerificationObject.Status == 1)
        {
            StartCoroutine(WaitSeconds());
            registrationSuccessfull.SetActive(true);
            StartCoroutine(WaitSeconds());
            emailVerificationCanvas.SetActive(false);
            logInCanvas.SetActive(true);
        }
        else
        {
            if (emailVerificationObject.Status == 0)
            {
                invalidOtp.SetActive(true);
            }
        }
    }

    public void ForgotPasswordMethod(string forgotPasswordResponse)
    {
        forgotPasswordObject = JsonConvert.DeserializeObject<ForgotPasswordReceivedDataClass>(forgotPasswordResponse);

        if (forgotPasswordObject.Status == 1)
        {
            keyCanvas.SetActive(true);
            forgotPasswordCanvas.SetActive(false);
        }
    }

    public void KeyVerificationMethod(string keyVerificationResponse)
    {
        keyVarificationObject = JsonConvert.DeserializeObject<KeyVerificationReceivedDataClass>(keyVerificationResponse);

        if (keyVarificationObject.Status == 1)
        {
            newPasswordCanvas.SetActive(true);
            keyCanvas.SetActive(false);
        }

        else
        {
            if (keyVarificationObject.Status == 0)
            {
                invalidKeyGameObject.SetActive(true);
            }
        }
    }

    public void PasswordVerificationMethod(string passwordVerificationResponse)
    {
        passwordVerificationObject = JsonConvert.DeserializeObject<PasswordVerificationReceivedDataClass>(passwordVerificationResponse);

        if (passwordVerificationObject.Status == 1)
        {
            passwordUpdateSuccessfullObject.SetActive(true);
            StartCoroutine(WaitSeconds());
            newPasswordCanvas.SetActive(false);
            logInCanvas.SetActive(true);

        }

        else if (passwordVerificationObject.Status == 0)
        {
            Debug.Log("Cannot change password!");
        }

        else
        {
            return;
        }
    }
}
