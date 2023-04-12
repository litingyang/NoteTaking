using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public interface INotesController
    {
        IActionResult Create();
        Task<IActionResult> Create(int Id, string Title, string Content);
        Task<IActionResult> Delete(int? id);
        Task<IActionResult> DeleteConfirmed(int id);
        Task<IActionResult> Details(int? id);
        Task<IActionResult> Edit(int id, [Bind(new[] { "Id,User,Title,Cotent" })] Note note);
        Task<IActionResult> Edit(int? id);
        Task<List<Note>> GetNotebyName(string name);
        Task<List<Note>> GetSearchResult(string name, string SearchPhrase);
        Task<IActionResult> Index();
        IActionResult ShowSearchForm();
        Task<IActionResult> ShowSearchResults(string SearchPhrase);
    }
}