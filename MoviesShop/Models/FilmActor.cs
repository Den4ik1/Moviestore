namespace MoviesShop.Models
{
    //Промежуточная таблица Фильм и Актёры
    public class FilmActor
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int ActorId { get; set; }
        public Film Film { get; set; }
        public Actor Actor { get; set; }
    }
}
