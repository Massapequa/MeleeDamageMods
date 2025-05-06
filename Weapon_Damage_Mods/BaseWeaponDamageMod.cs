using Server.Achievements;
using Server.Misc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Server.Items
{
    public class WeaponDamageMod
    {
        public static string FilePath = Path.Combine("Saves/Customs", "WeaponMods.bin");

        private static Dictionary<BaseWeapon, WeaponDamageMod> ModdedWeapons { get; set; }

        public static void Configure()
        {
            ModdedWeapons = new Dictionary<BaseWeapon, WeaponDamageMod>();

            EventSink.WorldSave += OnSave;
            EventSink.WorldLoad += OnLoad;
        }

        private BaseWeapon m_Weapon;
        private int m_Offset;
        private TimeSpan m_Duration;

        public BaseWeapon Weapon {  get { return m_Weapon; }  set { m_Weapon = value; } }

        public int Offset
        {
            get { return m_Offset; }
            set { m_Offset = value; }
        }

        public TimeSpan Duration { get { return m_Duration; } set { m_Duration = value; } }

        public WeaponDamageMod(BaseWeapon weapon, int offset, TimeSpan delay)
            : this(weapon, offset, delay, false)
        {
        }

        public WeaponDamageMod(BaseWeapon weapon, int offset, TimeSpan delay, bool worldLoad)
        {

            m_Weapon = weapon;
            m_Offset = offset;
            m_Duration = delay;

            if (!worldLoad)
                m_Weapon.Attributes.WeaponDamage += m_Offset;

            WeaponDamageModTimer timer = new WeaponDamageModTimer(weapon, offset, delay);
            ModdedWeapons.Add(weapon, this);
        }

        public static bool CanApplyMod(BaseWeapon weapon)
        {
            return !ModdedWeapons.ContainsKey(weapon);
        }

        public static void ApplyMod(BaseWeapon weapon, int offset, TimeSpan modTime)
        {
            if (CanApplyMod(weapon))
            {
                WeaponDamageMod mod = new WeaponDamageMod(weapon, offset, modTime);
            }
        }

        public static void RemoveMod(BaseWeapon weapon, int offset)
        {
            ModdedWeapons[weapon] = null;
            ModdedWeapons.Remove(weapon);
            weapon.Attributes.WeaponDamage -= offset;
            weapon.InvalidateProperties();
        }

        public static void OnSave(WorldSaveEventArgs e)
        {
            Persistence.Serialize(
                FilePath,
                writer =>
                {
                    writer.Write(1);
                    writer.Write(ModdedWeapons.Count);

                    foreach (var kvp in ModdedWeapons)
                    {
                        writer.Write(kvp.Key);
                        var mod = kvp.Value;

                        writer.Write(mod.Offset);
                        writer.Write(mod.Duration);

                    }
                });
        }

        public static void OnLoad()
        {
            Utility.WriteConsoleColor(ConsoleColor.Green, "Weapon Mod Data Loading...");

            Persistence.Deserialize(
                FilePath,
                reader =>
                {
                    int version = reader.ReadInt(); // version

                    int count = reader.ReadInt();

                    for (int i = 0; i < count; i++)
                    {
                        var weapon = reader.ReadItem() as BaseWeapon;
                        var offset = reader.ReadInt();
                        var duration = reader.ReadTimeSpan();
                        if (weapon != null || !weapon.Deleted)
                        {
                            WeaponDamageMod mod = new WeaponDamageMod(weapon, offset, duration, true);
                        }
                    }
                });

            Utility.WriteConsoleColor(ConsoleColor.Green, "Weapon Mod Loading Complete.");
        }
    }

    public class WeaponDamageModTimer : Timer
    {
        private BaseWeapon Weapon;
        private int Offset;

        public WeaponDamageModTimer(BaseWeapon weapon, int offset, TimeSpan delay)
            : base(delay)
        {
            Weapon = weapon;
            Offset = offset;
            Start();
        }

        protected override void OnTick()
        {
            if (Weapon != null)
            {
                WeaponDamageMod.RemoveMod(Weapon, Offset);
            }

            Stop();
        }
    }


}
