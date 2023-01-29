public class BookController : Controller
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public ActionResult Reserve(Guid id)
    {
        var book = _bookRepository.GetById(id);

        if (book == null)
        {
            return NotFound();
        }

        if (book.IsReserved)
        {
            return BadRequest("This book has already been reserved.");
        }

        book.IsReserved = true;
        _bookRepository.Update(book);

        var bookingNumber = Guid.NewGuid();

        // Raise an event for the reservation
        EventSourcing.RaiseEvent(new BookReservedEvent(book.Id, bookingNumber));

        return Ok(new { BookingNumber = bookingNumber });
    }
}