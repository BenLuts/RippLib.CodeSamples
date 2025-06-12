using System.Reflection;

namespace SmartEnums
{
    public abstract class Enumeration : IComparable, IEnumeration
    {
        public string Name { get; private set; }

        public int Id { get; }

        public string Title => GetType().Name;

        protected Enumeration()
        { }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            if (obj is not Enumeration otherValue)
                return false;

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object obj) =>
            obj is Enumeration other ? Id.CompareTo(other.Id) : 1;

        public static bool operator ==(Enumeration a, Enumeration b)
        {
            if (a is null)
            {
                return b is null;
            }
            return a.Equals(b);
        }
        public static bool operator >(Enumeration a, Enumeration b)
        {
            if (a is null)
                return false;
            return a.CompareTo(b) > 0;
        }
        public static bool operator <(Enumeration a, Enumeration b)
        {
            if (a is null)
                return false;
            return a.CompareTo(b) < 0;
        }
        public static bool operator !=(Enumeration a, Enumeration b)
        {
            return !(a == b);
        }

        public static bool operator <=(Enumeration a, Enumeration b)
        {
            return a is null || a.CompareTo(b) <= 0;
        }

        public static bool operator >=(Enumeration a, Enumeration b)
        {
            return a is null ? b is null : a.CompareTo(b) >= 0;
        }

        public static bool Exists<T>(Func<T, bool> predicate) where T : Enumeration
        {
            return GetAll<T>().Any(predicate);
        }

        public static T Find<T>(Func<T, bool> predicate) where T : Enumeration
        {
            return GetAll<T>().FirstOrDefault(predicate);
        }

        public static T FindById<T>(int? id) where T : Enumeration
        {
            return id is null ? null : Find<T>(e => e.Id == id);
        }

        public static T FindByName<T>(string name) where T : Enumeration
        {
            return Find<T>(e => string.Equals(e.Name, name, StringComparison.InvariantCultureIgnoreCase));
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
