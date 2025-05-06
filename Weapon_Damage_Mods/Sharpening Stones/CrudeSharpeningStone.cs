using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Items
{
    public class CrudeSharpeningStone : BaseSharpeningStone
    {
        [Constructable]
        public CrudeSharpeningStone()
            : this(1)
        {
        }

        [Constructable]
        public CrudeSharpeningStone(int amount)
        {
            Name = "Crude Sharpening Stone";
            Amount = amount;
            Offset = 3;
            Duration = TimeSpan.FromMinutes(30);
        }

        public CrudeSharpeningStone(Serial serial)
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
