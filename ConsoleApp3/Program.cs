using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public List<string>Languages { get; set; }
        public User()
        {
            Languages = new List<string>();
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"Name:{Name},Age:{Age}");
            foreach(var item in Languages)
            {
                builder.Append(item);
            }
            return builder.ToString();
        }

    }
    class Phone
    {
        public string Name{ get; set; }
        public string Company{ get; set; }

    }
    class Program
    {
        static void Main(string[] args)
        {
            string[] teams = { "Барселона","Бавария","Манчестер Юнайтед","Реал Мадрид","Ювентус","Порту","Тоттенхэм"};
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 20, 30, 40 };
            List<User> users = new List<User>
            {
                new User{ Name="Cristiano",Age=32,Languages = new List<string>{ "Испанский","Итальянский"} },
                 new User{ Name="Lionel",Age=31,Languages = new List<string>{ "Испанский","Болгарский"} },
                  new User{ Name="Garett",Age=28,Languages = new List<string>{ "Испанский","Английский"} },
                   new User{ Name="Karim",Age=29,Languages = new List<string>{ "Испанский","Фрацузкий"} },
                    new User{ Name="Andrei",Age=27,Languages = new List<string>{ "Английский","Украинский"} },
            };

            List<Phone> phones = new List<Phone>
            {
                new Phone { Name = "Lumia 630",Company="Microsoft"},
                new Phone { Name = "iPhone XS max 512gb 2000$",Company="Apple"},
            };
            // простой linq метод
            linqMethod(teams);
            //---------
            filterLinq(nums);
            //---------
            queryObject(users);
            //---------
            selectQuery(users);
            //-----------
            unionCollection(users, phones);
            //----------
            SkipAndWhileMethods(nums,teams);
        }

        private static void SkipAndWhileMethods(int[] nums,string[] teams)
        {
            Console.WriteLine("________");
            var result = nums.Take(5);
            result = nums.Skip(5);
            result = nums.Skip(4).Take(2);
            foreach(var item in result)
            {
                Console.WriteLine(item);
            }

            foreach(var item in teams.SkipWhile(x=>x.StartsWith("Б")))
            {
                Console.WriteLine(item);
            }
        }

        private static void unionCollection(List<User> users, List<Phone> phones)
        {
            var people = from user in users
                         from phone in phones
                         select new { Name = user.Name, phone = phone.Name };
            foreach (var item in people)
            {
                Console.WriteLine(item);
            }

            var result = users.Select(u=>u.Name).Union(phones.Select(p=>p.Name));
            foreach(var item in result)
            {
                Console.WriteLine(item);
            }
        }

        private static void selectQuery(List<User> users)
        {
            var names = from u in users select u.Name;
            names = users.Select(u => u.Name);
            var items = from u in users
                        select new
                        {
                            firstName = u.Name,
                            newAge = u.Age
                        };

            var items2 = users.Select(u=>new { firstName = u.Name,newAge = u.Age});

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        private static void queryObject(List<User> users)
        {
            var selectedUsers = from user in users
                                where user.Age > 25
                                select user;
            foreach (var item in users)
            {
                Console.WriteLine(item.Age + " " + item.Name);
            }
            var selectedHardUsers = from user in users
                                    from lang in user.Languages
                                    where user.Age > 25
                                    where lang == "Испанский"
                                    select user;
            selectedHardUsers = users.SelectMany(u => u.Languages,
                (u, l) => new { User = u, Lang = l })
                .Where(u => u.Lang == "Испанский" && u.User.Age > 25)
                .Select(u => u.User);
            PrintArray(selectedHardUsers);
        }

        private static void PrintArray(IEnumerable<User> selectedHardUsers)
        {
            Console.WriteLine("----------");
           foreach (var item in selectedHardUsers)
            {
                Console.WriteLine(item);
            }
        }

        private static void filterLinq(int[] nums)
        {
            IEnumerable<int> evens = from i in nums
                                     where i % 2 == 0 && i > 10
                                     select i;
            evens = nums.Where(i => i % 2 == 0 && i > 10);//метод
            foreach (var items in evens)
            {
                Console.WriteLine(items);
            }
        }

        private static void PrintArray(IOrderedEnumerable<string> selectedTeams)
        {
            foreach (var item in selectedTeams)
            {
                Console.WriteLine(item);
            }
        }
        private static void linqMethod(string [] teams)
        {
            //определяем каждый обьект из teams как t
            var selectedTeams = from t in teams
                                where t.ToUpper().StartsWith("Б")//фильтрация по критерию
                                orderby t//упорядочим по возрастанию
                                select t;
            PrintArray(selectedTeams);
            //разбираем в строку
            var selectedString = "Hello,World".Where(h => h == 'W');
           foreach (var item in selectedString)
            {
                Console.WriteLine(item);
            }
        }  
    }
}
