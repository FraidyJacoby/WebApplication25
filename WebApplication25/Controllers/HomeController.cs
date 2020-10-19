using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication25.Models;

namespace WebApplication25.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=ToDoItems;Integrated Security=True;";

        public IActionResult Index()
        {
            ToDoItemsViewModel vm = new ToDoItemsViewModel(_connectionString);
            ToDoItemDb db = new ToDoItemDb(_connectionString);
            vm.Items = db.GetAllNonCompletedItems();
            return View(vm);
        }

        [HttpPost]
        public IActionResult MarkComplete(int id)
        {
            ToDoItemDb db = new ToDoItemDb(_connectionString);
            db.MarkComplete(id);
            return Redirect("/home/index"); 
        }

        public IActionResult Completed()
        {
            ToDoItemsViewModel vm = new ToDoItemsViewModel(_connectionString);
            ToDoItemDb db = new ToDoItemDb(_connectionString);
            vm.Items = db.GetAllCompletedItems();
            return View(vm);
        }

        public IActionResult Categories()
        {
            ToDoItemDb db = new ToDoItemDb(_connectionString);
            return View(db.GetAllCategories());
        }

        public IActionResult EditCategoryForm(int id)
        {
            ToDoItemDb db = new ToDoItemDb(_connectionString);
            return View(db.GetCategoryById(id));
        }

        [HttpPost]
        public IActionResult EditCategory(int id, string name)
        {
            ToDoItemDb db = new ToDoItemDb(_connectionString);
            db.EditCategory(id, name);
            return Redirect("/home/categories");
        }

        public IActionResult AddCategoryForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(string name)
        {
            ToDoItemDb db = new ToDoItemDb(_connectionString);
            db.AddCategory(name);
            return Redirect("/home/categories");
        }

        public IActionResult AddItemPage()
        {
            ToDoItemDb db = new ToDoItemDb(_connectionString);
            return View(db.GetAllCategories());
        }

        [HttpPost]
        public IActionResult AddItem(string title, DateTime dueDate, int categoryId)
        {
            ToDoItemDb db = new ToDoItemDb(_connectionString);
            db.AddToDoItem(title, dueDate, categoryId);
            return Redirect("/home/index");
        }

        public IActionResult ItemsForCategory(int categoryId)
        {
            ToDoItemDb db = new ToDoItemDb(_connectionString);
            ToDoItemsViewModel vm = new ToDoItemsViewModel(_connectionString);
            vm.Items = db.GetItemsForCategory(categoryId);
            return View(vm);
        }

    }
}
