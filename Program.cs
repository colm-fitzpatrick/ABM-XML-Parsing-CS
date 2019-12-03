using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace ABM_XML_Parsing
{
    class XMLQuestions
    {
        public void question1()
        {
            IDictionary<string, string> results = new Dictionary<string, string>(); // dictionary to store results
            string edifact = @"UNA:+.? '
                UNB+UNOC:3+2021000969+4441963198+180525:1225+3VAL2MJV6EH9IX+KMSV7HMD+CUSDECU-IE++1++1'
                UNH+EDIFACT+CUSDEC:D:96B:UN:145050'
                BGM+ZEM:::EX+09SEE7JPUV5HC06IC6+Z'
                LOC+17+IT044100'
                LOC+18+SOL'
                LOC+35+SE'
                LOC+36+TZ'
                LOC+116+SE003033'
                DTM+9:20090527:102'
                DTM+268:20090626:102'
                DTM+182:20090527:102'";

            // Create a pattern
            string pattern = @"LOC\+([A-Za-z0-9\+]+)";
            // Create a Regex  
            Regex rg = new Regex(pattern);

            // Get all matches  
            MatchCollection matches = rg.Matches(edifact);
            // Process matched lines  
            for (int count = 0; count < matches.Count; count++)
            {
                string temp = matches[count].Value.Replace("LOC+", "");// Remove LOC+
                String[] strlist = temp.Split('+'); // Split two values by +
                results.Add(strlist[0], strlist[1]);
                Array.Clear(strlist, 0, strlist.Length); // clear temp var
            }

            foreach (KeyValuePair<string, string> kv in results) // printing results
            {
                Console.WriteLine("2nd Element = {0}, 3rd Element = {1}", kv.Key, kv.Value);
            }
        }

        public void question2() // The xml file provided was missing </Declaration> so I added it
        {
            List<string> RefCodes = new List<string>() // list of target RefCodes
            {
                "MWB",
                "TRV",
                "CAR"
            };

            XmlDocument doc = new XmlDocument(); // creating xml document object
            string path1 = System.IO.Path.GetFullPath(@"..\..\..\");
            string path2 = @"Data\Q2.xml";
            string path = Path.Combine(path1,path2); // creating path to xml file
            IDictionary<string, string> results = new Dictionary<string, string>(); // dictionary to store results
            doc.Load(path); // loading xml data

            XmlNodeList Reference = doc.GetElementsByTagName("Reference"); // targeting Reference node

            foreach (XmlNode elem in Reference)
            {
                if (RefCodes.Contains(elem.Attributes["RefCode"].Value)) // if a targeted RefCode
                {
                    results.Add(elem.Attributes["RefCode"].Value, elem["RefText"].InnerText); // add data to results dictionary
                }
            }

            foreach (KeyValuePair<string,string> kv in results) // printing results
            {
                Console.WriteLine("RefCode = {0}, RefText = {1}", kv.Key, kv.Value);
            }
        }
        static void Main()
        {
            XMLQuestions q1 = new XMLQuestions();
            Console.WriteLine("Question One Results:");
            q1.question1();
            Console.WriteLine("");
            Console.WriteLine("Question Two Results:");
            q1.question2();
        }
    }
}
