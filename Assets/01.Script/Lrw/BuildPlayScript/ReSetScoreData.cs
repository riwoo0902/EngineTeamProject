using _01.Script.Lrw.File;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace _01.Script.Lrw.BuildPlayScript
{
    public class ReSetScoreData : IPreprocessBuildWithReport
    {
        public int callbackOrder => 1;
        public void OnPreprocessBuild(BuildReport report)
        {
            Debug.Log("ScoreData Reset");
            FileManager.SetFile("0","Score");
        }
    }
}