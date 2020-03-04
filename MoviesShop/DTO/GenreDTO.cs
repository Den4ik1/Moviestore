namespace MoviesShop.DTO
{
    public class GenreDTO
    {
        public int Id { get; set; }
        //Жанр которые смотрел пользователь
        public string Title { get; set; }
        //Количество фильмов просмотренных с этим жанром
        public int Count { get; set; }
    }
}
