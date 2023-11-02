using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ReDoProject.Domain.Enums
{
    public enum InstrumentType
    {
        [Description("Acoustic Guitar")]
        AcousticGuitar,

        [Description("Electric Guitar")]
        ElectricGuitar,

        [Description("Bass Guitar")]
        BassGuitar,

        [Description("Acoustic Piano")]
        AcousticPiano,

        [Description("Digital Piano")]
        DigitalPiano,

        [Description("Violin")]
        Violin,

        [Description("Drum Set")]
        DrumSet,

        [Description("Trumpet")]
        Trumpet,

        [Description("Saxophone")]
        Saxophone,

        [Description("Flute")]
        Flute,

        [Description("Clarinet")]
        Clarinet,

        [Description("Other")]
        Other
    }

}



public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());

        DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();
        if (attribute != null)
        {
            return attribute.Description;
        }

        DisplayAttribute displayAttribute = field.GetCustomAttribute<DisplayAttribute>();
        if (displayAttribute != null)
        {
            return displayAttribute.Name;
        }

        return value.ToString();
    }
}
