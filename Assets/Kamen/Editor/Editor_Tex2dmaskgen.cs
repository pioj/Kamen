using System.IO;
using UnityEditor;
using UnityEngine;

namespace pioj.kamen
{
    public class Editor_Tex2dmaskgen : ScriptableWizard
    {
        public Texture2D source;
        public Color maskColor = Color.magenta;
        private Texture2D preview;

        [MenuItem("Tools/Kamen/Generate mask texture...")]
        static void CreateWizard()
        {
            ScriptableWizard.DisplayWizard<Editor_Tex2dmaskgen>("Mask Texture Generator", "Create Mask");
        }

        void OnWizardCreate()
        {
            if (source && preview)
            {
                var filepath = AssetDatabase.GetAssetPath(source);
                filepath += "_mask.png";
                    //AssetDatabase.CreateAsset(preview,filepath);
                 //File.WriteAllBytes(Application.dataPath + filepath, bytes);
                 File.WriteAllBytes(filepath, preview.EncodeToPNG());
                 AssetDatabase.Refresh();
                 Close();
            }
        }

        
        
        //SOLO EN CASO DE QUE NO FUNCIONE EL PREVIEWTEXTURE EN UN SCRIPTABLEWIZARD!!!
        
        void OnGUI()
        {
            base.DrawWizardGUI();
            if (source && source.isReadable)
            {
                preview = (source.alphaIsTransparency) ? source.OverwriteAlpha() : source.OverwriteColor(maskColor);

                EditorGUILayout.LabelField("Alpha channel detected, will use this instead of Mask color");
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                
                EditorGUILayout.BeginVertical(); // #PreviewSource
                EditorGUILayout.LabelField("Source:");
                EditorGUI.DrawPreviewTexture(new Rect(5,90,100, 100) , source, null, ScaleMode.ScaleAndCrop);
                EditorGUILayout.EndVertical();
                
                EditorGUILayout.BeginVertical(); // #PreviewMask
                EditorGUILayout.LabelField("Preview:");
                EditorGUI.DrawPreviewTexture(new Rect(200,90,100, 100) , preview, null, ScaleMode.ScaleAndCrop);
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
                
                //llamo a la fuerza al evento del botón..
                EditorGUILayout.Space();
                if (GUILayout.Button("Create Mask")) OnWizardCreate();
            }
        }

        void OnWizardUpdate()
        {
            isValid = source;
            helpString = source ? "" : "Select the Texture Asset...";
            if (source && source.alphaIsTransparency) helpString = "Alpha channel detected, will use this instead of black";
            if (source && !source.isReadable)
                helpString = "WARNING! Texture is NOT readable. Please fix it in your settings.";
        }

    }

    #region Texture2DExtensions
    public static class Texture2DExtensions
    {
        public static Texture2D OverwriteColor(this Texture2D texture, Color color)
        {
            Texture2D newTexture = new Texture2D(texture.width, texture.height);

            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    Color pixelColor = texture.GetPixel(x, y);
                    if (pixelColor == color)
                    {
                        newTexture.SetPixel(x, y, Color.clear);
                    }
                    else
                    {
                        newTexture.SetPixel(x, y, Color.white);
                    }
                }
            }

            newTexture.Apply();
            return newTexture;
        }

        public static Texture2D OverwriteAlpha(this Texture2D texture)
        {
            Texture2D newTexture = new Texture2D(texture.width, texture.height);

            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    Color pixelColor = texture.GetPixel(x, y);

                    if (pixelColor.a < 1f)
                    {
                        newTexture.SetPixel(x, y, Color.clear);
                    }
                    else
                    {
                        newTexture.SetPixel(x, y, Color.white);
                    }
                }
            }
            newTexture.Apply();
            return newTexture;
        }
    }
    #endregion
}
