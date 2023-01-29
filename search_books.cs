public class BookController : Controller
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public ActionResult Search(string term)
    {
        var books = _bookRepository.Search(term);
        return View(books);
    }
}