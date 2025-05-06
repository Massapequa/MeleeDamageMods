using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items
{
    public class FineWeightStone : BaseWeightStone
    {
        [Constructable]
        public FineWeightStone()
        {
        }

        [Constructable]
        public FineWeightStone(int amount)
        {
            Name = "Fine Weight Stone";
            Amount = amount;
            Offset = 8;
            Duration = TimeSpan.FromMinutes(45);
        }

        public FineWeightStone(Serial serial)
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
