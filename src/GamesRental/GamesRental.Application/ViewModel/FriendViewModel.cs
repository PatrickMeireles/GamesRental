namespace GamesRental.Application.ViewModel
{
    public class FriendViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool HaveRents { get; set; }

        public FriendViewModel() { }

        public FriendViewModel(int _id, string _name, string _email, bool _haveRents = false)
        {
            this.Id = _id;
            this.Name = _name;
            this.Email = _email;
            this.HaveRents = _haveRents;
        }
    }
}
