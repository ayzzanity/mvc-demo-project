﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AyzMVC.Areas.Security.Models;
using AyzMVC.Dal;

namespace AyzMVC.Areas.Security.Controllers
{
    public class UsersController : Controller
    {

        // GET: Security/Users
        public ActionResult Index()
        {
            using (var db = new DatabaseContext())
            {
                var users = (from User in db.Users
                             select new UserViewModel
                             {
                                 id = User.id,
                                 FirstName = User.FirstName,
                                 LastName = User.LastName,
                                 age = User.age,
                                 Gender = User.Gender,
                                 EmploymentDate = User.EmploymentDate
                             }).ToList();

               /* var edu = (from Education in db.Edu
                           select new UserViewModel
                           {
                               UserID = Education.UserID,
                               School = Education.School,
                               YearAttended = Education.YearAttended

                           }).ToList();
                List<UserViewModel> joined = users.Union(edu).ToList();*/

                return View(users);
            }
        }
        
        

        // GET: Security/Users/Details/5
        public ActionResult Details(int Id)
        {
            return View(GetUser(Id));
        }


        // GET: Security/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Security/Users/Create
        [HttpPost]
        public ActionResult Create(UserViewModel viewModel)
        {
             try {

                if (ModelState.IsValid == false)
                    return View();

                using (var db = new DatabaseContext())
                {
                    db.Users.Add(new User
                    {
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName,
                        age = viewModel.age,
                        Gender = viewModel.Gender,
                        EmploymentDate = viewModel.EmploymentDate
                    });

                    db.SaveChanges();
                }
                TempData["CreateSuccess"] = "New user has been added!";
                return RedirectToAction("Index");
            }

            catch
            {
                return View();
            }
        }

        // GET: Security/Users/Edit/5
        public ActionResult Edit(int Id)
        {
            return View(GetUser(Id));
        }

        // POST: Security/Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int Id, UserViewModel viewModel)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    var u = db.Users.FirstOrDefault(us => us.id == Id);

                    if (u != null)
                    {
                        u.FirstName = viewModel.FirstName;
                        u.LastName = viewModel.LastName;
                        u.age = viewModel.age;
                        u.Gender = viewModel.Gender;
                        u.EmploymentDate = viewModel.EmploymentDate;
                        db.SaveChanges();
                    }
                    TempData["EditSuccess"] = "User info has been updated!";
                    return RedirectToAction("Index");
                }
            }

            catch
            {
                return View();
            }
        }

        // GET: Security/Users/Delete/5
        public ActionResult Delete(int Id)
        {
            return View(GetUser(Id));
        }

        // POST: Security/Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int Id, FormCollection collection)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    var u = db.Users.FirstOrDefault(us => us.id == Id);
                    if (u != null)
                    {
                        db.Users.Remove(u);
                        db.SaveChanges();
                    }
                }
                TempData["DeleteSuccess"] = "User record has been deleted.";
                return RedirectToAction("Index");
            }

            catch
            {
                return View();
            }
        }

        private UserViewModel GetUser(int Id)
        {
            using (var db = new DatabaseContext())
            {
                return (from user in db.Users
                        where user.id == Id
                        select new UserViewModel
                        {
                            id = user.id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            age = user.age,
                            Gender = user.Gender,
                            EmploymentDate = user.EmploymentDate
                        }).FirstOrDefault();
            }
        }


    }
}
