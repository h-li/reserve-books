public class BookController : Controller
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public BookController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
}

public ActionResult Reserve(Guid id)
{
    var book = _queryDispatcher.Dispatch(new GetBookByIdQuery(id));

    if (book == null)
    {
        return NotFound();
    }

    if (book.IsReserved)
    {
        return BadRequest("This book has already been reserved.");
    }

    var bookingNumber = Guid.NewGuid();
    _commandDispatcher.Dispatch(new ReserveBookCommand(id, bookingNumber));

    return Ok(new { BookingNumber = bookingNumber });
}