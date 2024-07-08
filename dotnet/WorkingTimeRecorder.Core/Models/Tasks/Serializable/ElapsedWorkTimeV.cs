using System.Xml.Serialization;

namespace WorkingTimeRecorder.Core.Models.Tasks.Serializable
{
    /// <summary>
    /// The variable elapsed work time
    /// </summary>
    [Serializable]
    public class ElapsedWorkTimeV
    {
        [XmlAttribute("Hours")]
        public uint Hours { get; set; }

        [XmlAttribute("Minutes")]
        public uint Miniutes { get; set; }
    }
}
