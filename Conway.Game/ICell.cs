namespace Conway.Game
{
    using System.Collections.Generic;

    public interface ICell<out T>
    {
        IEnumerable<T> Neighbours();
    }
}
