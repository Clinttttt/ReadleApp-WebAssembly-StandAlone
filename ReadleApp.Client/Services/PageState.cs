namespace ReadleApp.Client.Services
{
    public class PageState
    {
        public enum PageView
        {
            bookshelve,
            ListMostPopular,
            ListAdventure,
            ListScience,
            ListClassics,
            ListRomance,
            ListMystery,
            ListYoungReaders,
            ListPoetry,
            ListHistory,
            ListShortStories,
            DisplayBook
        }
        public PageView CurrentView { get; private set; } = PageView.bookshelve;
        public event Action? Onchange;

        public void SetView(PageView view)
        {
            CurrentView = view;
            NotifyChanges();
        }
        public void NotifyChanges() => Onchange?.Invoke();
    }
}


   


