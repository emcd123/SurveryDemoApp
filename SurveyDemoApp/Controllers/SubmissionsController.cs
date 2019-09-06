using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SurveyDemoApp.Models;
using SurveyDemoApp.ViewModels;

namespace SurveyDemoApp.Controllers
{
    public class SubmissionsController : Controller
    {
        public ActionResult MultiCreate(List<string> AnswerText, List<int> QuestionId) {
            List<Submission> submissions = new List<Submission>();
            for (int i = 0; i < QuestionId.Count(); i++)
            {
                submissions.Add(new Submission { AnswerText = AnswerText[i], QuestionId = QuestionId[i] });
            }
            if (ModelState.IsValid)
            {
                foreach (Submission submission in submissions)
                {
                    _context.Submission.Add(submission);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(submissions);
        }

        private readonly SurveyDemoAppContext _context;

        public SubmissionsController(SurveyDemoAppContext context)
        {
            _context = context;
        }

        // GET: Submissions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Submission.ToListAsync());
        }

        // GET: Submissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submission = await _context.Submission
                .FirstOrDefaultAsync(m => m.Id == id);
            if (submission == null)
            {
                return NotFound();
            }

            return View(submission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAnswer([Bind("Id,EmployeeId,QuestionId,AnswerText")] Submission submission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(submission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(submission);
        }

        // GET: Submissions/Create
        //public IActionResult Create()
        //{
        //    List<Question> _questions = _context.Question.ToList();
        //    NewSubmissionViewModel vm = new NewSubmissionViewModel();
        //    vm.allQuestions = _questions;

        //    List<Submission> _submissions = new List<Submission>(_questions.Count());
        //    for (int i = 0; i < _submissions.Capacity; i++) _submissions.Add(null);
        //    vm.allSubmissions = _submissions;

        //    return View(vm);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? Id)
        {
            NewSubmissionViewModel vm = new NewSubmissionViewModel();
            List<Question> _questions;
            List<Submission> _submissions;

            if (Id == null)
            {
                _questions = _context.Question.ToList();
                vm.allQuestions = _questions;

                _submissions = new List<Submission>(_questions.Count());
                for (int i = 0; i < _submissions.Capacity; i++) _submissions.Add(null);
                vm.allSubmissions = _submissions;

                return View(vm);
            }

            Survey survey = await _context.Survey.FindAsync(Id);
            _questions = _context.Question.Where(question => survey.QuestionIds.Contains(question.Id.ToString())).ToList();
            vm.allQuestions = _questions;

            _submissions = new List<Submission>(_questions.Count());
            for (int i = 0; i < _submissions.Capacity; i++) _submissions.Add(null);
            vm.allSubmissions = _submissions;

            return View(vm);
        }

        // POST: Submissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,QuestionId,AnswerText")] Submission submission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(submission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(submission);
        }

        // GET: Submissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submission = await _context.Submission.FindAsync(id);
            if (submission == null)
            {
                return NotFound();
            }
            return View(submission);
        }

        // POST: Submissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,QuestionId,AnswerText")] Submission submission)
        {
            if (id != submission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(submission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubmissionExists(submission.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(submission);
        }

        // GET: Submissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submission = await _context.Submission
                .FirstOrDefaultAsync(m => m.Id == id);
            if (submission == null)
            {
                return NotFound();
            }

            return View(submission);
        }

        // POST: Submissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var submission = await _context.Submission.FindAsync(id);
            _context.Submission.Remove(submission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubmissionExists(int id)
        {
            return _context.Submission.Any(e => e.Id == id);
        }
    }
}
