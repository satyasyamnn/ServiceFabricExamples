using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Game
{
    [DataContract]
    public class ActorState
    {
        [DataMember]
        public int[] Board { get; set; }
        [DataMember]
        public string Winner { get; set; }
        [DataMember]
        public IList<Tuple<long, string>> Players { get; set; }
        [DataMember]
        public int NextPlayerIndex { get; set; }
        [DataMember]
        public int NumberOfMoves { get; set; }
    }
}
