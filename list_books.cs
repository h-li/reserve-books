public class BookController : Controller
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public ActionResult Index()
    {
        var books = _bookRepository.GetAll();
        return View(books);
    }
}