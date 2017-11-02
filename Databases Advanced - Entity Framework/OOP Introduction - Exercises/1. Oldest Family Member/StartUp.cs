namespace _1.Oldest_Family_Member
{
    using System;
    using System.Reflection;

    public class StartUp
    {
        public static void Main()
        {
            MethodInfo oldestMemberMethod = typeof(Family).GetMethod("GetOldestMember");
            MethodInfo addMemberMethod = typeof(Family).GetMethod("AddMember");
            if (oldestMemberMethod == null || addMemberMethod == null)
            {
                throw new Exception();
            }

            var n = int.Parse(Console.ReadLine());
            Family family = new Family();
            for (int i = 0; i < n; i++)
            {
                var tokens = Console.ReadLine().Split(' ');
                var name = tokens[0];
                var age = int.Parse(tokens[1]);
                Person currentPerson = new Person(name, age);
                family.AddMember(currentPerson);
            }
            Person oldestMember = family.GetOldestMember();
            Console.WriteLine("{0} {1}", oldestMember.Name, oldestMember.Age);
        }
    }
}
