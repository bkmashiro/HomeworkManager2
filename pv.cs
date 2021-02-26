using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace HomeworkManager
{
    public class pv
    {
        public static string VersionNumber_ = "105";
        public struct Task
        {
            public string name;
            public string Content;
            public DateTime StartTime;
            public DateTime EndTime;
            public bool Finished;
            public string Appendix;
        };

        public static List<List<string>> candiate = new List<List<string>>();
        public static List<string> candiate_lesson = new List<string>();
        public static Task Temp_task = new Task();

        public static void ReadXML()
        {
            pv.candiate.Clear();
            pv.candiate_lesson.Clear();
            XDocument xDoc = XDocument.Load(System.Windows.Forms.Application.StartupPath + "\\" + "Settings.xml");
            XElement rootElement = xDoc.Root;
            ReadProgrammeSettings(rootElement.Element("ProgrammeSettings"));
            ReadUsersData(rootElement.Element("UsersData"));
        }
        public static void ReadProgrammeSettings(XElement Settings)
        {

        }
        public static void ReadUsersData(XElement UsersData)
        {
            List<string> tmp = new List<string>();
            foreach (var subject in UsersData.Elements("Subjects"))
            {
                if (!subject.IsEmpty)
                {
                    foreach (var item in subject.Elements("word"))
                    {
                        if (!item.IsEmpty)
                        {
                            tmp.Add((string)item);
                        }
                    }
                }
                if (tmp.Count > 0 && subject.FirstAttribute.Value != string.Empty)
                {
                    pv.candiate_lesson.Add((string)subject.FirstAttribute);
                    pv.candiate.Add(tmp);
                }
                tmp = new List<string>();
            }
        }


    }
}
