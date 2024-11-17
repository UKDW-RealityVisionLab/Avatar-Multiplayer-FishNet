using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseController : MonoBehaviour
{
    public GameObject loginPanel, signupPanel, profilPanel, forgetPasswordPanel, notificationPanel;

    public InputField loginEmail, loginPassword,signupUserName, signupEmail, signupPassword, signupCPassword, forgetPassEmail;

    public Text notif_Title_Text, notif_Message_Text, profileUserName_Text, profileUserEmail_Text;

    public Toggle rememberMe;

    public void OpenLogInPanel(){
        loginPanel.SetActive(true);
        signupPanel.SetActive(false);
        profilPanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
    }

    public void OpenSignUpPanel(){
        loginPanel.SetActive(false);
        signupPanel.SetActive(true);
        profilPanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
    }
    
    public void OpenProfilPanel(){
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        profilPanel.SetActive(true);
        forgetPasswordPanel.SetActive(false);
    }

    public void OpenForgetPasswordPanel(){
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        profilPanel.SetActive(false);
        forgetPasswordPanel.SetActive(true);
    }

    public void LogInUser(){
        if(string.IsNullOrEmpty(loginEmail.text)&&string.IsNullOrEmpty(loginPassword.text)){
            showNotificationMessage("Error", "Fields Empty! Please, Input Details in All Fields");
            return;
        }
        // do login
    }

    public void SignUpUser(){
        if(string.IsNullOrEmpty(signupEmail.text)&&string.IsNullOrEmpty(signupPassword.text)&& string.IsNullOrEmpty(signupCPassword.text)&& string.IsNullOrEmpty(signupUserName.text)){
            showNotificationMessage("Error", "Fields Empty! Please, Input Details in All Fields");
            return;
        }
        // do signup
    }

    public void forgetPass(){
        if(string.IsNullOrEmpty(forgetPassEmail.text)){
            showNotificationMessage("Error", "Fields Empty! Please, Input Details in Fields");
            return;
    }
    }

    private void showNotificationMessage(string title, string message){
        notif_Title_Text.text = "" + title;
        notif_Message_Text.text = "" + message;

        notificationPanel.SetActive(true);
    }

    public void CloseNotif_Panel(){
        notif_Title_Text.text = "";
        notif_Message_Text.text = "";

        notificationPanel.SetActive(false);
    }

    public void LogOut(){
  
        profileUserEmail_Text.text = "";
        profileUserName_Text.text = "";
        OpenLogInPanel();
    }

}
