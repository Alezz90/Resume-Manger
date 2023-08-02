using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ResumeManager.Data;
using ResumeManager.Models;

using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Humanizer;

namespace ResumeManager.Controllers
{
    public class ResumeController : Controller
    {
        private readonly ResumeDbContext _context;

        private readonly IWebHostEnvironment _webHost;




        public ResumeController(ResumeDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;

        }
        public IActionResult Index()
        {
            List<Applicant> applicants;
            applicants = _context.Applicants.ToList();
            return View(applicants);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Applicant applicant = new Applicant();
            applicant.Experiences.Add(new Experience() { ExperienceId = 1 });
           // ViewBag.Gender = getGender();
            return View(applicant);
        }


        [HttpPost]
        public IActionResult Create(Applicant applicant)
        {
            foreach(Experience experience in applicant.Experiences)
            {
                if(experience.CompanyName==null|| experience.CompanyName.Length==0)
                    applicant.Experiences.Remove(experience);
            }
            string uniqueFileName = GetUploadedFileName(applicant);
            applicant.PhotoUrl = uniqueFileName;

            _context.Add(applicant);
            _context.SaveChanges();
            return RedirectToAction("index");

        }


        private string GetUploadedFileName(Applicant applicant)
        {
            string uniqueFileName = null;

            if (applicant.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + applicant.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    applicant.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public IActionResult Details(int id)
        {
            Applicant applicant = _context.Applicants.Include(e=> e.Experiences).Where(a => a.Id == id).FirstOrDefault();
            return View(applicant);
        }
        public IActionResult Delete(int id)
        {
            Applicant applicant = _context.Applicants.Include(e => e.Experiences).Where(a => a.Id == id).FirstOrDefault();
            return View(applicant);

        }
        [HttpPost]
        public IActionResult Delete(Applicant applicant)
        {
            _context.Attach(applicant);
            _context.Entry(applicant).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Applicant applicant = _context.Applicants
            .Include(e => e.Experiences)
            .Where(a => a.Id == id).FirstOrDefault();
            return View(applicant);

        }
        [HttpPost]
        public IActionResult Edit(Applicant applicant)
        {
            List<Experience> expDetails = _context.Experiences.Where(d=>d.ApplicantId == applicant.Id).ToList();
            _context.Experiences.RemoveRange(expDetails);
            _context.SaveChanges();

            applicant.Experiences.RemoveAll(n => n.YearsWorked == 0);
            applicant.Experiences.RemoveAll(n => n.IsDeleted == true);

            if (applicant.ProfilePhoto != null)
            {
                string uniqueFileName = GetUploadedFileName(applicant);
                applicant.PhotoUrl = uniqueFileName;
            }

            _context.Attach(applicant);
            _context.Entry(applicant).State = EntityState.Modified;
            _context.Experiences.AddRange(applicant.Experiences);

            _context.SaveChanges();
            return RedirectToAction("Index");

            }

        
        }
}

