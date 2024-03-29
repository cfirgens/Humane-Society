﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query 
    {        
        static HumaneSocietyDataContext db;

        static Query()
        {
            db = new HumaneSocietyDataContext();
        }

        internal static List<USState> GetStates()
        {
            List<USState> allStates = db.USStates.ToList();       

            return allStates;
        }
            
        internal static Client GetClient(string userName, string password)
        {
            Client client = db.Clients.Where(c => c.UserName == userName && c.Password == password).Single();

            return client;
        }

        internal static List<Client> GetClients()
        {
            List<Client> allClients = db.Clients.ToList();

            return allClients;
        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int stateId)
        {
            Client newClient = new Client();

            newClient.FirstName = firstName;
            newClient.LastName = lastName;
            newClient.UserName = username;
            newClient.Password = password;
            newClient.Email = email;

            Address addressFromDb = db.Addresses.Where(a => a.AddressLine1 == streetAddress && a.Zipcode == zipCode && a.USStateId == stateId).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if (addressFromDb == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = streetAddress;
                newAddress.City = null;
                newAddress.USStateId = stateId;
                newAddress.Zipcode = zipCode;                

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                addressFromDb = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            newClient.AddressId = addressFromDb.AddressId;

            db.Clients.InsertOnSubmit(newClient);

            db.SubmitChanges();
        }

        internal static void UpdateClient(Client clientWithUpdates)
        {
            // find corresponding Client from Db
            Client clientFromDb = null;

            try
            {
                clientFromDb = db.Clients.Where(c => c.ClientId == clientWithUpdates.ClientId).Single();
            }
            catch(InvalidOperationException e)
            {
                Console.WriteLine("No clients have a ClientId that matches the Client passed in.");
                Console.WriteLine("No update have been made.");
                return;
            }
            
            // update clientFromDb information with the values on clientWithUpdates (aside from address)
            clientFromDb.FirstName = clientWithUpdates.FirstName;
            clientFromDb.LastName = clientWithUpdates.LastName;
            clientFromDb.UserName = clientWithUpdates.UserName;
            clientFromDb.Password = clientWithUpdates.Password;
            clientFromDb.Email = clientWithUpdates.Email;

            // get address object from clientWithUpdates
            Address clientAddress = clientWithUpdates.Address;

            // look for existing Address in Db (null will be returned if the address isn't already in the Db
            Address updatedAddress = db.Addresses.Where(a => a.AddressLine1 == clientAddress.AddressLine1 && a.USStateId == clientAddress.USStateId && a.Zipcode == clientAddress.Zipcode).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if(updatedAddress == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = clientAddress.AddressLine1;
                newAddress.City = null;
                newAddress.USStateId = clientAddress.USStateId;
                newAddress.Zipcode = clientAddress.Zipcode;                

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                updatedAddress = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            clientFromDb.AddressId = updatedAddress.AddressId;
            
            // submit changes
            db.SubmitChanges();
        }
        
        internal static void AddUsernameAndPassword(Employee employee)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();

            employeeFromDb.UserName = employee.UserName;
            employeeFromDb.Password = employee.Password;

            db.SubmitChanges();
        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber).FirstOrDefault();

            if (employeeFromDb == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return employeeFromDb;
            }
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.UserName == userName && e.Password == password).FirstOrDefault();

            return employeeFromDb;
        }

        internal static bool CheckEmployeeUserNameExist(string userName)
        {
            Employee employeeWithUserName = db.Employees.Where(e => e.UserName == userName).FirstOrDefault();

            return employeeWithUserName == null;
        }


        //// TODO Items: ////
        
        // TODO: Allow any of the CRUD operations to occur here
        internal static void RunEmployeeQueries(Employee employee, string crudOperation)
        {
            switch (crudOperation)
            {
                case "create":
                    CreateNewEmployee(employee);
                    break;

                case "read":
                    ReadEmployee(employee);
                    break;

                case "update":
                    UpdateEmployee(employee);
                    break;

                case "delete":
                    DeleteEmployee(employee);
                    break;

                default:
                    UserInterface.DisplayUserOptions("Input was not recognized, please try again");
                    break;
            }
        }

        internal static void CreateNewEmployee(Employee employee)
        {
            bool newEmployee = db.Employees.Where(e => e.FirstName == employee.FirstName && e.LastName == employee.LastName).Any();
            if (!newEmployee)
            {
                db.Employees.InsertOnSubmit(employee);
                db.SubmitChanges();
            }
            else
            {
                UserInterface.DisplayUserOptions("Employee already exists");
            }
        }

        internal static void ReadEmployee(Employee employee)
        {
            Employee fetchedEmployee = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();
            Console.WriteLine("Name: "+ employee.FirstName + " " + employee.LastName + "Email: " + employee.Email + "Employee Number: " + employee.EmployeeNumber);
        }

        internal static void UpdateEmployee(Employee employee)
        {
            Employee updateEmployee = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();

            updateEmployee.FirstName = employee.FirstName;
            updateEmployee.LastName = employee.LastName;
            updateEmployee.Email = employee.Email;
            updateEmployee.EmployeeNumber = employee.EmployeeNumber;

            db.SubmitChanges();
        }

        internal static void DeleteEmployee(Employee employee)
        {
            Employee employeeToDelete = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();
            db.Employees.DeleteOnSubmit(employeeToDelete);
            db.SubmitChanges();

        }

        // TODO: Animal CRUD Operations
        internal static void AddAnimal(Animal animal) 
        {
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
        }

        internal static Animal GetAnimalByID(int id)
        {
            Animal animalFromDB = db.Animals.Where(a => a.AnimalId == id).Single();
            return animalFromDB;
        }

        internal static void UpdateAnimal(int animalId, Dictionary<int, string> updates)
        {
            var animalInDb = db.Animals.Where(a => a.AnimalId == animalId).Single();            
            foreach (KeyValuePair<int, string> update in updates)
            {
                switch (update.Key)
                {
                    case 1:
                        animalInDb.CategoryId = int.Parse(update.Value);
                        break;

                    case 2:
                        animalInDb.Name = update.Value;
                        break;

                    case 3:
                        animalInDb.Age = int.Parse(update.Value);
                        break;

                    case 4:
                        animalInDb.Demeanor = update.Value;
                        break;

                    case 5:

                        animalInDb.KidFriendly = bool.Parse(update.Value);
                        break;

                    case 6:

                        animalInDb.PetFriendly = bool.Parse(update.Value);

                        break;

                    case 7:
                        animalInDb.Weight = int.Parse(update.Value);
                        break;

                    default:
                        UserInterface.DisplayUserOptions("Input was not recognized, please try again");
                        break;
                }
            }

            try
            {
                
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("No animals have a AnimalId that matches the Animal passed in.");
                Console.WriteLine("No update have been made.");
                return;
            }
        }

        internal static void RemoveAnimal(Animal animal)
        {
            Animal animalToDelete = db.Animals.Where(a => a.AnimalId == animal.AnimalId).FirstOrDefault();
            db.Animals.DeleteOnSubmit(animal);
            db.SubmitChanges();
        }
        
        // TODO: Animal Multi-Trait Search
        internal static IQueryable<Animal> SearchForAnimalsByMultipleTraits(Dictionary<int, string> updates)
        { 
            IQueryable<Animal> animals = db.Animals;         
            foreach (KeyValuePair<int, string> update in updates)
            {
                switch (update.Key)
                {
                    case 1:
                        animals = animals.Where(a => a.CategoryId == int.Parse(update.Value));
                        break;
                    case 2:
                        animals = animals.Where(a => a.Name == update.Value);
                        break;
                    case 3:
                        animals = animals.Where(a => a.Age == int.Parse(update.Value));
                        break;
                    case 4:
                        animals = animals.Where(a => a.Demeanor == update.Value);
                        break;
                    case 5:
                        animals = animals.Where(a => a.KidFriendly == bool.Parse(update.Value));
                        break;
                    case 6:
                        animals = animals.Where(a => a.PetFriendly == bool.Parse(update.Value));
                        break;
                    case 7:
                        animals = animals.Where(a => a.Weight == int.Parse(update.Value));
                        break;
                    case 8:
                        animals = animals.Where(a => a.AnimalId == int.Parse(update.Value));
                        break;
                    case 9:
                        break;
                }
            }
            return animals;
        }
        
         
        // TODO: Misc Animal Things
        internal static int GetCategoryId(string categoryName)
        {
            Category category = db.Categories.Where(c => c.Name == categoryName).First();
            return category.CategoryId;
        }
        
        internal static Room GetRoom(int animalId)
        {
           Room room = db.Rooms.Where(r => r.AnimalId == animalId).FirstOrDefault();
           return room;            
        }
        
        internal static int GetDietPlanId(string dietPlanName)
        {
            DietPlan dietPlans = db.DietPlans.Where(d => d.Name == dietPlanName).FirstOrDefault();
            return dietPlans.DietPlanId;
        }

        // TODO: Adoption CRUD Operations
        internal static void Adopt(Animal animal, Client client)
        {
            Adoption adopted = new Adoption();
            adopted.AnimalId = animal.AnimalId;
            adopted.ClientId = client.ClientId;
            adopted.ApprovalStatus = "Pending";
            adopted.AdoptionFee = 100;
            adopted.PaymentCollected = false;
            Console.WriteLine("Your adoption is pending.");
            Console.ReadLine();
            db.Adoptions.InsertOnSubmit(adopted);
            db.SubmitChanges();
        }

        internal static IQueryable<Adoption> GetPendingAdoptions()
        {
            IQueryable<Adoption> pendingAdoptions = db.Adoptions.Where(a => a.ApprovalStatus == "Pending");
            return pendingAdoptions;
        }

        internal static void UpdateAdoption(bool isAdopted, Adoption adoption)
        {
            {
                if (isAdopted == true)
                {
                    adoption.ApprovalStatus = "Approved";

                }
                else
                {
                    adoption.ApprovalStatus = "Pending";
                }
                db.SubmitChanges();
            }
        }

        internal static void RemoveAdoption(int animalId, int clientId)
        {
            Adoption adopted = db.Adoptions.Where(a => a.AnimalId == animalId && a.ClientId == clientId).First();
            db.Adoptions.DeleteOnSubmit(adopted);
            db.SubmitChanges();
        }

        // TODO: Shots Stuff
        internal static IQueryable<AnimalShot> GetShots(Animal animal)
        {
            IQueryable<AnimalShot> animalShots = db.AnimalShots.Where(a => a.AnimalId == animal.AnimalId);
            return animalShots;
        }

        internal static void UpdateShot(string shotName, Animal animal)
        {
            var newShot = db.Shots.Where(s => s.Name == shotName).First();
            AnimalShot animalShot = new AnimalShot();

            animalShot.AnimalId = animal.AnimalId;
            animalShot.ShotId = newShot.ShotId;
            animalShot.DateReceived = DateTime.Today;

            db.AnimalShots.InsertOnSubmit(animalShot);
            db.SubmitChanges();
        }
    }
}