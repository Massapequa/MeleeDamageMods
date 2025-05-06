using System;
using Server.Targeting;

namespace Server.Items
{
    public class BaseWeightStone : Item
    {
        private int m_Offset;
        private TimeSpan m_Duration;

        public int Offset
        {
            get { return m_Offset; }
            set { m_Offset = value; }
        }

        public TimeSpan Duration
        {
            get { return m_Duration; }
            set { m_Duration = value; }
        }

        public BaseWeightStone()
            : base(0x0F7F) //0xB240)
        {
            Name = "Weight Stone";
            Weight = 2.0;
            Hue = 2101;
            Stackable = true;
        }

        public BaseWeightStone(Serial serial)
            : base(serial) { }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            string power = $"Bonus: {m_Offset}";
            list.Add(power);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from == null || !from.Alive || from.Backpack == null)
                return;

            // Ensure the item is in the backpack
            if (!IsChildOf(from.Backpack))
            {
                from.SendMessage("This item must be in your backpack to use.");
                return;
            }

            from.Target = new InternalTarget(this);
            from.SendMessage("WHat would you like to use this item on?");
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version

            writer.Write(m_Offset);
            writer.Write(m_Duration);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            m_Offset = reader.ReadInt();
            m_Duration = reader.ReadTimeSpan();
        }

        public static bool IsBluntWeapon(BaseMeleeWeapon weapon)
        {
            return (weapon is BaseBashing || weapon is BaseStaff);
        }

        public static void AppyMod(BaseWeapon weapon, BaseWeightStone stone)
        {
            if (weapon == null || stone == null || weapon.Deleted || stone.Deleted || !WeaponDamageMod.CanApplyMod(weapon))
                return;

            WeaponDamageMod.ApplyMod(weapon, stone.Offset, stone.Duration);

            if (stone.Amount > 1)
                stone.Amount--;
            else
                stone.Delete();
        }

        private class InternalTarget : Target
        {
            private BaseWeightStone Stone;
            public InternalTarget(BaseWeightStone stone)
                : base(1, false, TargetFlags.None)
            {
                Stone = stone;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (from == null || !from.Alive || from.Backpack == null)
                    return;

                if (targeted is BaseMeleeWeapon)
                {
                    BaseMeleeWeapon weap = targeted as BaseMeleeWeapon;

                    // Ensure the item is in the backpack
                    if (!weap.IsChildOf(from.Backpack))
                    {
                        from.SendMessage("This item must be in your backpack to use.");
                        return;
                    }

                    if (IsBluntWeapon(weap))
                    {
                        from.PlaySound(0x065A);
                        BaseWeightStone.AppyMod(weap, Stone);
                        from.SendMessage("You secure the weight stone to your weapon!");
                    }
                    else
                    {
                        from.SendMessage("That only works on blunt weapons.");
                    }

                }
            }
        }
    }
}
