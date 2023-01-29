public class BookController : Controller
{
    [Authorize]
    public ActionResult Reserve(Guid id)
    {
        // Reserve book logic
    }
}