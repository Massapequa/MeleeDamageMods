using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Items
{
    public class SuperiorWeightStone : BaseWeightStone
    {
        [Constructable]
        public SuperiorWeightStone()
            : this(1)
        {
        }

        [Constructable]
        public SuperiorWeightStone(int amount)
        {
            Name = "Superior Weight Stone";
            Amount = amount;
            Offset = 10;
            Duration = TimeSpan.FromMinutes(60);
        }

        public SuperiorWeightStone(Serial serial)
            : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
