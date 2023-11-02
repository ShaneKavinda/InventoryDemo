using System;  
using System.Collections.Generic;  
using System.Diagnostics;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using MVCInventoryDemo.Models;

namespace MVCInventoryDemo{
    public class CustomerController : Controller{
        CustomerDataAccessLayer objCust = new CustomerDataAccessLayer();
        public IActionResult Index(){              
            List<Customer> lstCustomer = new List<Customer>();              
            lstCustomer = objCust.GetAllCustomers().ToList();                
            return View(lstCustomer);          
        } 


        // Create a new Customer
        [HttpGet]
        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Customer customer){
            if (ModelState.IsValid){
                objCust.AddCustomer(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // Edit an existing Customer
        [HttpGet]
        public IActionResult Edit(Guid id){
            if (id == Guid.Empty){
                return NotFound();
            }
            Customer customer = objCust.GetCustomerData(id);
            if (customer == null){
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind] Customer customer){
            if (id != customer.UserID){
                return NotFound();
            }
            if (ModelState.IsValid){
                objCust.UpdateCustomer(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // Get Customer Details 
        [HttpGet]
        public IActionResult Details(Guid id){
            if (id == null){
                return NotFound();
            }
            Customer customer = objCust.GetCustomerData(id);
            if (customer == null){
                return NotFound();
            }
            return View(customer);  
        }

        // Delete a customer
        [HttpGet]
        public IActionResult Delete(Guid id){
            if (id == null){
                return NotFound();
            }
            Customer customer = objCust.GetCustomerData(id);
            if (customer == null){
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id){
            objCust.DeleteCustomer(id);
            return RedirectToAction("Index");
        }
    }
}