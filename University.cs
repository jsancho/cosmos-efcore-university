using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using CosmosEfCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmosEfCore
{
    public class University
    {
        private readonly UniversityContext _context;

        public University(
            UniversityContext context)
        {
            _context = context;
        }

        public async Task Run()
        {
            var created = await _context.Database.EnsureCreatedAsync();

            if (created)
            {
                await AddContent();
            }

            await QueryCollegePartition(College.Arts);

            await QueryCollegePartition(College.Philosophy);
        }

        private async Task AddContent()
        {
            await _context.People.AddAsync(new Lecturer("Jeff", "Hanneman", ContractType.Permanent, College.Philosophy));
            await _context.People.AddAsync(new Lecturer("Tom", "Araya", ContractType.Permanent, College.Engineering));
            await _context.People.AddAsync(new Administrative("Kerry", "King", ContractType.Permanent, College.Business, "Shredding"));
            await _context.People.AddAsync(new Lecturer("Dave", "Lombardo", ContractType.Temporary, College.Arts));
            await _context.People.AddAsync(new Student("Gary", "Holt", College.Arts, 2014));

            int changed = await _context.SaveChangesAsync();

            Console.WriteLine($"Created {changed} records");

        }

        private async Task QueryCollegePartition(College college)
        {
            var result = await _context
                                .People.Where(p => p.College == college)
                                .ToListAsync();

            foreach (var person in result)
            {
                PrintAsJson(person);
            }
        }

        private void PrintAsJson(Person person)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            options.Converters.Add(new JsonStringEnumConverter());

            string json = JsonSerializer.Serialize(person, person.GetType(), options);

            Console.WriteLine(json);
        }
    }
}
