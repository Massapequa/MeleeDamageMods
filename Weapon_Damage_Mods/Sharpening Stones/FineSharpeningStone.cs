using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items
{
    public class FineSharpeningStone : BaseSharpeningStone
    {
        [Constructable]
        public FineSharpeningStone()
            : this(1)
        {
        }

        [Constructable]
        public FineSharpeningStone(int amount)
        {
            Name = "Fine Sharpening Stone";
            Amount = amount;
            Offset = 8;
            Duration = TimeSpan.FromMinutes(45);
        }

        public FineSharpeningStone(Serial serial)
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
