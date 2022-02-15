using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DemoMvcProject.DAL;
using DemoMvcProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoMvcProject.Models.ViewModels
{
    public class IndexView
    {
        public StudentSearch Search { get; set; }
        public List<StudentModel> students { get; set; }

        public static async Task<IndexView> GetViewInstance(SchoolContext _context)
        {

            IndexView indexView = new IndexView();
            indexView.Search = null;
            indexView.students = await _context.Students.Take(100).ToListAsync();

            return indexView;
        }

        public static async Task<IndexView> GetInstance(SchoolContext _context, StudentSearch search)
        {
            // var result1 = await _context.Students.Where(c =>
            //             search.FirstName == null || c.FirstName.Contains(search.FirstName) &&
            //             search.LastName == null || c.LastName.Contains(search.LastName) &&
            //             search.Email == null || c.Email.Contains(search.Email) &&
            //             search.Phone == null || c.Phone.Contains(search.Phone)
            //             ).Take(100).ToListAsync();

            var result = await _context.Students.Where(c => String.IsNullOrEmpty(search.FirstName) || c.FirstName.ToLower().Contains(search.FirstName.ToLower()))
                                               .Where(c => String.IsNullOrEmpty(search.LastName) || c.LastName.ToLower().Contains(search.LastName.ToLower()))
                                               .Where(c => String.IsNullOrEmpty(search.Email) || c.Email.ToLower().Contains(search.Email.ToLower()) ) 
                                               .Where(c => String.IsNullOrEmpty(search.Phone) || c.Phone.Contains(search.Phone) )
                                               .Where(c => (search.DOB == null) || search.DOB == DateTime.MinValue || c.DOB == search.DOB  )
                                               .OrderBy( c => c.FirstName)
                                               .Take(100).ToListAsync();
                                               

            IndexView indexView = new IndexView();
            indexView.Search = search;
            indexView.students = result;

            return indexView;
        }
    }
}


//COde first

//Entity Frame Work