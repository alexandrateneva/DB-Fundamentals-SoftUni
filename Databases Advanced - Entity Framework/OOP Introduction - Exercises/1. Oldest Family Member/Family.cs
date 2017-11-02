namespace _1.Oldest_Family_Member
{

    using System.Collections.Generic;
    using System.Linq;

    public class Family
    {
        public List<Person> family;

        public Family()
        {
            this.family = new List<Person>();
        }

        public void AddMember(Person member)
        {
            this.family.Add(member);
        }

        public Person GetOldestMember()
        {
            return this.family.OrderByDescending(p => p.Age).First();
        }
    }
}
