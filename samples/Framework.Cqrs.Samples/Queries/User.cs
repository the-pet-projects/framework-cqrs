namespace Framework.Cqrs.Samples.Queries
{
    public class User
    {
        public User(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}