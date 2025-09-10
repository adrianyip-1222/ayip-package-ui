namespace AYip.UI.Pages
{
    /// <summary>
    /// A decorator interface to mark a page as protected.
    /// Protected pages will not be destroyed when there is another page to show.
    /// </summary>
    public interface IProtectedPage : IPage { }
}