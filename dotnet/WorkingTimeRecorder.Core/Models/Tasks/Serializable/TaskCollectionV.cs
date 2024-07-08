using System.Xml.Serialization;

namespace WorkingTimeRecorder.Core.Models.Tasks.Serializable
{
    /// <summary>
    /// The variable task collection.
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "Tasks", Namespace = "", IsNullable = false)]
    public class TaskCollectionV
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlArrayItem("TaskItem")]
        public List<TaskItemV> Items { get; set; }
    }
}
