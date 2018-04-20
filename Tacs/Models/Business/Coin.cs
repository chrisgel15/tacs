using System.Runtime.Serialization;

namespace Tacs.Models
{
    [DataContract]
    public class Coin
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        public Coin(string name)
        {
            this.Name = name;
        }

        public Coin(string name, int Id)
        {
            this.Name = name;
            this.Id = Id;
        }
    }
}