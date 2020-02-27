namespace MoviesShop.DTO
{
    //Класс для заполениея промежуточно таблицы
    public class RelationshipStagingDTO
    {
        public int Id { get; set; }
        public int MainId { get; set; }
        public int SecondId { get; set; }
    }
}
