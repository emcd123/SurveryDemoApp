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
    public class SurveysController : Controller
    {
        private readonly SurveyDemoAppContext _context;

        public SurveysController(SurveyDemoAppContext context)
        {
            _context = context;
        }

        // GET: Surveys
        public async Task<IActionResult> Index()
        {
            return View(await _context.Survey.ToListAsync());
        }

        // GET: Surveys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _context.Survey
                .FirstOrDefaultAsync(m => m.Id == id);
            if (survey == null)
            {
                return NotFound();
            }

            var vm = new QuestionSelectViewModel();
            vm.Survey = survey;
            vm.QuestionSelections = await _context.Question
                                        .Select(a => new QuestionSelection()
                                        {
                                            Id = a.Id,
                                            Text = a.QuestionText
                                        })
                                        .ToListAsync();
            vm.SurveyTitle = survey.Title;
            return View(vm);
        }

        // GET: Surveys/Create
        public async Task<IActionResult> Create()
        {
            var vm = new QuestionSelectViewModel();
            vm.QuestionSelections = await _context.Question
                                        .Select(a => new QuestionSelection()
                                        {
                                            Id = a.Id,
                                            Text = a.QuestionText
                                        })
                                        .ToListAsync();
            return View(vm);
        }

        // POST: Surveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionSelectViewModel model)
        {
            Survey survey = new Survey();
            var selected = model.QuestionSelections.Where(a => a.IsSelected).ToList();
            // If you want Id's select that
            var ids = selected.Select(g => g.Id).ToList();
            var result = string.Join(",", ids.Select(x => x.ToString()).ToArray());

            if (ModelState.IsValid)
            {
                survey.QuestionIdsForDB = result;
                survey.Title = model.SurveyTitle;
                _context.Survey.Add(survey);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(survey);
        }

        // GET: Surveys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _context.Survey.FindAsync(id);
            if (survey == null)
            {
                return NotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,QuestionIdsForDB")] Survey survey)
        {
            if (id != survey.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(survey);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurveyExists(survey.Id))
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
            return View(survey);
        }

        // GET: Surveys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _context.Survey
                .FirstOrDefaultAsync(m => m.Id == id);
            if (survey == null)
            {
                return NotFound();
            }

            return View(survey);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var survey = await _context.Survey.FindAsync(id);
            _context.Survey.Remove(survey);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurveyExists(int id)
        {
            return _context.Survey.Any(e => e.Id == id);
        }
    }
}
