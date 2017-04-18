using Microsoft.AspNetCore.Mvc;

using TechJobs.Data;

using TechJobs.ViewModels;

using TechJobs.Models;



namespace TechJobs.Controllers

{

    public class JobController : Controller

    {



        // Our reference to the data store

        private static JobData jobData;





        static JobController()

        {

            jobData = JobData.GetInstance();

        }



        public int id { get; private set; }



        // The detail display for a given Job at URLs like /Job?id=17

        public IActionResult Index(int id)

        {

            Job someJob = jobData.Find(id);

            return View(someJob);

        }



        public IActionResult New()

        {

            NewJobViewModel newJobViewModel = new NewJobViewModel();

            return View(newJobViewModel);

        }



        [HttpPost]

        public IActionResult New(NewJobViewModel newJobViewModel)

        {



            if (ModelState.IsValid)

            {

                JobData jobData = JobData.GetInstance();

                Job newJob = new Job

                {

                    Name = newJobViewModel.Name,

                    Employer = jobData.Employers.Find(newJobViewModel.EmployerID),

                    Location = jobData.Locations.Find(newJobViewModel.LocationID),

                    CoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID),

                    PositionType = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID),



                };

                jobData.Jobs.Add(newJob);

                return Redirect(string.Format("/Job?id={0}", newJob.ID));



            }

            return View(newJobViewModel);

        }

    }

}