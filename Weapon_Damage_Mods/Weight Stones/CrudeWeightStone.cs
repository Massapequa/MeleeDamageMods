using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items
{
    public class CrudeWeightStone : BaseWeightStone
    {
        [Constructable]
        public CrudeWeightStone()
        {
        }

        [Constructable]
        public CrudeWeightStone(int amount)
        {
            Name = "Crude Weight Stone";
            Amount = amount;
            Offset = 3;
            Duration = TimeSpan.FromMinutes(30);
        }

        public CrudeWeightStone(Serial serial)
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
