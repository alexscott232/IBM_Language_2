using IBM.Cloud.SDK;
using IBM.Cloud.SDK.Authentication;
using IBM.Cloud.SDK.Authentication.Iam;
using IBM.Cloud.SDK.Utilities;
using IBM.Watson.LanguageTranslator.V3;
using IBM.Watson.LanguageTranslator.V3.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageTranslatorScript : MonoBehaviour
{
    #region PLEASE SET THESE VARIABLES IN THE INSPECTOR
    [Space(10)]
    [Tooltip("The service URL (optional). This defaults to \"https://gateway.watsonplatform.net/langauge-translator/api\"")]
    [SerializeField]
    private string serviceUrl;
    //[Tooltip("Text field to display the results of translation.")]
    //public Text ResultsField;
    //[Tooltip("Input field to display what is to be translated")]
    //public InputField TextInput;
    //[Header("IAM Authentication")]
    //[Tooltip("The IAM apikey.")]
    [SerializeField]
    private string iamApikey;
    [Header("Parameters")]
    // https://cloud.ibm.com/apidocs/language-translator#list-models
    [Tooltip("The translation model to use. See https://cloud.ibm.com/apidocs/language-translator#list-models.")]
    [SerializeField]
    private string translationModel;
    [Tooltip("The version date with which you would like to use the service in the form YYYY-MM-DD.")]
    [SerializeField]
    private string versionDate;
    #endregion

    private LanguageTranslatorService languageTranslator;

    public string username;
    public int maxMessages = 25;

    public GameObject chatPanel, textObject;
    public InputField chatBox;

    //public Color playerMessage, info;

    [SerializeField]
    List<Message> messageList = new List<Message>();
    // Start is called before the first frame update
    void Start()
    {
        if (string.IsNullOrEmpty(iamApikey))
            throw new IBMException("Please set the Language Translator iamApikey in the inspector.");
        if (string.IsNullOrEmpty(translationModel))
            throw new IBMException("Please set the translationModel in the inspector.");
        Runnable.Run(CreateService());
    }

    // Update is called once per frame
    void Update()
    {
        if (chatBox.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //SendMessageToChat(username + ": " + chatBox.text, Message.MessageType.playerMessage);
                SendMessageToChat(username + ": " + chatBox.text);
                Translation(chatBox.text);
                chatBox.text = "";

            }
        }
        else
        {
            if (!chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                chatBox.ActivateInputField();
            }
        }
        if (!chatBox.isFocused)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //SendMessageToChat("You Pressed space!", Message.MessageType.info);
                SendMessageToChat("You Pressed space!");
            }
        }
    }

    private IEnumerator CreateService()
    {
        Debug.Log("Inside Create Service");
        if (string.IsNullOrEmpty(iamApikey))
        {
            throw new IBMException("Plesae provide IAM ApiKey for the service.");
        }

        IamAuthenticator authenticator = new IamAuthenticator(apikey: iamApikey);

        //  Wait for tokendata
        while (!authenticator.CanAuthenticate())
        {
            //Log.Debug("NULLLLLLLLLLL", "DEBUGGG");
            yield return null;
        }

        languageTranslator = new LanguageTranslatorService(versionDate, authenticator);

        if (!string.IsNullOrEmpty(serviceUrl))
        {
            languageTranslator.SetServiceUrl(serviceUrl);
        }
    }

    public void Translation(string text)
    {
        //  Array of text to translate
        List<string> translateText = new List<string>();
        translateText.Add(text);

        //  Call to the service
        languageTranslator.Translate(OnTranslate, translateText, translationModel);
    }

    private void OnTranslate(DetailedResponse<TranslationResult> response, IBMError error)
    {
        string outputSentence = response.Result.Translations[0]._Translation;

        //Debug.Log(outputSentence + "here");
        ////ResultsField.text = "hi";
        //ResultsField.text = outputSentence;
        SendMessageToChat("Translator: " + outputSentence);
    }


    //public void SendMessageToChat(string text, Message.MessageType messageType)
    public void SendMessageToChat(string text)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();  //ADD new message to list

        newMessage.text = text; //ADD new message to list

        GameObject newText = Instantiate(textObject, chatPanel.transform);

        newMessage.textObject = newText.GetComponent<Text>();

        newMessage.textObject.text = newMessage.text;
        //newMessage.textObject.color = MessageTypeColor(messageType);

        messageList.Add(newMessage); //ADD new message to list
    }

    //Color MessageTypeColor(Message.MessageType messageType)
    //{
    //    Color color = info;

    //    switch(messageType)
    //    {
    //        case Message.MessageType.playerMessage:
    //            color = playerMessage;
    //            break; //to end the case statement
    //    }

    //    return color;
    //}
}

[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
    //public MessageType messageType;

    //public enum MessageType
    //{
    //    playerMessage,
    //    info
    //}
}
