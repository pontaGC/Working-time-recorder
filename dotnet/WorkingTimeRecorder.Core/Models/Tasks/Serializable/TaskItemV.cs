using System.Xml.Serialization;

namespace WorkingTimeRecorder.Core.Models.Tasks.Serializable
{
    /// <summary>
    /// The variable task item.
    /// </summary>
    [Serializable]
    public class TaskItemV
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlElement("ElapsedWorkTime")]
        public ElapsedWorkTimeV ElapsedWorkTime { get; set; }
    }
}
