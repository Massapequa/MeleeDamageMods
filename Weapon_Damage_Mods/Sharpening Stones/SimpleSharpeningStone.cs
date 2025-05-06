using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items
{
    public class SimpleSharpeningStone : BaseSharpeningStone
    {
        [Constructable]
        public SimpleSharpeningStone()
            : this(1)
        {
        }

        [Constructable]
        public SimpleSharpeningStone(int amount)
        {
            Name = "Simple Sharpening Stone";
            Amount = amount;
            Offset = 5;
            Duration = TimeSpan.FromMinutes(30);
        }

        public SimpleSharpeningStone(Serial serial)
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
