#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;

namespace JS_Resources
{
    public static class JSResources
    {
        public static T DownloadFile<T>(string url, string savePath, bool overwrite = true) where T : Object
        {
            if (!overwrite)
                savePath = AssetDatabase.GenerateUniqueAssetPath(savePath);

            using (UnityWebRequest client = new UnityWebRequest(url))
            {
                client.downloadHandler = new DownloadHandlerFile(savePath);
                client.timeout = 10;
                client.SendWebRequest();

                bool stop = false;
                float lastProgress = 0;
                while (!stop)
                {
                    if (client.isNetworkError || client.isHttpError)
                    {
                        stop = true;
                        EditorUtility.ClearProgressBar();
                        Debug.LogError(client.error);
                    }
                    if (client.isDone)
                    {
                        EditorUtility.ClearProgressBar();
                        AssetDatabase.ImportAsset(savePath);
                        return AssetDatabase.LoadAssetAtPath<T>(savePath);
                    }
                    if (lastProgress != client.downloadProgress)
                    {
                        lastProgress = client.downloadProgress;
                        string fileSize = "0";
                        if (client.downloadProgress != 0)
                        {
                            fileSize = (client.downloadedBytes / (float)100000 / client.downloadProgress).ToString();
                            fileSize = fileSize.Substring(0, fileSize.IndexOf('.') + 2);
                        }
                        string downloaded = (client.downloadedBytes / (float)100000).ToString();
                        downloaded = downloaded.Substring(0, downloaded.IndexOf('.') + 2);
                        if (EditorUtility.DisplayCancelableProgressBar("Downloading Files", "Download Progress: " + downloaded + " / " + fileSize, lastProgress))
                        {
                            stop = true;
                            EditorUtility.ClearProgressBar();
                        }
                    }

                }
            }
            return null;
        }

        public static Texture2D DownloadSprite(string url, string savePath, bool overwrite = true)
        {
            Texture2D Icon = DownloadFile<Texture2D>(url, savePath);
            TextureImporter Sprite = (TextureImporter)TextureImporter.GetAtPath(savePath);
            Sprite.maxTextureSize = 256;
            Sprite.textureCompression = TextureImporterCompression.Compressed;
            Sprite.streamingMipmaps = true;
            Sprite.textureType = TextureImporterType.Sprite;
            EditorUtility.SetDirty(Sprite);
            Sprite.SaveAndReimport();
            return Icon;
        }

        public static void JSCredits()
        {
            //Declare Icons
            Texture2D IconJSLogo;
            Texture2D IconDiscord;
            Texture2D IconGithub;
            Texture2D IconGumroad;

            #region Download Icons

            //Download Logo
            if (AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/JustSleightly/Resources/Icons/JSLogo.png") == null)
            {
                IconJSLogo = SleightlyBall.DownloadSprite("https://github.com/JustSleightly/Resources/raw/main/Icons/JS_Logo%20Normal%20Lines%20Rounded.png", "Assets/JustSleightly/Resources/Icons/JSLogo.png", false);
            }

            else
            {
                IconJSLogo = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/JustSleightly/Resources/Icons/JSLogo.png", typeof(Texture2D));
            }

            //Download Discord Icon
            if (AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/JustSleightly/Resources/Icons/Discord.png") == null)
            {
                IconDiscord = SleightlyBall.DownloadSprite("https://github.com/JustSleightly/Resources/raw/main/Icons/Discord.png", "Assets/JustSleightly/Resources/Icons/Discord.png", false);
            }

            else
            {
                IconDiscord = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/JustSleightly/Resources/Icons/Discord.png", typeof(Texture2D));
            }

            //Discord GitHub Icon
            if (AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/JustSleightly/Resources/Icons/GitHub.png") == null)
            {
                IconGithub = SleightlyBall.DownloadSprite("https://github.com/JustSleightly/Resources/raw/main/Icons/GitHub.png", "Assets/JustSleightly/Resources/Icons/GitHub.png", false);
            }

            else
            {
                IconGithub = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/JustSleightly/Resources/Icons/GitHub.png", typeof(Texture2D));
            }

            //Discord Gumroad Icon
            if (AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/JustSleightly/Resources/Icons/Gumroad.png") == null)
            {
                IconGumroad = SleightlyBall.DownloadSprite("https://github.com/JustSleightly/Resources/raw/main/Icons/Gumroad.png", "Assets/JustSleightly/Resources/Icons/Gumroad.png", false);
            }

            else
            {
                IconGumroad = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/JustSleightly/Resources/Icons/Gumroad.png", typeof(Texture2D));
            }

            #endregion

            //Create GUI Style for Name
            GUIStyle CreditsLabel = new GUIStyle();
            CreditsLabel.alignment = TextAnchor.MiddleRight;
            CreditsLabel.fixedHeight = 40;
            CreditsLabel.richText = true;

            //Name Label
            EditorGUILayout.LabelField("<size=15><i><b><color=#ff6961>JustSleightly#0001</color></b></i></size>", CreditsLabel);

            //JSLogo Icon
            if (IconJSLogo != null)
            {
                GUIContent buttonJSLogocontent = new GUIContent(IconJSLogo);
                buttonJSLogocontent.tooltip = "JustSleightly";

                if (GUILayout.Button(buttonJSLogocontent, "label", GUILayout.Width(40), GUILayout.Height(40)))
                {
                    //Application.OpenURL("https://vrc.sleightly.dev/");
                }
            }

            //Discord Icon
            if (IconDiscord != null)
            {
                GUIContent buttonDiscordcontent = new GUIContent(IconDiscord);
                buttonDiscordcontent.tooltip = "Discord";

                if (GUILayout.Button(buttonDiscordcontent, "label", GUILayout.Width(40), GUILayout.Height(40)))
                {
                    Application.OpenURL("https://discord.gg/dakrwvjDzR");
                }
            }

            //GitHub Icon
            if (IconGithub != null)
            {
                GUIContent buttonGitHubcontent = new GUIContent(IconGithub);
                buttonGitHubcontent.tooltip = "GitHub";

                if (GUILayout.Button(buttonGitHubcontent, "label", GUILayout.Width(40), GUILayout.Height(40)))
                {
                    Application.OpenURL("https://github.com/JustSleightly");
                }
            }

            //Gumroad Icon
            if (IconGumroad != null)
            {
                GUIContent buttonGumroadcontent = new GUIContent(IconGumroad);
                buttonGumroadcontent.tooltip = "Gumroad";

                if (GUILayout.Button(buttonGumroadcontent, "label", GUILayout.Width(40), GUILayout.Height(40)))
                {
                    Application.OpenURL("https://gumroad.com/justsleightly");
                }
            }
        }
    }
}
#endif