using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Items
{
    public class SuperiorSharpeningStone : BaseSharpeningStone
    {
        [Constructable]
        SuperiorSharpeningStone()
            : this(1)
        {
        }

        [Constructable]
        public SuperiorSharpeningStone(int amount)
        {
            Name = "Superior Sharpening Stone";
            Amount = amount;
            Offset = 10;
            Duration = TimeSpan.FromMinutes(60);
        }

        public SuperiorSharpeningStone(Serial serial)
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
