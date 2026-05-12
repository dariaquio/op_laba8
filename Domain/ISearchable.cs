namespace ElectronicDeanery.Domain
{
    /// інтерфейс для об'єктів, які можна шукати за ключовим словом.
    /// використовується для демонстрації відношення (реалізація)
    public interface ISearchable
    {
        /// перевіряє, чи відповідає об'єкт ключовому слову пошуку
        bool Matches(string keyword);
    }
}
