using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyBooks9.Models
{
    public class MyBooksModel
    {
        public int Id { get; set; }
        [DisplayName("Назва книги")]
        [Required(ErrorMessage = "Це поле не може бути пустим")]
        public string NameBook { get; set; }
        [DisplayName("Жанр книги")]
        [Required(ErrorMessage = "Це поле не може бути пустим")]
        public string GenreBook { get; set; }
        [DisplayName("Автор книги")]
        [Required(ErrorMessage = "Це поле не може бути пустим")]
        public string AuthorBook { get; set; }
        [DisplayName("Сторінки")]
        [Required(ErrorMessage = "Це поле не може бути пустим")]
        public int Pages { get; set; }
        [DisplayName("Дата")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
    }
}
