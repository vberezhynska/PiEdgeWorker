using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiEdgeWorker
{
    internal static class PeopleExtention
    {
        public static int Compare(this People sourse, People other)
        {
            if (ReferenceEquals(sourse, other))
                return 0;

            if (sourse is null) return -1;
            if (other is null) return 1;

            int idComparison = sourse.Id.CompareTo(other.Id);
            if (idComparison != 0) return idComparison;

            int classIdComparison = sourse.ClassId.CompareTo(other.ClassId);
            if(classIdComparison != 0) return classIdComparison;

            return sourse.Name.CompareTo(other.Name);
        }
    }
    
    internal class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }

        public override string ToString()
        {
            if (this is null) return String.Empty;

            return $"Name: {Name}, Id: {Id}, ClassId: {ClassId}";
        }
    }
}
