﻿using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        public string Position { get; set; }
        
        public double Salary { get; set; }

        public Employee(int id, string name, string position, double salary)
        {
            Id = id;
            Name = name;
            Position = position;
            Salary = salary;
        }
    }
}
