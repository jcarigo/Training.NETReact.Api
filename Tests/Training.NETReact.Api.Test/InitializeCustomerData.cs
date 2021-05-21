using System;
using System.Collections.Generic;
using Training.NETReact.Application.Dtos;
using Training.NETReact.Domain.ENUM;
using Training.NETReact.Domain.Models;

namespace Training.NETReact.Api.Test
{
    public class InitializeCustomerData
    {
        public List<Customer> CustomerData()
        {
            return new List<Customer>
            {
                new Customer {Id= 1, FirstName="Juan",LastName="Dela Cruz",Email="jdcruz@hotmail.com",BirthDate=DateTime.Parse("1970-03-14"),Gender=Gender.Male},
                new Customer {Id= 2,FirstName="Maria Claria",LastName="De Los Santos",Email="ma_clara@hotmail.com",BirthDate=DateTime.Parse("1990-09-01"),Gender=Gender.Female},
                new Customer {Id= 3,FirstName="Padre",LastName="Damaso",Email="verdolagas@gmail.com",BirthDate=DateTime.Parse("1960-06-01"),Gender=Gender.NotSpecified},
            };
        }
    }
}
