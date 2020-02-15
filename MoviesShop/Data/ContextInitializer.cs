using MoviesShop.Models;


namespace MoviesShop.Data
{
    public class ContextInitializer
    {
        public void Seed(MoviesShopContext db)
        {
            //Проверка на существование БД
            if ((db.Database.EnsureCreated()))
            {
                Countrys Country1 = new Countrys() { NameOfTheCountry = "Austria" };
                Countrys Country2 = new Countrys() { NameOfTheCountry = "USA" };
                Countrys Country3 = new Countrys() { NameOfTheCountry = "Russia" };
                Countrys Country4 = new Countrys() { NameOfTheCountry = "Germany" };
                Countrys Country5 = new Countrys() { NameOfTheCountry = "Great Britain" };
                Countrys Country6 = new Countrys() { NameOfTheCountry = "Australia " };
                db.Country.AddRange(new Countrys[] { Country1, Country2, Country3, Country4, Country5, Country6 });
                db.SaveChanges();


                Genre Genre1 = new Genre() { Title = "Драма" };
                Genre Genre2 = new Genre() { Title = "Боевик " };
                Genre Genre3 = new Genre() { Title = "Экшн" };
                Genre Genre4 = new Genre() { Title = "Супергеройский фильм" };
                Genre Genre5 = new Genre() { Title = "Комедия" };
                Genre Genre6 = new Genre() { Title = "Фэнтези" };
                Genre Genre7 = new Genre() { Title = "Триллер" };
                db.Genre.AddRange(new Genre[] { Genre1, Genre2, Genre3, Genre4, Genre5, Genre6, Genre7 });
                db.SaveChanges();


                Actor Actor1 = new Actor() { Name = "Морган Фриман", BirthDay = new System.DateTime(1986, 05, 30), Country = Country2 };
                Actor Actor2 = new Actor() { Name = "Аль Пачино", BirthDay = new System.DateTime(1956, 04, 22), Country = Country6 };
                Actor Actor3 = new Actor() { Name = "Кристиан Бейл", BirthDay = new System.DateTime(1937, 03, 14), Country = Country4 };
                Actor Actor4 = new Actor() { Name = "Брэд Питт", BirthDay = new System.DateTime(1977, 02, 16), Country = Country2 };
                db.Actor.AddRange(new Actor[] { Actor1, Actor2, Actor3, Actor4 });
                db.SaveChanges();


               


                User User1 = new User() { Loggin = "GoOd_MeN", Password = "536", Name = "Tom", Age = 25 };
                User User2 = new User() { Loggin = "Mr*PoZitiV4iK", Password = "234", Name = "Den", Age = 15 };
                User User3 = new User() { Loggin = "Vinni_пыххх", Password = "425", Name = "Greg", Age = 52 };
                User User4 = new User() { Loggin = "~Master~ ", Password = "721", Name = "Tim", Age = 10 };
                db.User.AddRange(new User[] { User1, User2, User3, User4 });
                db.SaveChanges();



                Film Film1 = new Film() { Title = "Побег из Шоушенка", Year = 1995, Countrys = Country2 };
                Film Film2 = new Film() { Title = "Крёстный отец", Year = 1985, Countrys = Country1 };
                Film Film3 = new Film() { Title = "Тёмный рыцарь", Year = 2005, Countrys = Country1 };
                Film Film4 = new Film() { Title = "Криминальное чтиво", Year = 2001, Countrys = Country4 };
                Film Film5 = new Film() { Title = "Властелин колец: Возвращение короля", Year = 2003, Countrys = Country5 };
                Film Film6 = new Film() { Title = "Джокер", Year = 2019, Countrys = Country6 };
                Film Film7 = new Film() { Title = "Бойцовский клуб", Year = 2011, Countrys = Country2 };
                db.Film.AddRange(new Film[] { Film1, Film2, Film3, Film4, Film5, Film6, Film7 });
                db.SaveChanges();


                #region Заполнение промежиточной таблицы UserFilm
                Film1.UserFilm.Add(new UserFilm { FilmId = Film1.Id });
                Film1.UserFilm.Add(new UserFilm { FilmId = Film2.Id });
                Film1.UserFilm.Add(new UserFilm { FilmId = Film4.Id });
                Film2.UserFilm.Add(new UserFilm { FilmId = Film3.Id });
                Film2.UserFilm.Add(new UserFilm { FilmId = Film2.Id });
                Film3.UserFilm.Add(new UserFilm { FilmId = Film1.Id });
                Film4.UserFilm.Add(new UserFilm { FilmId = Film4.Id });
                Film5.UserFilm.Add(new UserFilm { FilmId = Film3.Id });
                Film6.UserFilm.Add(new UserFilm { FilmId = Film1.Id });
                Film6.UserFilm.Add(new UserFilm { FilmId = Film4.Id });
                Film7.UserFilm.Add(new UserFilm { FilmId = Film3.Id });
                db.SaveChanges();
                #endregion

                #region Заполнение промежиточной таблицы FilmActor

                Film1.FilmActor.Add(new FilmActor { FilmId = Film1.Id });
                Film1.FilmActor.Add(new FilmActor { FilmId = Film3.Id });
                Film2.FilmActor.Add(new FilmActor { FilmId = Film2.Id });
                Film2.FilmActor.Add(new FilmActor { FilmId = Film1.Id });
                Film3.FilmActor.Add(new FilmActor { FilmId = Film4.Id });
                Film3.FilmActor.Add(new FilmActor { FilmId = Film2.Id });
                Film3.FilmActor.Add(new FilmActor { FilmId = Film3.Id });
                Film4.FilmActor.Add(new FilmActor { FilmId = Film1.Id });
                Film5.FilmActor.Add(new FilmActor { FilmId = Film1.Id });
                Film5.FilmActor.Add(new FilmActor { FilmId = Film4.Id });
                Film5.FilmActor.Add(new FilmActor { FilmId = Film2.Id });
                Film5.FilmActor.Add(new FilmActor { FilmId = Film3.Id });
                Film6.FilmActor.Add(new FilmActor { FilmId = Film1.Id });
                Film6.FilmActor.Add(new FilmActor { FilmId = Film4.Id });
                Film7.FilmActor.Add(new FilmActor { FilmId = Film1.Id });
                db.SaveChanges();
                #endregion


                #region Заполнение промежиточной таблицы FilmGener
                Genre1.FilmGenre.Add(new FilmGenre { GenreId = Genre1.Id });
                Genre2.FilmGenre.Add(new FilmGenre { GenreId = Genre1.Id });
                Genre3.FilmGenre.Add(new FilmGenre { GenreId = Genre2.Id });
                Genre3.FilmGenre.Add(new FilmGenre { GenreId = Genre3.Id });
                Genre3.FilmGenre.Add(new FilmGenre { GenreId = Genre4.Id });
                Genre4.FilmGenre.Add(new FilmGenre { GenreId = Genre2.Id });
                Genre4.FilmGenre.Add(new FilmGenre { GenreId = Genre5.Id });
                Genre5.FilmGenre.Add(new FilmGenre { GenreId = Genre6.Id });
                Genre5.FilmGenre.Add(new FilmGenre { GenreId = Genre3.Id });
                Genre5.FilmGenre.Add(new FilmGenre { GenreId = Genre1.Id });
                Genre6.FilmGenre.Add(new FilmGenre { GenreId = Genre7.Id });
                Genre6.FilmGenre.Add(new FilmGenre { GenreId = Genre4.Id });
                Genre6.FilmGenre.Add(new FilmGenre { GenreId = Genre1.Id });
                Genre7.FilmGenre.Add(new FilmGenre { GenreId = Genre1.Id });
                Genre7.FilmGenre.Add(new FilmGenre { GenreId = Genre7.Id });
                Genre7.FilmGenre.Add(new FilmGenre { GenreId = Genre3.Id });
                db.SaveChanges();
                #endregion


                #region Заполнение таблицы Actors
                ////В 1-ой стране Актёр 1
                //List<Actor> list1 = new List<Actor>();
                //list1.AddRange(new Actor[] { Actor1 });

                ////В 2-ой стране Актёры 2 и 3
                //List<Actor> list2 = new List<Actor>();
                //list2.AddRange(new Actor[] { Actor2, Actor3 });

                ////В 3-й  стране Актёр 4
                //List<Actor> list3 = new List<Actor>();
                //list3.AddRange(new Actor[] { Actor4 });
                #endregion

            }
        }
    }
}
