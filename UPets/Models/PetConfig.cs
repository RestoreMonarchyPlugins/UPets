using System.Xml.Serialization;

namespace RestoreMonarchy.UPets.Models
{
    public class PetConfig
    {
        [XmlAttribute]
        public ushort Id { get; set; }
        [XmlAttribute]
        public string Name { get; set; }        
        [XmlAttribute]
        public decimal Cost { get; set; }
        [XmlAttribute]
        public string Permission { get; set; }

        public PetConfig(ushort id, string name, decimal cost, string permission)
        {            
            Id = id;
            Name = name;
            Cost = cost;
            Permission = permission;
        }

        public PetConfig() { }
    }
}
