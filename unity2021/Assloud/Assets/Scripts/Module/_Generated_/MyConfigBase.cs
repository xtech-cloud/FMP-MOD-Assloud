
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.12.0.  DO NOT EDIT!
//*************************************************************************************

using System.Xml.Serialization;

namespace XTC.FMP.MOD.Assloud.LIB.Unity
{
    public class MyConfigBase
    {
        public class GRPC
        {
            [XmlAttribute("address")]
            public string address { get; set; } = "";
        }

        public class UI
        {
            [XmlAttribute("visible")]
            public bool visible { get; set; } = false;

            [XmlAttribute("slot")]
            public string slot { get; set; } = "[root]";
        }

        public class Instance
        {
            [XmlAttribute("uid")]
            public string uid { get; set; } = "";
            [XmlAttribute("style")]
            public string style { get; set; } = "";
        }

        public class Parameter
        {
            [XmlAttribute("key")]
            public string key { get; set; } = "";
            [XmlAttribute("value")]
            public string value { get; set; } = "";
            [XmlAttribute("type")]
            public string type { get; set; } = "";
        }


        public class Subject
        {
            [XmlAttribute("message")]
            public string message { get; set; } = "";
            [XmlArray("Parameters"), XmlArrayItem("Parameter")]
            public Parameter[] parameters { get; set; } = new Parameter[0];
        }

        public class Preload
        {
            [XmlArray("Subjects"), XmlArrayItem("Subject")]
            public Subject[] subjects { get; set; } = new Subject[0];
        }

        [XmlAttribute("version")]
        public string version { get; set; } = "";

        [XmlElement("GRPC")]
        public GRPC grpc { get; set; } = new GRPC();

        [XmlElement("UI")]
        public UI ui { get; set; } = new UI();

        [XmlArray("Instances"), XmlArrayItem("Instance")]
        public Instance[] instances { get; set; } = new Instance[0];
        [XmlElement("Preload")]
        public Preload preload { get; set; } = new Preload();
    }
}

