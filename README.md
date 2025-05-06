# Sharpening Stones &amp; Weight Stones: Temporary Melee Damage Mods

## Overview
A simple damage mod system that gives your players a number of consumable items, Sharpening Stones & Weight stones, that proved a temporary damage increase to their melee weapon.
Both types of consumeables come in various quality levels that give increasingly more powerful damage buffs and increased duration. 

## Quality Levels
Crude, Simple, Fine, & Superior.

## Sharpening Stones
Sharpening Stones provide a damage buff to bladed weapons which iclude,
swords, axes, spears, polearms, etc.

## Weight Stones
Weight stones provide a damage buff to blunt weapons which include,
clubs, maces, hammers, staves, etc.

## Modification
The damage bonus and duration of effect are intentionally easy to modify. No code editing is required in the Base Classes, and it's simply a matter of changing teh values in the item's file.
Example:
```
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
```
Simply change the Offset and Duration variables to modify the item or create your own.

## Implementation
The only file modification for this system, which is optional, is the new SBWeaponSmith.cs, which includes the consumables in the vendor's inventory.
If you do not want your weapon smith selling these item,s simply omit the file. The rest of the system can be easily dropped into your custom scrips folder without any hassle.

## Removal
If you want to remove the system from your server, or wipe all existing damage modification, go to Saves > Customs > then delete the WeaponMods.bin file.

