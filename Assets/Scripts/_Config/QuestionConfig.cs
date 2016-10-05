using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PlayBento
{
	public class QuestionConfig : BentoConfig 
	{
		public List<QuestionData> data = new List<QuestionData>();

        public QuestionData GetDialogDataBySceneName(string _sceneName)
        {
            foreach(QuestionData _data in data)
            {
                if (_data.SceneName == _sceneName)
                    return _data;
            }
            return null;
        }
	}

	public class QuestionData
	{
        [XmlAttribute("Type")]
        public string type;
        [XmlAttribute("SceneName")]
        public string SceneName;
        public string Intro;
		public string Intro2;
        public string Question;
        [XmlArray("Choices")]
        [XmlArrayItem("Choice")]
        public List<string> ChoiceList = new List<string>();
        public int Answer;
        public string AfterRightAnswer;
        public string AfterWrongAnswer;
        public string AfterWrongAnswer2;
    }

}