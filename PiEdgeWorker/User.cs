using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;

namespace PiEdgeWorker
{
    //Attribute descriptio
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class AgeRangeAttribute : Attribute
    { 
        public int Min { get; } = 0;
        public int Max { get; } = 120;

        public bool IsValid(int age) => age >= Min && age <= Max;

    }

    //Reflection engine
    public static class UserModelValidator
    {
        public static bool TryValidate(object instance, out List<string> errors)
        {
            errors  = [];

            Type type = instance.GetType();
            foreach (PropertyInfo prop in type.GetProperties())
            { 
                if (prop is null) continue;

                var customAttribute = prop.GetCustomAttribute<AgeRangeAttribute>();
                if (customAttribute is null) continue;

                if (prop.GetValue(instance) is int ageValue)
                {
                    if (customAttribute.IsValid(ageValue)) 
                        return true;

                    var error = $"Property '{prop.Name}' has value {ageValue}, which is outside the allowed range ({customAttribute.Min}..{customAttribute.Max}).";
                    errors.Add(error);
                }
                else {
                    errors.Add($"Property {prop.Name} has attribute {nameof(customAttribute)} set to non Int type");
                }
            }

            return false;
        }
    }

    // Class with attribute
    [Table("users")]
    internal class User
    {
        [Key]
        [Column("user_id")]
        public int Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
        [AgeRangeAttribute] //Custom attribute
        public int Age { get; set; }
    }
}
