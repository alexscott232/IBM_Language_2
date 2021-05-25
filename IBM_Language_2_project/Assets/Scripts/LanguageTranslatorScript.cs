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

namespace LanguageTranslatorAssistance
{

    public class LanguageTranslatorScript : MonoBehaviour
    {
        #region PLEASE SET THESE VARIABLES IN THE INSPECTOR
        [Space(10)]
        [Tooltip("The service URL (optional). This defaults to \"https://gateway.watsonplatform.net/langauge-translator/api\"")]
        [SerializeField]
        private string serviceUrl;
        [Tooltip("Text field to display the results of translation.")]
        public Text ResultsField;
        [Tooltip("Input field to display what is to be translated")]
        public InputField TextInput;
        [Header("IAM Authentication")]
        [Tooltip("The IAM apikey.")]
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

        public Button enterButton;

        void Start()
        {
            if (string.IsNullOrEmpty(iamApikey))
                throw new IBMException("Please set the Language Translator iamApikey in the inspector.");
            if (string.IsNullOrEmpty(translationModel))
                throw new IBMException("Please set the translationModel in the inspector.");
            //  Start coroutine to create service
            //StartCoroutine(CreateService());
            Button btn = enterButton.GetComponent<Button>();
            btn.onClick.AddListener(TextEntered);
            Runnable.Run(CreateService());
        }

        private void TextEntered()
        {
            Debug.Log("Inside TextEntered");

            string userInput = TextInput.text;
            Debug.Log(userInput);


            Translation(userInput);
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


        //  Call this method from ExampleStreaming
        public void Translation(string text)
        {
            //  Array of text to translate
            List<string> translateText = new List<string>();
            translateText.Add(text);

            //  Call to the service
            languageTranslator.Translate(OnTranslate, translateText, translationModel);
        }

        //  OnTranslate handler
        private void OnTranslate(DetailedResponse<TranslationResult> response, IBMError error)
        {
            string outputSentence = response.Result.Translations[0]._Translation;

            Debug.Log(outputSentence + "here");
            //ResultsField.text = "hi";
            ResultsField.text = outputSentence;
        }
    }


}
