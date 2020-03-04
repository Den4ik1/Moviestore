namespace MoviesShop.DTO
{
    //Класс для заполениея промежуточных таблицы
    //(используется только для записи в БД)
    public class RelationshipStagingDTO
    {
        //Id связи (создаётся автоматически)
        public int Id { get; set; }
        //Id основного класса
        public int MainId { get; set; }
        //Id вложенного класса
        public int SecondId { get; set; }
    }
}
